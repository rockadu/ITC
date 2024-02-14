﻿using CrossCutting.Utils.HashMd5;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting;
public static class DependencyInjection
{
    public static IServiceCollection AddCrossCutting(this IServiceCollection services)
    {
        services.AddScoped<IMd5, Md5>();

        return services;
    }
}