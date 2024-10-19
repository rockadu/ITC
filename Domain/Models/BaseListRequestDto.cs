namespace Domain.Models;

public class BaseListRequestDto
{
    public int Pagina { get; set; } = 1;
    public int ItensPorPagina { get; set; } = 10;
    public string? Filtro { get; set; }
    public int Deslocamento { get => Offset(); }

    public int Offset()
    {
        return ItensPorPagina * (Pagina <= 1 ? 0 : Pagina - 1);
    }
}