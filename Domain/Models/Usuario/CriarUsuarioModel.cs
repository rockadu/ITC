namespace Domain.Models.Usuario;

public class CriarUsuarioModel
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int CodigoUnidade { get; set; }
    public int CodigoSetor { get; set; }
    public int? CodigoCargo { get; set; }
}