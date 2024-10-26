using CrossCutting.Configuration;
using Domain.Entities.Identificacao;
using System.Data.SqlClient;
using Repository.Base;
using Domain.Dto;
using Domain.Dto.Identificacao;
using Domain.Models;
using Dapper;

namespace Repository.Identificacao.Usuario;
public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    private readonly AppSettings _appSettings;
    public UsuarioRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<UsuarioEntity?> BuscarPorEmailESenha(string email, string senhaMd5)
    {
        using (SqlConnection conn = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            conn.Open();
            var cmd = new SqlCommand(@"SELECT 
                                        Codigo,
                                        Nome,
                                        Apelido,
                                        Email,
                                        Senha,
                                        Foto,
                                        CodigoCargo,
                                        CodigoSetor,
                                        CodigoSuperiorImediato,
                                        Ativo
                                    FROM 
                                        Usuario WITH (NOLOCK)
                                    WHERE
                                        Email = @Email
                                        AND Senha = @Senha", conn);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senhaMd5);

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var result = new UsuarioEntity();
                result.Codigo = GetInt(reader, "Codigo");
                result.Nome = GetString(reader, "Nome");
                result.Apelido = GetString(reader, "Apelido");
                result.Email = GetString(reader, "Email");
                result.Senha = GetString(reader, "Senha");
                result.Foto = GetStringNullable(reader, "Foto");
                result.CodigoCargo = GetInt(reader, "CodigoCargo");
                result.CodigoSetor = GetInt(reader, "CodigoSetor");
                result.CodigoSuperiorImediato = GetInt(reader, "CodigoSuperiorImediato");
                result.Ativo = GetBool(reader, "Ativo");

                conn.Close();
                return result;
            }

            conn.Close();
            return null;
        };
    }

    public async Task<BaseListResultDto<UsuarioListDto>> Listar(BaseListRequestDto request)
    {
        var _result = new BaseListResultDto<UsuarioListDto>(request.Pagina, request.ItensPorPagina);

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
            _result.Items = (await _conexao.QueryAsync<UsuarioListDto>(_comando, new { 
                offset = request.Deslocamento, itensPorPagina = request.ItensPorPagina, filtro = request.Filtro })).ToList();

        _result.Total = _result.Items != null && _result.Items.Count > 0 ? _result.Items.First().TotalItens : 0;
                
        return _result;
    }
}