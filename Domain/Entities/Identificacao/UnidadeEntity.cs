namespace Domain.Entities.Identificacao;
public class UnidadeEntity
{
    public int Codigo { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool Ativa { get; set; }
}