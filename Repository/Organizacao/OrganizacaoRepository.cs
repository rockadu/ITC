using CrossCutting.Configuration;
using Dapper;
using Domain.Dto;
using Domain.Dto.Identificacao;
using Domain.Dto.Organizacao;
using Domain.Models;
using System.Data.SqlClient;

namespace Repository.Organizacao;

public class OrganizacaoRepository : IOrganizacaoRepository
{
    private readonly AppSettings _appSettings;

    public OrganizacaoRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestDto request)
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

    public async Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request)
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

    public async Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestDto request)
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
}