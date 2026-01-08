namespace Comparing_lists.Models;

public class Collection
{
    public Collection(List<Book> books)
    {
        Books = books;
    }
    public List<string>? AuthorIds { get; set; }
    public List<Book> Books { get; set; }
    public bool IsComplete { get; set; }
}