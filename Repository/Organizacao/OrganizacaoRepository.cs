using CrossCutting.Configuration;
using Domain.Dto;
using Domain.Dto.Organizacao;
using Domain.Models;

namespace Repository.Organizacao;

public class OrganizacaoRepository : IOrganizacaoRepository
{
    private readonly AppSettings _appSettings;

    public OrganizacaoRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public Task<BaseListResultDto<SetorListDto>> ListarCargos(BaseListRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<BaseListResultDto<SetorListDto>> ListarUnidades(BaseListRequestDto request)
    {
        throw new NotImplementedException();
    }
}