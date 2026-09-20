using Autofac.Extensions.DependencyInjection;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceContracts;
using ServiceContracts1;
using Services;
using Serilog;
using firstASPnet.Filters.ActionFilters;
using ServiceContracts1.ServiceContracts;
using ServiceContracts1.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventLog();
    logging.AddEventSourceLogger();
    logging.AddTraceSource("Information, ActivityTracing");
    //logging.AddFile("Logs/myapp-{Date}.txt");
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers();
builder.Services.AddControllersWithViews(options =>
{
options.Filters.Add(new ResponseHeaderActionFilter(
    builder.Services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>(),
    "X-Custom-Key", "Custom-Value", 2));
});
builder.Services.AddScoped<ICountriesService, ICountriesService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.Add(new ServiceDescriptor(
    typeof(ICitiesService), 
    typeof(CitiesService), 
    ServiceLifetime.Transient
    ));

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("MyOwnConfig.json", optional: true, reloadOnChange: true);

});
builder.Services.AddDbContext<ApplictaionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});
// Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonsDatabase;Integrated Security=True;Connect
// Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestHeaders.Add("MyOwnHeader");
    logging.ResponseHeaders.Add("MyOwnHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .WithHeaders("Authorization", "accept", "content-type", "origin")
               .WithMethods("GET", "POST", "PUT", "DELETE");
    });

});

var app = builder.Build();
app.UseHttpLogging();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging() )
    {
    app.UseDeveloperExceptionPage();
}

app.Logger.LogDebug("Application started in {EnvironmentName} mode.", app.Environment.EnvironmentName);
app.Logger.LogInformation("Application started in {EnvironmentName} mode.", app.Environment.EnvironmentName);
app.Logger.LogWarning("Application started in {EnvironmentName} mode.", app.Environment.EnvironmentName);
app.Logger.LogError("Application started in {EnvironmentName} mode.", app.Environment.EnvironmentName);
app.Logger.LogCritical("Application started in {EnvironmentName} mode.", app.Environment.EnvironmentName);


app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.Map("/config", async context =>
    {
        //await.context.Response.WriteAsync(app.Configuration["MyKey"]);
     // await.context.Response.WriteAsync(app.Configuration.GetValue<string>["MyKey"]);
    });
});

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.MapControllers();
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");


app.Map("/map1", async (context) =>
{
    await context.Response.WriteAsync("in map 1");
});

app.Map("/map2", async (context) =>
{
    await context.Response.WriteAsync("in map 2");
});


app.Map("files/{filename}.{extension}", async context =>
{
   string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"in files - {filename} - {extension}");

});
app.Map("emp/profile/{empname:length(3,10):alpha=empname}", async context =>
{
    string? empname = Convert.ToString
    (context.Request.RouteValues["empname"]);
        await context.Response.WriteAsync($"in emp profile - {empname} ");
});
app.Map("prod/dets/{id:int:range(1,1000)?}", async context =>
{
    if (context.Request.RouteValues.ContainsKey("id"))
    {
        int id = Convert.ToInt32
        (context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"prod dets - {id} ");
    }
    else
    {
        await context.Response.WriteAsync($"prod dets - not found :D ");
    }
    
});
app.Map("cities/{cityid=guid}", async(context) =>
{
    Guid cityid = Guid.Parse(Convert.ToString( context.Request.RouteValues["cityid"])!);
    await context.Response.WriteAsync($"city info - {cityid}");

});



app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});



app.Run();  
