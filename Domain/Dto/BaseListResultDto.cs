namespace Domain.Dto;

public class BaseListResultDto<T> where T : class
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public int Total { get; set; }
    public List<T> Items { get; set; } = new List<T>();

    public BaseListResultDto(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}