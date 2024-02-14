namespace Domain.Dto.Identificacao;
public class UsuarioListDto
{
    public int Codigo { get; set; }
    public string? Foto { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}