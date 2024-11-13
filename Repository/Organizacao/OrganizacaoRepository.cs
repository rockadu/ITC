using CrossCutting.Configuration;
using Dapper;
using Domain.Dto;
using Domain.Dto.Abstrato;
using Domain.Dto.Identificacao;
using Domain.Dto.Organizacao;
using Domain.Entities.Organizacao;
using Domain.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Repository.Organizacao;

public class OrganizacaoRepository : IOrganizacaoRepository
{
    private readonly AppSettings _appSettings;

    public OrganizacaoRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<BaseListResultDto<CargoListDto>> ListarCargosPaginadoAsync(BaseListRequestDto request)
    {
        var _result = new BaseListResultDto<CargoListDto>(request.Pagina, request.ItensPorPagina);

        string _comando = $@"SELECT
	                cargo.Codigo AS Codigo,
	                cargo.Chave AS Chave,
	                cargo.Nome AS Nome,
	                cargo.Ativo AS Ativo,
                    COUNT(*) OVER() TotalItens
                FROM
	                Cargo cargo
                WHERE 1 = 1
                    AND @filtro IS NULL OR (cargo.Codigo LIKE '%' + @filtro + '%'
	                    OR cargo.Chave LIKE '%' + @filtro + '%'
	                    OR cargo.Nome LIKE '%' + @filtro + '%')
				ORDER BY 
                    cargo.Nome
                OFFSET @offset ROW
                FETCH NEXT @itensPorPagina ROWS ONLY";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            _result.Items = (await _conexao.QueryAsync<CargoListDto>(_comando, new
            {
                offset = request.Deslocamento,
                itensPorPagina = request.ItensPorPagina,
                filtro = request.Filtro
            })).ToList();

        _result.Paginacao.Total = _result.Items != null && _result.Items.Count > 0 ? _result.Items.First().TotalItens : 0;

        return _result;
    }

    public async Task<BaseListResultDto<SetorListDto>> ListarSetoresPaginadoAsync(BaseListRequestDto request)
    {
        var _result = new BaseListResultDto<SetorListDto>(request.Pagina, request.ItensPorPagina);

        string _comando = $@"SELECT
	                setor.Codigo AS Codigo,
	                setor.Chave AS Chave,
	                setor.Nome AS Nome,
	                setor.Ativo AS Ativo,
	                unidade.Nome AS Unidade,
                    COUNT(*) OVER() TotalItens
                FROM
	                Setor 
	                LEFT JOIN Unidade unidade ON setor.CodigoUnidade = unidade.Codigo
                WHERE 1 = 1
                    AND @filtro IS NULL OR (setor.Codigo LIKE '%' + @filtro + '%'
	                    OR setor.Chave LIKE '%' + @filtro + '%'
	                    OR setor.Nome LIKE '%' + @filtro + '%'
	                    OR unidade.Nome LIKE '%' + @filtro + '%')
				ORDER BY 
                    setor.Nome
                OFFSET @offset ROW
                FETCH NEXT @itensPorPagina ROWS ONLY";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            _result.Items = (await _conexao.QueryAsync<SetorListDto>(_comando, new
            {
                offset = request.Deslocamento,
                itensPorPagina = request.ItensPorPagina,
                filtro = request.Filtro
            })).ToList();

        _result.Paginacao.Total = _result.Items != null && _result.Items.Count > 0 ? _result.Items.First().TotalItens : 0;

        return _result;
    }

    public async Task<BaseListResultDto<UnidadeListDto>> ListarUnidadesPaginadoAsync(BaseListRequestDto request)
    {
        var _result = new BaseListResultDto<UnidadeListDto>(request.Pagina, request.ItensPorPagina);

        string _comando = $@"SELECT
	                unidade.Codigo AS Codigo,
	                unidade.Chave AS Chave,
	                unidade.Nome AS Nome,
	                unidade.Ativa AS Ativa,
                    COUNT(*) OVER() TotalItens
                FROM
	                Unidade unidade
                WHERE 1 = 1
                    AND @filtro IS NULL OR (unidade.Codigo LIKE '%' + @filtro + '%'
	                    OR unidade.Chave LIKE '%' + @filtro + '%'
	                    OR unidade.Nome LIKE '%' + @filtro + '%')
                    AND unidade.Ativa = 1
				ORDER BY 
                    unidade.Nome
                OFFSET @offset ROW
                FETCH NEXT @itensPorPagina ROWS ONLY";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            _result.Items = (await _conexao.QueryAsync<UnidadeListDto>(_comando, new
            {
                offset = request.Deslocamento,
                itensPorPagina = request.ItensPorPagina,
                filtro = request.Filtro
            })).ToList();

        _result.Paginacao.Total = _result.Items != null && _result.Items.Count > 0 ? _result.Items.First().TotalItens : 0;

        return _result;
    }

    public async Task<List<ListaParaSelectDto>> SetoresPorUnidadeSelectListAsync(int codigoUnidade)
    {
        var _result = new List<SetorListDto>();

        string _comando = $@"SELECT
	                setor.Codigo AS Chave,
	                setor.Nome AS Valor
                FROM
	                Setor setor
                WHERE 
                    setor.CodigoUnidade = @codigoUnidade
                    AND setor.Ativo = 1
				ORDER BY 
                    setor.Nome";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            return (await _conexao.QueryAsync<ListaParaSelectDto>(_comando, new { codigoUnidade = codigoUnidade })).ToList();
    }

    public async Task<List<ListaParaSelectDto>> CargosSelectListAsync()
    {
        var _result = new List<SetorListDto>();

        string _comando = $@"SELECT
	                cargo.Codigo AS Chave,
	                cargo.Nome AS Valor
                FROM
	                Cargo cargo
                WHERE 
                    cargo.Ativo = 1
				ORDER BY 
                    cargo.Nome";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            return (await _conexao.QueryAsync<ListaParaSelectDto>(_comando)).ToList();
    }

    public async Task<List<ListaParaSelectDto>> UnidadesSelectListAsync()
    {
        var _result = new List<SetorListDto>();

        string _comando = $@"SELECT
	                unidade.Codigo AS Chave,
	                unidade.Nome AS Valor
                FROM
	                Unidade unidade
                WHERE 
                    unidade.Ativa = 1
				ORDER BY 
                    unidade.Nome";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            return (await _conexao.QueryAsync<ListaParaSelectDto>(_comando)).ToList();
    }

    public async Task<List<UnidadeEntity>> UnidadesAsync()
    {
        var query = "SELECT * FROM Unidade";

        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            return (await _conexao.QueryAsync<UnidadeEntity>(query)).ToList();
    }

    public async Task AdicionarUnidadeAsync(UnidadeEntity unidade)
    {
        var query = "INSERT INTO Unidade (Chave, Nome, Ativa) VALUES (@Chave, @Nome, @Ativa)";

        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            await _conexao.ExecuteAsync(query, unidade);
    }

    public async Task AtualizarUnidadeAsync(UnidadeEntity unidade)
    {
        var query = "UPDATE Unidade SET Chave = @Chave, Nome = @Nome, Ativa = @Ativa WHERE Codigo = @Codigo";

        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            await _conexao.ExecuteAsync(query, unidade);
    }

    public async Task InativarUnidadesRangeAsync(string[] codigosUnidades)
    {
        var query = "UPDATE Unidade SET Ativa = 0 WHERE Codigo IN @Codigos";

        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            await _conexao.ExecuteAsync(query, new { Codigos = codigosUnidades });
    }
}