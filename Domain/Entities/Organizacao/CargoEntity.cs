namespace Domain.Entities.Organizacao;
public class CargoEntity
{
    public int Codigo { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}