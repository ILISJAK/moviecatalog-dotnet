using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from user secrets in development environment
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.
builder.Services.AddRazorPages();

// Register the DbContext with the connection string from configuration
builder.Services.AddDbContext<MovieCatalogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieCatalogDbContext")));

// Add controllers
builder.Services.AddControllers();

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure localization options
var supportedCultures = new[]
{
    new CultureInfo("hr"),
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

// Build the app
var app = builder.Build();

// Use localization middleware
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
if (localizationOptions != null)
    app.UseRequestLocalization(localizationOptions);

// Set the default culture for threads in the application
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run("http://*:8080");  // Explicitly specify the URL and port
