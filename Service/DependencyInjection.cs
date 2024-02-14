using Microsoft.Extensions.DependencyInjection;
using Service.Identificacao;

namespace Service;
public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        
        return services;
    }
}