using Microsoft.Extensions.DependencyInjection;
using Service.Identificacao;
using Service.Organizacao;

namespace Service;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ISetorService, SetorService>();

        return services;
    }
}