using Domain.Dto;
using Domain.Dto.Abstrato;
using Domain.Dto.Organizacao;
using Domain.Models;

namespace Service.Organizacao;

public interface IOrganizacaoService
{
    public Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request);
    public Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestDto request);
    public Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestDto request);
    public Task<List<ListaParaSelectDto>> SetoresPorUnidadeSelectList(int codigoUnidade);
    public Task<List<ListaParaSelectDto>> CargosSelectList();
    public Task<List<ListaParaSelectDto>> UnidadesSelectList();
}