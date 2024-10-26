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

    public async Task<BaseListResultDto<SetorListDto>> ListarCargos(BaseListRequestDto request)
    {
        var _result = new BaseListResultDto<SetorListDto>(request.Pagina, request.ItensPorPagina);

        string _comando = $@"SELECT
	                usuario.Codigo AS Codigo,
                    usuario.Foto AS Foto,
	                usuario.Nome AS Nome,
	                usuario.Email AS Email,
	                cargo.Nome AS Cargo,
	                setor.Nome AS Setor,
	                unidade.Nome AS Unidade,
	                Usuario.Ativo AS Ativo,
                    COUNT(*) OVER() TotalItens
                FROM
	                Usuario usuario
	                LEFT JOIN Cargo cargo ON usuario.CodigoCargo = cargo.Codigo
	                LEFT JOIN Setor setor ON usuario.CodigoSetor = setor.Codigo
	                LEFT JOIN Unidade unidade ON setor.CodigoUnidade = unidade.Codigo
                WHERE 1 = 1
                    AND @filtro IS NULL OR (usuario.Nome LIKE '%' + @filtro + '%'
	                    OR usuario.Email LIKE '%' + @filtro + '%'
	                    OR cargo.Nome LIKE '%' + @filtro + '%'
	                    OR setor.Nome LIKE '%' + @filtro + '%'
	                    OR unidade.Nome LIKE '%' + @filtro + '%')
				ORDER BY 
                    usuario.Nome
                OFFSET @offset ROW
                FETCH NEXT @itensPorPagina ROWS ONLY";


        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            _result.Items = (await _conexao.QueryAsync<SetorListDto>(_comando, new
            {
                offset = request.Deslocamento,
                itensPorPagina = request.ItensPorPagina,
                filtro = request.Filtro
            })).ToList();

        _result.Total = _result.Items != null && _result.Items.Count > 0 ? _result.Items.First().TotalItens : 0;

        return _result;
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