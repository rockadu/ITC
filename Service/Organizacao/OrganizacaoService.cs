using ClosedXML.Excel;
using Domain.Dto;
using Domain.Dto.Abstrato;
using Domain.Dto.Organizacao;
using Domain.Entities.Organizacao;
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
        return await _repo.ListarCargosPaginadoAsync(request);
    }

    public async Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request)
    {
        return await _repo.ListarSetoresPaginadoAsync(request);
    }

    public async Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestDto request)
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

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Unidades");

            // Cabeçalho
            worksheet.Cell(1, 1).Value = "Código";
            worksheet.Cell(1, 2).Value = "Chave";
            worksheet.Cell(1, 3).Value = "Nome";
            worksheet.Cell(1, 4).Value = "Ativa";

            // Dados
            for (int i = 0; i < _unidades.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = _unidades[i].Codigo;
                worksheet.Cell(i + 2, 2).Value = _unidades[i].Chave;
                worksheet.Cell(i + 2, 3).Value = _unidades[i].Nome;
                worksheet.Cell(i + 2, 4).Value = _unidades[i].Ativa ? "Sim" : "Não";
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
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
}