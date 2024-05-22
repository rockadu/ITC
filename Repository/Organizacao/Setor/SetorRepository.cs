using CrossCutting.Configuration;

namespace Repository.Organizacao.Setor;

public class SetorRepository : ISetorRepository
{
    private readonly AppSettings _appSettings;

    public SetorRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task BaseListResultDto<>
}