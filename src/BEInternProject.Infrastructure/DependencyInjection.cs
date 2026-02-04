using BEInternProject.Application.Interfaces;
using BEInternProject.Infrastructure.Data;
using BEInternProject.Infrastructure.Repositories;
using BEInternProject.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BEInternProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}
