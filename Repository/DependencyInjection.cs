using Microsoft.Extensions.DependencyInjection;
using Repository.Identificacao;

namespace Repository;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}