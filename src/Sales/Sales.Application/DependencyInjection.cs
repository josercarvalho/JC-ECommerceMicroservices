using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Behaviors;
using Sales.Application.Orders.Handlers;
using Sales.Application.Orders.Validators;
using Sales.Domain.Interfaces;
using Sales.Infrastructure.Repositories;

namespace Sales.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

            // Add MediatR behaviors for validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
