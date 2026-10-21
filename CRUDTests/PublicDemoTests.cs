using System.ComponentModel.DataAnnotations;
using firstASPnet.Controllers;
using firstASPnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CRUDTests;

public class PublicDemoTests
{
    [Theory]
    [InlineData(null, 1)]
    [InlineData(" ", 1)]
    [InlineData("Sample", null)]
    [InlineData("Sample", 0)]
    [InlineData("Sample", 101)]
    public void InvalidFictionalDetailsFailValidation(string? name, int? count)
    {
        var request = new DemoRequest { ProjectName = name, RecordCount = count };
        Assert.False(Validator.TryValidateObject(request, new ValidationContext(request), [], true));
    }

    [Fact]
    public void NameLengthAndRecordBoundariesAreEnforced()
    {
        foreach (var count in new[] { 1, 100 })
        {
            var request = new DemoRequest { ProjectName = new string('A', 80), RecordCount = count };
            Assert.True(Validator.TryValidateObject(request, new ValidationContext(request), [], true));
        }
        var tooLong = new DemoRequest { ProjectName = new string('A', 81), RecordCount = 1 };
        Assert.False(Validator.TryValidateObject(tooLong, new ValidationContext(tooLong), [], true));
    }

    private static HomeController Controller() => new() {
        ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
    };

    [Fact]
    public void InvalidFormPreservesErrorsAndValidFormRedirectsWithoutCachingInput()
    {
        var controller = Controller();
        var request = new DemoRequest { ProjectName = "Fictional project", RecordCount = 1 };
        controller.ModelState.AddModelError(nameof(DemoRequest.RecordCount), "Enter a number from 1 to 100.");
        Assert.Same(request, Assert.IsType<ViewResult>(controller.Demo(request)).Model);
        Assert.Equal("no-store", controller.Response.Headers.CacheControl);
        controller.ModelState.Clear();
        Assert.Equal("ThankYou", Assert.IsType<RedirectToActionResult>(controller.Demo(request)).ActionName);
    }

    [Fact]
    public void RateLimitPageKeeps429AndOtherStatusPagesKeepTheirMeaning()
    {
        var controller = Controller();
        Assert.Equal("RateLimited", Assert.IsType<ViewResult>(controller.StatusPage(429)).ViewName);
        Assert.Equal(429, controller.Response.StatusCode);
        Assert.Equal("NotFound", Assert.IsType<ViewResult>(controller.StatusPage(404)).ViewName);
        Assert.Equal(404, controller.Response.StatusCode);
        Assert.IsType<BadRequestResult>(controller.StatusPage(200));
    }

    [Theory]
    [InlineData("http://example.test")]
    [InlineData("https://user:password@example.test")]
    [InlineData("https://example.test/private")]
    [InlineData("https://example.test/?token=fixture")]
    public void InvalidPublicOriginsNeverEnterMetadata(string origin)
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Site:PublicUrl"] = origin }).Build();
        Assert.Null(HomeController.PublicUrl(config));
    }
}
