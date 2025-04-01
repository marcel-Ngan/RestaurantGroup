using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantGroup.Identity.Domain.Interfaces;
using RestaurantGroup.Identity.Infrastructure.Data.Context;
using RestaurantGroup.Identity.Infrastructure.Data.Repositories;
using RestaurantGroup.Identity.Infrastructure.Security;

namespace RestaurantGroup.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityDatabase"),
                    b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)));

            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Register security services
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            
            // Configure JWT settings
            services.AddOptions<JwtSettings>()
                .Bind(configuration.GetSection("JwtSettings"));

            return services;
        }
    }
}