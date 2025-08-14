using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stock.Infrastructure.Data;
using Stock.API;

namespace Stock.Tests.Integration
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's StockDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StockDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add StockDbContext using an in-memory database for testing.
                services.AddDbContext<StockDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryStockDb");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                // (StockDbContext) and initialize the database.
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<StockDbContext>();

                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
