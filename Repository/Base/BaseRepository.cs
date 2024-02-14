using System.Data.SqlClient;

namespace Repository.Base;

public class BaseRepository
{
    protected string GetUpdateAttributesByReflection(object entity)
    {
        var resultado = new List<string>();
        var propriedades = entity.GetType().GetProperties();

        foreach (var p in propriedades.Where(x => x.Name.ToUpper() != "Codigo"))
        {
            if (p.PropertyType.ToString().ToLower() == "system.string")
            {
                if (p.GetValue(entity) == null)
                    resultado.Add($"{p.Name} = NULL");
                else
                    resultado.Add($"{p.Name} = '{p.GetValue(entity)}'");
            }
            else if (p.PropertyType.ToString().ToLower().Contains("system.int32"))
            {
                if (p.GetValue(entity) == null)
                    resultado.Add($"{p.Name} = NULL");
                else
                    resultado.Add($"{p.Name} = {p.GetValue(entity) ?? "NULL"}");
            }
            else if (p.PropertyType.ToString().ToLower().Contains("system.decimal"))
            {
                if (p.GetValue(entity) == null)
                    resultado.Add($"{p.Name} = NULL");
                else
                    resultado.Add($"{p.Name} = {p.GetValue(entity)?.ToString()?.Replace(',', '.')}");
            }
            else if (p.PropertyType.ToString().ToLower().Contains("system.datetime"))
            {
                if (p.GetValue(entity) == null || Convert.ToDateTime(p.GetValue(entity)) == new DateTime())
                    resultado.Add($"{p.Name} = NULL");
                else
                    resultado.Add($"{p.Name} = '{Convert.ToDateTime(p.GetValue(entity)).ToString("yyyy-MM-dd HH:mm:ss")}'");
            }
            else if (p.PropertyType.ToString().ToLower().Contains("system.boolean"))
            {
                if (p.GetValue(entity) == null)
                    resultado.Add($"{p.Name} = NULL");
                else
                    resultado.Add($"{p.Name} = {(Convert.ToBoolean(p.GetValue(entity)) ? '1' : '0')}");
            }
        }

        return string.Join(", ", resultado);
    }

    protected string GetInsertAttributesByReflection(object entity)
    {
        var parametros = new List<string>();
        var command = "(";
        var propriedades = entity.GetType().GetProperties();

        foreach (var p in propriedades.Where(x => x.Name != "Codigo"))
        {
            if (p.PropertyType.ToString().ToLower().Contains("system.string") ||
                    p.PropertyType.ToString().ToLower().Contains("system.int32") ||
                    p.PropertyType.ToString().ToLower().Contains("system.decimal") ||
                    p.PropertyType.ToString().ToLower().Contains("system.datetime") ||
                    p.PropertyType.ToString().ToLower().Contains("system.boolean"))
                parametros.Add($"{p.Name}");
        }

        command += string.Join(", ", parametros);
        command += ") VALUES (";

        var valores = new List<string>();

        foreach (var p in propriedades.Where(x => x.Name != "Codigo"))
        {
            if (!parametros.Any(x => x == p.Name))
                continue;

            if (p.GetValue(entity) == null)
                valores.Add("null");
            else if (p.PropertyType.ToString().ToLower() == "system.string")
                valores.Add($"'{p.GetValue(entity).ToString().Replace("'", "''")}'");
            else if (p.PropertyType.ToString().ToLower().Contains("system.int32"))
                valores.Add($"{p.GetValue(entity)}");
            else if (p.PropertyType.ToString().ToLower().Contains("system.decimal"))
                valores.Add($"{p.GetValue(entity).ToString().Replace(',', '.')}");
            else if (p.PropertyType.ToString().ToLower().Contains("system.datetime"))
            {
                if (p.GetValue(entity) == null || Convert.ToDateTime(p.GetValue(entity)) == new DateTime())
                    valores.Add($"NULL");
                else
                    valores.Add($"'{Convert.ToDateTime(p.GetValue(entity)).ToString("yyyy-MM-dd HH:mm:ss")}'");
            }
            else if (p.PropertyType.ToString().ToLower().Contains("system.boolean"))
                valores.Add($"{(Convert.ToBoolean(p.GetValue(entity)) ? '1' : '0')}");
        }

        command += string.Join(", ", valores);
        command += ")";

        return command;
    }

    protected string? GetStringNullable(SqlDataReader reader, string propriedade)
    {
        try
        {
            if (reader[propriedade] != DBNull.Value)
                return reader[propriedade].ToString();
            else
                return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor string de {propriedade} | {ex}");
        }
    }

    protected string GetString(SqlDataReader reader, string propriedade)
    {
        try
        {
            return reader[propriedade].ToString()!;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor string de {propriedade} | {ex}");
        }
    }

    protected double GetDouble(SqlDataReader reader, string propriedade)
    {
        try
        {

            return Convert.ToDouble(reader[propriedade]);

        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor string de {propriedade} | {ex}");
        }
    }

    protected int? GetIntNullable(SqlDataReader reader, string propriedade)
    {
        try
        {
            if (reader[propriedade] != DBNull.Value)
                return Convert.ToInt32(reader[propriedade].ToString());
            else
                return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor int de {propriedade} | {ex}");
        }
    }

    protected decimal? GetDecimalNullable(SqlDataReader reader, string propriedade)
    {
        try
        {
            if (reader[propriedade] != DBNull.Value)
                return Convert.ToDecimal(reader[propriedade].ToString());
            else
                return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor decimal de {propriedade} | {ex}");
        }
    }

    protected DateTime? GetDateTimeNullable(SqlDataReader reader, string propriedade)
    {
        try
        {
            if (reader[propriedade] != DBNull.Value)
                return Convert.ToDateTime(reader[propriedade].ToString());
            else
                return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor DateTime de {propriedade} | {ex}");
        }
    }

    protected bool? GetBoolNullable(SqlDataReader reader, string propriedade)
    {
        try
        {
            if (reader[propriedade] != DBNull.Value)
                return Convert.ToBoolean(reader[propriedade].ToString());
            else
                return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Falha ao recuperar valor bool de {propriedade} | {ex}");
        }
    }

    protected int GetInt(SqlDataReader reader, string propriedade)
    {
        return Convert.ToInt32(GetStringNullable(reader, propriedade));
    }

    protected DateTime GetDateTime(SqlDataReader reader, string propriedade)
    {
        return Convert.ToDateTime(GetStringNullable(reader, propriedade));
    }

    protected bool GetBool(SqlDataReader reader, string propriedade)
    {
        return Convert.ToBoolean(GetStringNullable(reader, propriedade));
    }

    protected decimal GetDecimal(SqlDataReader reader, string propriedade)
    {
        return Convert.ToDecimal(GetStringNullable(reader, propriedade));
    }
}