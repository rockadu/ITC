namespace Domain.Models;

public class BaseListRequestDto
{
    public int Pagina { get; set; }
    public int ItensPorPagina { get; set; }
    public string? Filtro { get; set; }

    public int Offset()
    {
        return ItensPorPagina * (Pagina <= 1 ? 0 : Pagina - 1);
    }
}