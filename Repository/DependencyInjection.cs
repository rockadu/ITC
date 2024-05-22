using Microsoft.Extensions.DependencyInjection;
using Repository.Identificacao.Usuario;
using Repository.Organizacao.Setor;

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