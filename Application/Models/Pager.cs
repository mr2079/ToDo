namespace Application.Models;

public class PagedResult<TItem>
{
    public PagedResult(
        IEnumerable<TItem> items,
        int totalItems,
        int currentPage = 1,
        int pageSize = 10,
        int maxPages = 10)
    {
        if (pageSize < 1)
        {
            pageSize = 10;
        }

        if (totalItems > 0 && pageSize > totalItems)
        {
            pageSize = totalItems;
        }

        var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

        if (totalItems == 0)
        {
            pageSize = 0;
        }

        if (currentPage < 1)
        {
            currentPage = 1;
        }
        else if (currentPage > totalPages)
        {
            currentPage = totalPages;
        }

        int startPage, endPage;
        if (totalPages <= maxPages)
        {
            startPage = 1;
            endPage = totalPages;
        }
        else
        {
            var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
            var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
            if (currentPage <= maxPagesBeforeCurrentPage)
            {
                startPage = 1;
                endPage = maxPages;
            }
            else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
            {
                startPage = totalPages - maxPages + 1;
                endPage = totalPages;
            }
            else
            {
                startPage = currentPage - maxPagesBeforeCurrentPage;
                endPage = currentPage + maxPagesAfterCurrentPage;
            }
        }

        var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);

        Items = items;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = totalPages;
        StartPage = startPage;
        EndPage = endPage;
        Pages = pages;
    }

    public IEnumerable<TItem>? Items { get; private set; }
    public int TotalItems { get; private set; }
    public int CurrentPage { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public int StartPage { get; private set; }
    public int EndPage { get; private set; }
    public IEnumerable<int> Pages { get; private set; }
}
