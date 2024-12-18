using CrossCutting.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace Repository.Base;

public class BaseRepository
{
    private readonly AppSettings _appSettings;

    public BaseRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<List<T>> ListarAsync<T>() where T : class
    {
        var tableName = typeof(T).Name.Replace("Entity", "");
        var query = $"SELECT * FROM {tableName}";

        using (SqlConnection conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            return (await conexao.QueryAsync<T>(query)).ToList();
        }
    }

    public async Task AtualizarAsync<T>(T objeto, string chavePrimaria) where T : class
    {
        var tableName = typeof(T).Name.Replace("Entity", "");

        var propriedades = typeof(T).GetProperties();

        var campos = propriedades
            .Where(p => p.Name != chavePrimaria)
            .Select(p => $"{p.Name} = @{p.Name}");

        var query = $@"
        UPDATE {tableName}
        SET {string.Join(", ", campos)}
        WHERE {chavePrimaria} = @{chavePrimaria}";

        using (SqlConnection conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            await conexao.ExecuteAsync(query, objeto);
        }
    }

    public async Task InserirAsync<T>(T objeto) where T : class
    {
        var tableName = typeof(T).Name;

        var propriedades = typeof(T).GetProperties();

        var colunas = string.Join(", ", propriedades.Select(p => p.Name));
        var parametros = string.Join(", ", propriedades.Select(p => $"@{p.Name}"));

        var query = $@"
        INSERT INTO {tableName} ({colunas})
        VALUES ({parametros})";

        using (SqlConnection conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            await conexao.ExecuteAsync(query, objeto);
        }
    }

    public async Task ExcluirAsync<T>(int chavePrimariaValor) where T : class
    {
        var tableName = typeof(T).Name;

        var query = $@"
        DELETE FROM {tableName}
        WHERE Codigo = @ChavePrimaria";

        using (SqlConnection conexao = new SqlConnection(_appSettings.DataBase.StringConnection()))
        {
            await conexao.ExecuteAsync(query, new { ChavePrimaria = chavePrimariaValor });
        }
    }
}