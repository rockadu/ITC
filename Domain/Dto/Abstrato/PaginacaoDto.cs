namespace Domain.Dto.Abstrato;

public class PaginacaoDto
{
    public int Pagina { get; set; }
    public int ItensPorPagina { get; set; }
    public int Total { get; set; }
    public int TotalPaginas { get => (int)Math.Ceiling((double)Total / ItensPorPagina); }
}