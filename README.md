# firstASPnet — ASP.NET Core MVC learning sample

A C# learning repository exploring MVC, Razor views, entities, service contracts, repositories, filters, and JWT generation. The runnable public demo includes informational pages and a form validation exercise. **This is an educational sample, not a production CRUD application.**

## Run locally

Install a supported .NET SDK capable of targeting .NET 8, plus the ASP.NET Core 8 runtime. Keep both updated with security patches.

```powershell
dotnet restore firstASPnet.sln
dotnet build firstASPnet.sln
dotnet dev-certs https --trust
dotnet run --project firstASPnet --launch-profile https
```

Open <https://localhost:7010>. The development profile also listens on HTTP port 5010; HTTP requests redirect to HTTPS. The public pages require no API key, database, or email account.

## Working public routes

- `/` — project introduction.
- `/company` — accurate project background and implementation status.
- `/contact` — public GitHub profile and issue tracker; no invented phone number or email address.
- `/demo` — server-side validation using fictional project details, with native browser validation and anti-forgery protection.
- `/thank-you` — confirms successful validation. No records are saved and no email is sent.
- `/privacy` — essential security cookie and hosting-log information.
- Unmatched paths return a custom page with an actual HTTP 404 status.
- `/robots.txt` and `/sitemap.xml` — deployment-aware crawler configuration.

## Structure and boundaries

- `firstASPnet` contains the runnable MVC website, views, form model, and static assets.
- `Entities` contains EF Core entities and the database context exercises.
- `RepositoryContracts` and `Repositories` contain data-access contracts and unfinished repository exercises.
- `ServiceContracts`, `ServiceContracts1`, and `Services` contain DTOs, contracts, JWT generation, and unfinished services.
- `Exceptions` contains a custom exception exercise.
- `CRUDTests` contains test fixtures and security regression tests. Its country service is a test double, not a production service.
- `tools/smoke_test.py` checks the real HTTPS server in both pre-deployment and configured-deployment modes.

The original person-management, product, export, and SQL exercises contain unimplemented methods. Their controllers are explicitly marked `NonController`, preserving the source for study while keeping incomplete endpoints offline. There is no production login or validated JWT authentication pipeline. Complete persistence, authenticated access, authorization policies, and business validation before enabling those controllers. Do not use real personal records in the sample.

The legacy PDF renderer is outside `wwwroot` and is not used by the public site. Review its version, provenance, platform compatibility, and licensing before restoring PDF functionality. Never serve executable utilities from the public web root.

## Secrets and personal information

Tracked configuration contains no API credentials or JWT signing value. The previous secret-to-view assignments, sample birth date, fake contact number, and commented personal-record examples have been removed. Public profile links intentionally identify the repository owner.

Use .NET User Secrets locally and a hosting secret manager or server environment variables in production. The standard configuration providers preserve the normal override order; no custom JSON file overrides environment secrets. Never place credentials in Razor views, browser JavaScript, HTML metadata, logs, GitHub issues, or `wwwroot`.

For the **unfinished JWT exercise only**, configure `Jwt:Key` outside the repository using at least 32 cryptographically random bytes, along with `Jwt:Issuer`, `Jwt:Audience`, and `Jwt:EXPIRATION_MINUTES`. Generate a fresh signing value; never reuse a published example. Configuration supplied through environment variables uses double underscores, such as `Jwt__Key`. The running public demo does not use JWT or require this configuration.

`.gitignore` excludes local environment files, local configuration overrides, secret files, private keys, logs, and build outputs. Ignoring a file does not remove earlier commits. A public-history review found illustrative secret placeholders and a hardcoded signing example. Gitleaks 8.30.1 reported no recognized secrets in the three original commits; the manual/pattern review identified no verified provider API credential. These checks cannot guarantee that every possible credential format has been detected.

**If any committed value was ever used as a real credential, revoke or rotate it at its provider.** Deleting it from source does not revoke it.

On October 18, 2026, an owner-approved history rewrite removed the personal birth-date block, published signing example, illustrative secret values, and commented contact-record samples from the published branch histories. The current source trees were verified unchanged by the rewrite. Re-clone existing checkouts before contributing so old history is not accidentally reintroduced. GitHub pull-request references, cached commit pages, forks, or third-party clones can retain old copies; removing GitHub's retained references or caches requires GitHub Support. See [GitHub's sensitive-data removal guidance](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/removing-sensitive-data-from-a-repository).

## Website checklist implemented

- Shared valid HTML layout, unique page titles, meta descriptions, Open Graph metadata, canonical URLs when a domain is configured, favicon, and a compact 1200 × 630 social preview image.
- Responsive navigation, visible keyboard focus, skip link, labelled fields, error summaries, and a fixed mobile contact button.
- Honest public contact links, form error feedback, a POST/redirect/GET thank-you flow, custom 404 and generic error pages.
- HTTPS redirects, production HSTS, host allowlisting, CSP, frame protection, MIME sniffing protection, secure anti-forgery cookies, automatic anti-forgery checks, and per-IP form rate limits.
- No request-body logging, permissive cross-origin policy, secret-rendering endpoint, third-party scripts, analytics, or advertising cookies.
- A privacy page explains the essential anti-forgery cookie. No optional cookies are set; if analytics or advertising is added later, implement real consent choices before those services load.
- Small local assets, versioned CSS links, static asset caching, and Brotli/Gzip for static text. HTML containing anti-forgery tokens is not compressed. The PNG is optimized and the favicon is a small SVG.
- Patched transitive cryptography dependencies and automated build, test, HTTPS smoke, and NuGet vulnerability checks.

## Before deploying a real website

There is no live deployment or verified domain associated with this work. Set the following **server-side** configuration for your actual domain:

```text
ASPNETCORE_ENVIRONMENT=Production
AllowedHosts=your-domain.example;www.your-domain.example
Site__PublicUrl=https://your-domain.example
HttpsPort=443
```

Replace the example domains. Use a valid TLS certificate, keep secrets in your host's secret store, and configure an HTTPS endpoint. If TLS terminates at a reverse proxy, configure ASP.NET Core forwarded headers with explicitly trusted proxies/networks before HTTPS redirection; do not trust arbitrary forwarded headers. Verify redirects end-to-end. See [Microsoft's HTTPS guidance](https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0).

Without a valid HTTPS `Site:PublicUrl`, pages use `noindex`, `robots.txt` disallows crawling, and the sitemap returns 404. This prevents a localhost or invented domain from appearing in search metadata. Once configured, the sitemap includes only public informational pages. Preview, error, and demo pages remain excluded from indexing.

Review host access logs and retention, verify contact links, and test social previews against the deployed domain. A production application also needs completed business features and an operational backup/monitoring plan appropriate to what it stores.

**The under-two-second loading target is not certified.** Local HTTPS response checks are not internet page-load measurements. After deployment, measure the real URL on mobile with Lighthouse or PageSpeed Insights, including cold-cache runs, network latency, and your hosting region. Optimize any measured bottlenecks before claiming a speed result.

## Verification

```powershell
dotnet test firstASPnet.sln
python tools/smoke_test.py
dotnet list firstASPnet.sln package --vulnerable --include-transitive
```

The smoke test requires a local .NET HTTPS development certificate and free ports 17443/17080. It launches and terminates its own server. Certificate verification is disabled **only inside this loopback test client**; production TLS verification is unchanged.

For optional desktop/mobile screenshots, install Playwright in your development tooling and run `node tools/preview.cjs`. It uses installed Edge by default; `BROWSER_CHANNEL=chrome` selects Chrome. `PLAYWRIGHT_MODULE` can point to an existing Playwright module. Screenshots go to ignored `artifacts/`.

## Contact

Maintainer: [omarhussainfahmi on GitHub](https://github.com/omarhussainfahmi). For non-sensitive project questions, use [the issue tracker](https://github.com/omarhussainfahmi/ASP.NET-project-Full-dummy-sample-/issues). Never post credentials, private records, or detailed security exploit information publicly.
