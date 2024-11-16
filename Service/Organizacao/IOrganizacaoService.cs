using Domain.Dto;
using Domain.Dto.Abstrato;
using Domain.Dto.Organizacao;
using Domain.Entities.Organizacao;
using Domain.Models;
using Domain.Models.Organizacao;

namespace Service.Organizacao;

public interface IOrganizacaoService
{
    public Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestModel request);
    public Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestModel request);
    public Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestModel request);
    public Task<List<ListaParaSelectDto>> SetoresPorUnidadeSelectList(int codigoUnidade);
    public Task<List<ListaParaSelectDto>> CargosSelectList();
    public Task<List<ListaParaSelectDto>> UnidadesSelectList();
    public Task<byte[]> ExportarExcelUnidades();
    public Task ImportarExcelUnidades(Stream excelFile);
    public Task InativarUnidadesRangeAsync(string[] codigosUnidades);
    public Task<UnidadeEntity> AdicionarUnidadeAsync(AdicionarUnidadeModel unidade);
}