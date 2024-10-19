using Microsoft.Extensions.DependencyInjection;
using Repository.Identificacao.Usuario;
using Repository.Organizacao;

namespace Repository;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IOrganizacaoRepository, OrganizacaoRepository>();

        return services;
    }
}