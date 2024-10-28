namespace Domain.Dto;

public interface IBaseListResultDto
{
    public int Pagina { get; set; }
    public int ItensPorPagina { get; set; }
    public int Total { get; set; }
}