using Domain.Dto.Abstrato;

namespace Domain.Dto;

public class BaseListResultDto<T> where T : class
{
    public PaginacaoDto Paginacao { get; set; } = new PaginacaoDto();
    public List<T> Items { get; set; } = new List<T>();

    public BaseListResultDto(int pagina, int itensPorPagina)
    {
        Paginacao.Pagina = pagina;
        Paginacao.ItensPorPagina = itensPorPagina;
    }
}