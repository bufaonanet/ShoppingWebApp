using Microsoft.EntityFrameworkCore;
using ShoppingWebApp.Data;
using ShoppingWebApp.Repositories.Interfaces;
using ShoppingWebApp.Repositories;

namespace ShoppingWebApp;

public static class StartupHelperExtensions
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");
        if (useInMemoryDatabase)
        {
            services.AddDbContext<ShoppingContext>(c =>
                           c.UseInMemoryDatabase("ShoppingWebApp"));
        }
        else
        {
            var connectionString = configuration.GetConnectionString("ShoppingWebAppConnection");
            services.AddDbContext<ShoppingContext>(c =>
                           c.UseSqlServer(connectionString));
        }

        return services;
    }

    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        // add repository dependecy
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }

    public static void UseAppConfigurations(this WebApplication app, IConfiguration configuration)
    {
        var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");
        app.ResetDatabaseAsync(useInMemoryDatabase).Wait();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
    }

    public static async Task ResetDatabaseAsync(this WebApplication app, bool useInMemoryDatabase)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ShoppingContext>();
        var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
        try
        {
            if (useInMemoryDatabase is false)
            {
                context.Database.EnsureCreated();
            }

            await ShoppingContextSeed.SeedAsync(context, loggerFactory);

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}
