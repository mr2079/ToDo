namespace Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Page<T>(
        this IQueryable<T> queryable,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var skip = Skip(pageNumber, pageSize);

        return queryable.Skip(skip).Take(pageSize);
    }

    public static IQueryable<T> Page<T>(
        this IEnumerable<T> queryable,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var skip = Skip(pageNumber, pageSize);

        return queryable.Skip(skip).Take(pageSize).AsQueryable();
    }

    private static int Skip(int page, int take)
        => (page - 1) * take;
}