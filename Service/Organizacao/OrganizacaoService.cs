using Domain.Dto;
using Domain.Dto.Organizacao;
using Domain.Models;
using Repository.Organizacao;

namespace Service.Organizacao;

public class OrganizacaoService : IOrganizacaoService
{
    private readonly IOrganizacaoRepository _repo;

    public OrganizacaoService(IOrganizacaoRepository setorRepository)
    {
        _repo = setorRepository;
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