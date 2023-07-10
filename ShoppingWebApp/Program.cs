using ShoppingWebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDbContextConfiguration(builder.Configuration)
    .AddDependencyInjection()
    .AddRazorPages();

var app = builder.Build();

app.UseAppConfigurations(builder.Configuration);

app.Run();