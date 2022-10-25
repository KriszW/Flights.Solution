namespace Flights.Contracts.Common;

public sealed class PagedList<T>
{
    public PagedList()
    {

    }

    public PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
        : this()
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items.ToList();
    }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;

    public IReadOnlyCollection<T> Items { get; set; }
}
