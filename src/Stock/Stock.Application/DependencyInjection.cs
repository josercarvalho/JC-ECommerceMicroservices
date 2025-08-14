using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Behaviors;
using Stock.Application.Products.Handlers;
using Stock.Application.Products.Validators;
using Stock.Domain.Interfaces;
using Stock.Infrastructure.Repositories;

namespace Stock.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

            // Add MediatR behaviors for validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
