namespace CrossCutting.Configuration.SubAppSettings;
public class DataBase
{
    public string Server { get; set; } = string.Empty;
    public string Base { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Pass { get; set; } = string.Empty;

    public string StringConnection()
    {
        return $"Server={Server};Database={Base};User Id={User};Password={Pass};";
    }
}