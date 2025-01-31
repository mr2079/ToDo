namespace Application.Queries;

public abstract class SearchQuery
{
    public Page Page { get; set; } = new();
}

public class Page
{
    public int Number { get; set; }
    public int Size { get; set; }
}