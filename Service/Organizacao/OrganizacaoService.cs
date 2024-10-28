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

    public async Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestDto request)
    {
        return await _repo.ListarCargos(request);
    }

    public async Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request)
    {
        return await _repo.ListarSetores(request);
    }

    public async Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestDto request)
    {
        return await _repo.ListarUnidades(request);
    }
}