using Products.API.Extensions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// TODO: Use strongly-typed configuration and pass it to AddPresentation
builder.Services.AddPresentation(builder.Configuration);

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpPipeline();

app.Run();
