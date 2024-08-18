namespace Market.Warehouse.Application.Dto;

public class BaseSortFilterDto
{
    public const int DEFAULT_PAGE_SIZE = 10;
    private int _pageSize;
    private int _page;
    private string? _sortType;
    private string? _sortBy;

    public int PageSize
    {
        get => _pageSize > 0 && _pageSize <= DEFAULT_PAGE_SIZE ? _pageSize : DEFAULT_PAGE_SIZE;
        set => _pageSize = value;
    }

    public int Page
    {
        get => _page < 0 ? _page : 1;
        set => _page = value;
    }

    public string? SearchingWord { get; set; }

    public string SortType
    {
        get => _sortType ?? "ASC";
        set => _sortType = value;
    }

    public string SortBy
    {
        get => _sortBy ?? "Id";
        set => _sortBy = value;
    }

    public bool HasSearch => !string.IsNullOrEmpty(SearchingWord);
    public bool HasSort => !string.IsNullOrEmpty(SortBy);
}
