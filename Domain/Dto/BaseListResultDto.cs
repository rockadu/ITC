namespace Domain.Dto;

public class BaseListResultDto<T> where T : class
{
    public int Pagina { get; set; }
    public int ItensPorPagina { get; set; }
    public int Total { get; set; }
    public List<T> Items { get; set; } = new List<T>();

    public BaseListResultDto(int pagina, int itensPorPagina)
    {
        Pagina = pagina;
        ItensPorPagina = itensPorPagina;
    }
}