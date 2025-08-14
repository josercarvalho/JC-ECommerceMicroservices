using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sales.Infrastructure.Data;
using Sales.API;

namespace Sales.Tests.Integration
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's SalesDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<SalesDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add SalesDbContext using an in-memory database for testing.
                services.AddDbContext<SalesDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemorySalesDb");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                // (SalesDbContext) and initialize the database.
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SalesDbContext>();

                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
