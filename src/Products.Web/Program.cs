using Products.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var baseUrl = builder.Configuration["API_Base_Url"];
if (!string.IsNullOrEmpty(baseUrl))
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
}
else
{
    // TODO: Handle missing configuration (e.g., log a warning, use a default value)
    // Handle missing configuration (e.g., log a warning, use a default value)
    Console.WriteLine("Warning: API_Base_Url configuration not found.");
}

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
