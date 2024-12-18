using ClosedXML.Excel;
using CrossCutting.Exceptions;
using CrossCutting.Utils.Excel;
using Domain.Dto;
using Domain.Dto.Abstrato;
using Domain.Dto.Organizacao;
using Domain.Entities.Organizacao;
using Domain.Models;
using Domain.Models.Organizacao;
using Repository.Organizacao;

namespace Service.Organizacao;

public class OrganizacaoService : IOrganizacaoService
{
    private readonly IOrganizacaoRepository _repo;
    private readonly IExcelUtils _excelUtils;

    public OrganizacaoService(IOrganizacaoRepository setorRepository, IExcelUtils excelUtils)
    {
        _repo = setorRepository;
        _excelUtils = excelUtils;
    }

    public async Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestModel request)
    {
        return await _repo.ListarCargosPaginadoAsync(request);
    }

    public async Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestModel request)
    {
        return await _repo.ListarSetoresPaginadoAsync(request);
    }

    public async Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestModel request)
    {
        return await _repo.ListarUnidadesPaginadoAsync(request);
    }

    public async Task<List<ListaParaSelectDto>> CargosSelectList()
    {
        return await _repo.CargosSelectListAsync();
    }

    public async Task<List<ListaParaSelectDto>> SetoresPorUnidadeSelectList(int codigoUnidade)
    {
        return await _repo.SetoresPorUnidadeSelectListAsync(codigoUnidade);
    }

    public async Task<List<ListaParaSelectDto>> UnidadesSelectList()
    {
        return await _repo.UnidadesSelectListAsync();
    }

    public async Task<byte[]> ExportarExcelUnidades()
    {
        var _unidades = await _repo.UnidadesAsync();

        return _excelUtils.GerarExcel(_unidades);
    }

    public async Task<byte[]> ExportarExcelSetores()
    {
        var _setores = await _repo.SetoresAsync();

        return _excelUtils.GerarExcel(_setores);
    }

    public async Task<byte[]> ExportarExcelCargos()
    {
        var _cargos = await _repo.CargosAsync();

        return _excelUtils.GerarExcel(_cargos);
    }

    public async Task ImportarExcelUnidades(Stream excelFile)
    {
        using (var stream = new MemoryStream())
        {
            await excelFile.CopyToAsync(stream);
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                var unidadesExistentes = await _repo.UnidadesAsync();

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var codigo = row.Cell(1).GetValue<int?>();
                    var chave = row.Cell(2).GetValue<string>();
                    var nome = row.Cell(3).GetValue<string>();
                    var ativa = row.Cell(4).GetValue<string>().ToLower() == "sim";

                    var unidade = unidadesExistentes.FirstOrDefault(u => u.Codigo == codigo || u.Chave == chave);
                    if (unidade != null)
                    {
                        unidade.Chave = chave;
                        unidade.Nome = nome;
                        unidade.Ativa = ativa;
                        await _repo.AtualizarUnidadeAsync(unidade);
                    }
                    else
                    {
                        var novaUnidade = new UnidadeEntity
                        {
                            Chave = chave,
                            Nome = nome,
                            Ativa = ativa
                        };
                        await _repo.AdicionarUnidadeAsync(novaUnidade);
                    }
                }
            }
        }
    }

    public async Task InativarUnidadesRangeAsync(int[] codigosUnidades)
    {
        await _repo.InativarUnidadesRangeAsync(codigosUnidades);
    }

    public async Task<UnidadeEntity> AdicionarUnidadeAsync(AdicionarUnidadeModel unidade)
    {
        if (await _repo.BuscarUnidadeExistenteAsync(unidade.Chave, unidade.Nome))
            throw new JaExisteException();

        var _entidade = new UnidadeEntity();
        _entidade.Nome = unidade.Nome;
        _entidade.Chave = unidade.Chave;
        _entidade.Ativa = true;

        return await _repo.AdicionarUnidadeAsync(_entidade);
    }
}