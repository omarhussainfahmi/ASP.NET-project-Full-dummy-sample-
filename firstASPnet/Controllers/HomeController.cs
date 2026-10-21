using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using firstASPnet.Models;
using System.Xml.Linq;

namespace firstASPnet.Controllers;
public class HomeController : Controller
{
    [HttpGet("/")] public IActionResult Index() => View();
    [HttpGet("/company")] public IActionResult About() => View();
    [HttpGet("/contact")] public IActionResult Contact() => View();
    [HttpGet("/privacy")] public IActionResult Privacy() => View();
    [HttpGet("/terms")] public IActionResult Terms() => View();
    [HttpGet("/demo")] public IActionResult Demo() => View(new DemoRequest());
    [HttpPost("/demo"), EnableRateLimiting("form"), RequestSizeLimit(16 * 1024)]
    public IActionResult Demo(DemoRequest request)
    {
        Response.Headers["Cache-Control"] = "no-store";
        if (!ModelState.IsValid) return View(request);
        // Validation demonstration only: no database, mail, logging, or personal data storage.
        return RedirectToAction(nameof(ThankYou));
    }
    [HttpGet("/thank-you")] public IActionResult ThankYou() => View();
    [Route("/error")]
    public IActionResult Error() { Response.StatusCode = 500; return View("Error"); }
    [Route("/status/{code:int}")]
    public IActionResult StatusPage(int code) {
        if (code < 400 || code > 599) return BadRequest();
        Response.StatusCode = code;
        return View(code switch { 404 => "NotFound", 429 => "RateLimited", _ => "Error" });
    }
    [HttpGet("/robots.txt")]
    public IActionResult Robots([FromServices] IConfiguration config) {
        var url = PublicUrl(config);
        return Content(url is null ? "User-agent: *\nDisallow: /\n" :
            $"User-agent: *\nAllow: /\nDisallow: /demo\nDisallow: /thank-you\nDisallow: /status/\nDisallow: /error\nSitemap: {url}/sitemap.xml\n", "text/plain");
    }
    [HttpGet("/sitemap.xml")]
    public IActionResult Sitemap([FromServices] IConfiguration config) {
        var url = PublicUrl(config);
        if (url is null) return NotFound();
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        return Content(new XDocument(new XElement(ns + "urlset",
            new[] { "", "/company", "/contact", "/privacy", "/terms" }.Select(path =>
                new XElement(ns + "url", new XElement(ns + "loc", url + path)))))
            .ToString(), "application/xml");
    }
    public static string? PublicUrl(IConfiguration config) =>
        Uri.TryCreate(config["Site:PublicUrl"], UriKind.Absolute, out var uri)
        && uri.Scheme == "https" && string.IsNullOrEmpty(uri.UserInfo)
        && uri.AbsolutePath == "/" && string.IsNullOrEmpty(uri.Query) && string.IsNullOrEmpty(uri.Fragment)
            ? uri.GetLeftPart(UriPartial.Authority) : null;
}
