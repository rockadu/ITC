using Domain.Dto;
using Domain.Dto.Abstrato;
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

    public async Task<List<ListaParaSelectDto>> CargosSelectList()
    {
        return await _repo.CargosSelectList();
    }

    public async Task<List<ListaParaSelectDto>> SetoresPorUnidadeSelectList(int codigoUnidade)
    {
        return await _repo.SetoresPorUnidadeSelectList(codigoUnidade);
    }

    public async Task<List<ListaParaSelectDto>> UnidadesSelectList()
    {
        return await _repo.UnidadesSelectList();
    }
}