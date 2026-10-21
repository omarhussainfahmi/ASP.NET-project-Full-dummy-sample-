"""Exercise the real local HTTPS server. Requires a .NET development certificate.
Run after dotnet build firstASPnet.sln. No external Python packages required.
"""
import gzip
import http.client
from html.parser import HTMLParser
from http.cookies import SimpleCookie
import os
from pathlib import Path
import ssl
import statistics
import subprocess
import time
from urllib.parse import urlencode, urlsplit

ROOT = Path(__file__).resolve().parents[1]
TLS_PORT, HTTP_PORT = 17443, 17080
CONTEXT = ssl._create_unverified_context()  # Loopback test certificate only.

class Page(HTMLParser):
    def __init__(self, text):
        super().__init__()
        self.links, self.tokens, self.titles, self.descriptions = [], [], 0, 0
        self.feed(text)
    def handle_starttag(self, tag, attrs):
        attrs = dict(attrs)
        if tag == 'title': self.titles += 1
        if tag == 'meta' and attrs.get('name') == 'description': self.descriptions += 1
        if attrs.get('name') == '__RequestVerificationToken': self.tokens.append(attrs['value'])
        if tag in ('a','link','img','script'):
            value = attrs.get('href') or attrs.get('src')
            if value and value.startswith('/') and not value.startswith('//'): self.links.append(value)

def request(path, method='GET', data=None, headers=None, secure=True):
    conn = http.client.HTTPSConnection('localhost', TLS_PORT, context=CONTEXT, timeout=10) if secure else http.client.HTTPConnection('localhost', HTTP_PORT, timeout=10)
    conn.request(method, path, body=data, headers=headers or {})
    response = conn.getresponse()
    body = response.read()
    result = response.status, dict(response.getheaders()), body
    conn.close()
    return result

def run(configured):
    env = dict(os.environ, ASPNETCORE_ENVIRONMENT='Production', HttpsPort=str(TLS_PORT), AllowedHosts='localhost;127.0.0.1;example.test', Site__PublicUrl='https://example.test' if configured else '')
    with open(ROOT / 'smoke-server.log','w',encoding='utf-8') as log:
        process = subprocess.Popen(['dotnet','run','--no-build','--project',str(ROOT/'firstASPnet/firstASPnet.csproj'),'--no-launch-profile','--urls',f'https://localhost:{TLS_PORT};http://localhost:{HTTP_PORT}'],env=env,stdout=log,stderr=log,cwd=ROOT)
        try:
            for _ in range(100):
                try:
                    if request('/')[0] == 200: break
                except OSError: pass
                if process.poll() is not None: raise AssertionError('Server exited; inspect smoke-server.log')
                time.sleep(.1)
            else: raise AssertionError('Server did not start')
            links = set()
            for path in ['/', '/company', '/contact', '/privacy', '/terms', '/demo', '/thank-you']:
                status, headers, body = request(path)
                assert status == 200, (path,status)
                html = body.decode()
                page = Page(html)
                assert page.titles == 1 and page.descriptions == 1, path
                assert html.lower().count('<html') == 1, path
                assert 'Content-Security-Policy' in headers and headers['X-Content-Type-Options'] == 'nosniff'
                assert 'ClientSecret' not in html and 'weatherapi' not in html
                assert ('rel="canonical"' in html) == configured
                assert html.count('aria-current="page"') == (1 if path in ['/', '/company', '/contact', '/demo'] else 0), path
                links.update(page.links)
            for link in links:
                assert request(link)[0] == 200, link
            for path in ['/missing-page','/persons/index','/PersonsCSV','/PersonsExcel','/products','/config','/appsettings.json','/Rotativa/wkhtmltopdf.exe','/wkhtmltopdf.exe']:
                status, _, body = request(path)
                assert status == 404 and b'Page not found' in body, (path,status)
            assert request('/error')[0] == 500
            status, headers, _ = request('/', secure=False)
            assert status == 308 and headers['Location'] == f'https://localhost:{TLS_PORT}/'
            assert 'Strict-Transport-Security' in request('/',headers={'Host':'example.test'})[1]
            assert request('/',headers={'Host':'untrusted.invalid'})[0] == 400
            status, headers, body = request('/demo')
            cookie = SimpleCookie(); cookie.load(headers['Set-Cookie'])
            cookie_header = '; '.join(f'{key}={value.value}' for key,value in cookie.items())
            assert 'secure' in headers['Set-Cookie'].lower() and 'httponly' in headers['Set-Cookie'].lower()
            token = Page(body.decode()).tokens[0]
            form_headers = {'Content-Type':'application/x-www-form-urlencoded','Cookie':cookie_header}
            assert request('/demo','POST',urlencode({'ProjectName':'Example'}),form_headers)[0] == 400
            invalid = urlencode({'__RequestVerificationToken':token,'ProjectName':'','RecordCount':'101'})
            status, headers, body = request('/demo','POST',invalid,form_headers)
            assert status == 200 and b'Enter a sample project name.' in body and b'from 1 to 100' in body
            assert body.count(b'aria-invalid="true"') == 2 and b'tabindex="-1"' in body
            assert b'validation-focus.js?v=' in body
            assert 'no-store' in headers['Cache-Control']
            attack = urlencode({'__RequestVerificationToken':token,'ProjectName':'<script>alert(1)</script>','RecordCount':'101'})
            _, _, body = request('/demo','POST',attack,form_headers)
            assert b'<script>alert(1)</script>' not in body and b'&lt;script&gt;' in body
            valid = urlencode({'__RequestVerificationToken':token,'ProjectName':'Fictional project','RecordCount':'3'})
            status, headers, _ = request('/demo','POST',valid,form_headers)
            assert status == 302 and headers['Location'] == '/thank-you'
            oversized = urlencode({'__RequestVerificationToken':token,'ProjectName':'A' * 17000,'RecordCount':'1'})
            # MVC anti-forgery can translate a rejected body read into HTTP 400.
            assert request('/demo','POST',oversized,form_headers)[0] in (400, 413)
            assert request('/demo','GET',headers={'Accept-Encoding':'gzip'})[1].get('Content-Encoding') is None
            status, headers, body = request('/StyleSheet.css',headers={'Accept-Encoding':'gzip'})
            assert status == 200 and headers.get('Content-Encoding') == 'gzip' and len(gzip.decompress(body)) > 0
            robots = request('/robots.txt')[2].decode()
            sitemap_status, _, sitemap = request('/sitemap.xml')
            if configured:
                assert 'Sitemap: https://example.test/sitemap.xml' in robots
                assert sitemap_status == 200 and sitemap.count(b'<loc>') == 5
                assert b'https://example.test/terms' in sitemap
                assert b'https://example.test/company' in sitemap
            else:
                assert 'Disallow: /\n' in robots and sitemap_status == 404
            limited = [response for response in [request('/demo','POST',invalid,form_headers) for _ in range(12)] if response[0] == 429]
            assert limited
            for _, headers, body in limited:
                assert 1 <= int(headers['Retry-After']) <= 60
                assert 'no-store' in headers['Cache-Control']
                assert b'Please wait before trying again' in body and b'/demo' in body
                assert b'StackTrace' not in body
            times = []
            for _ in range(10):
                start=time.perf_counter(); request('/'); times.append((time.perf_counter()-start)*1000)
            print(f'PASS: {"configured deployment" if configured else "pre-deployment"}; routes, metadata, links, TLS redirect, HSTS, host filtering, CSRF, errors, escaping, PRG, rate limits, assets, robots/sitemap. Local HTTPS median: {statistics.median(times):.1f} ms (not browser load speed).')
        finally:
            process.terminate()
            try: process.wait(timeout=10)
            except subprocess.TimeoutExpired: process.kill(); process.wait()

if __name__ == '__main__':
    run(False)
    run(True)
