namespace Domain.Models;

public class BaseListRequestDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }

    public int Offset()
    {
        return PageSize * (PageIndex - 1);
    }
}