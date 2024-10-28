namespace Domain.Dto.Organizacao;

public class CargoListDto
{
    public int Codigo { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public int TotalItens { get; set; }
}