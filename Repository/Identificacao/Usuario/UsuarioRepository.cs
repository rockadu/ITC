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
        var items = new List<UsuarioListDto>();
        var result = new BaseListResultDto<UsuarioListDto>(request.Pagina, request.ItensPorPagina);

        string _filtro = $@"AND usuario.Nome LIKE '%{request.Filtro}%'
	                    OR usuario.Email LIKE '%{request.Filtro}%'
	                    OR cargo.Nome LIKE '%{request.Filtro}%'
	                    OR setor.Nome LIKE '%{request.Filtro}%'
	                    OR unidade.Nome LIKE '%{request.Filtro}%'";

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
                    #FILTER#
				ORDER BY usuario.Nome
                OFFSET @offset ROW
                FETCH NEXT @itensPorPagina ROWS ONLY";

        if (!string.IsNullOrEmpty(request.Filtro))
            _comando = _comando.Replace("#FILTER#", _filtro);
        else
            _comando = _comando.Replace("#FILTER#", string.Empty);

        using (SqlConnection _conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
            var result = await _conexao.QueryAsync<UsuarioListDto>(_comando, new { offset = request.Deslocamento, itensPorPagina = request.ItensPorPagina });
                

    }
}