using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using System.Globalization;

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

// Build the app
var app = builder.Build();

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

app.Run();
