using System;
using Infrastructure.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));



        return services;
    }
}