using Domain.Dto.Organizacao;
using Domain.Dto;
using Domain.Models;
using Domain.Dto.Abstrato;
using Domain.Entities.Organizacao;

namespace Repository.Organizacao;

public interface IOrganizacaoRepository
{
    public Task<BaseListResultDto<SetorListDto>> ListarSetoresPaginadoAsync(BaseListRequestDto request);
    public Task<BaseListResultDto<CargoListDto>> ListarCargosPaginadoAsync(BaseListRequestDto request);
    public Task<BaseListResultDto<UnidadeListDto>> ListarUnidadesPaginadoAsync(BaseListRequestDto request);
    public Task<List<ListaParaSelectDto>> SetoresPorUnidadeSelectListAsync(int codigoUnidade);
    public Task<List<ListaParaSelectDto>> CargosSelectListAsync();
    public Task<List<ListaParaSelectDto>> UnidadesSelectListAsync();
    public Task<List<UnidadeEntity>> UnidadesAsync();
    public Task AdicionarUnidadeAsync(UnidadeEntity unidade);
    public Task AtualizarUnidadeAsync(UnidadeEntity unidade);
}