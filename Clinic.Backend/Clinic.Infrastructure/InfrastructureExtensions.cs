﻿using Clinic.Application.Interfaces.Auth;
using Clinic.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}