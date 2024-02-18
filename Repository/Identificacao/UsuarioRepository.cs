using CrossCutting.Configuration;
using Domain.Entities.Identificacao;
using System.Data.SqlClient;
using Repository.Base;
using Domain.Dto;
using Domain.Dto.Identificacao;
using Domain.Models;

namespace Repository.Identificacao;
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
        var result = new BaseListResultDto<UsuarioListDto>(request.PageIndex, request.PageSize);

        string filter = $@"AND usuario.Nome LIKE '%{request.Filter}%'
	                    OR usuario.Email LIKE '%{request.Filter}%'
	                    OR cargo.Nome LIKE '%{request.Filter}%'
	                    OR setor.Nome LIKE '%{request.Filter}%'
	                    OR unidade.Nome LIKE '%{request.Filter}%'";

        string query = $@"SELECT
	                usuario.Codigo AS Codigo,
                    usuario.Foto AS Foto,
	                usuario.Nome AS Nome,
	                usuario.Email AS Email,
	                cargo.Nome AS Cargo,
	                setor.Nome AS Setor,
	                unidade.Nome AS Unidade,
	                Usuario.Ativo AS Ativo
                FROM
	                Usuario usuario
	                LEFT JOIN Cargo cargo ON usuario.CodigoCargo = cargo.Codigo
	                LEFT JOIN Setor setor ON usuario.CodigoSetor = setor.Codigo
	                LEFT JOIN Unidade unidade ON setor.CodigoUnidade = unidade.Codigo
                WHERE 1 = 1
                    #FILTER#
				ORDER BY usuario.Nome
                OFFSET {request.Offset()} ROW
                FETCH NEXT {request.PageSize} ROWS ONLY";

        string total = $@"SELECT
                            COUNT(*) AS TOTAL
                        FROM
	                        Usuario usuario
	                        LEFT JOIN Cargo cargo ON usuario.CodigoCargo = cargo.Codigo
	                        LEFT JOIN Setor setor ON usuario.CodigoSetor = setor.Codigo
	                        LEFT JOIN Unidade unidade ON setor.CodigoUnidade = unidade.Codigo
                        WHERE 1 = 1
                            #FILTER#";

        if (!string.IsNullOrEmpty(request.Filter))
        {
            query = query.Replace("#FILTER#", filter);
            total = total.Replace("#FILTER#", filter);
        }
        else
        {
            query = query.Replace("#FILTER#", string.Empty);
            total = total.Replace("#FILTER#", string.Empty);
        }

        using (SqlConnection conn = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            conn.Open();
            var cmd = new SqlCommand(query, conn);

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var usuario = new UsuarioListDto();
                usuario.Codigo = GetInt(reader, "Codigo");
                usuario.Foto = GetStringNullable(reader, "Foto");
                usuario.Nome = GetString(reader, "Nome");
                usuario.Email = GetString(reader, "Email");
                usuario.Cargo = GetString(reader, "Cargo");
                usuario.Setor = GetString(reader, "Setor");
                usuario.Unidade = GetString(reader, "Unidade");
                usuario.Ativo = GetBool(reader, "Ativo");

                items.Add(usuario);
            }

            conn.Close();
        };

        using (SqlConnection conn = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            conn.Open();
            var cmd = new SqlCommand(total, conn);

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Total = GetInt(reader, "TOTAL");
            }

            conn.Close();
        };

        result.Items = items;
        return result;
    }
}