using Products.Web.Components;
using Products.Web.Configuration; // Make sure you have this using statement
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

// Configuration is already available here!
var baseUrl = builder.Configuration["API_Base_Url"];

// Validate configuration *before* using it to create services
if (string.IsNullOrWhiteSpace(baseUrl))
{
    throw new ConfigurationErrorsException("API_Base_Url configuration is missing or contains only whitespace.");
}

try
{
    _ = new Uri(baseUrl);
}
catch (UriFormatException ex)
{
    throw new ConfigurationErrorsException($"API_Base_Url configuration is invalid: {ex.Message}");
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl!) }); // Non-null assertion is now safe

builder.Services.AddScoped<IStartupFilter, ConfigurationValidationFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
