using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestaurantGroup.Identity.Application.Behaviors;

namespace RestaurantGroup.Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            // Register AutoMapper profiles manually
            services.AddSingleton(provider => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            }).CreateMapper());
            
            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            // Register pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}