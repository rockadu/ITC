namespace Domain.Dto.Organizacao;

public class UnidadeListDto
{
    public int Codigo { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool Ativa { get; set; }
    public int TotalItens { get; set; }
}