namespace Domain.Entities.Identificacao;

public class SetorEntity
{
    public int Codigo { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int CodigoUnidade { get; set; }
    public bool Ativo { get; set; }
    public UnidadeEntity Unidade { get; set; } = new UnidadeEntity();
}