﻿using Microsoft.Extensions.DependencyInjection;
using Repository.Identificacao;
using Repository.Organizacao;

namespace Repository;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ISetorRepository, SetorRepository>();

        return services;
    }
}