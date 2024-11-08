namespace Domain.Dto.Identificacao;
public class UsuarioListDto
{
    public int Codigo { get; set; }
    string? _Foto;
    public string? Foto { get => string.IsNullOrEmpty(_Foto) ? "/images/default/FotoPadrao.webp" : _Foto; set => _Foto = value; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public int TotalItens { get; set; }
}