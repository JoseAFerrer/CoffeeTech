namespace ListsJoyAndPain.Models;

public class Collection
{
    public Collection()
    {
        
    }
    public Collection(List<Book> books, bool isComplete)
    {
        Books = books;
        IsComplete = isComplete;
    }
    public List<string>? AuthorIds { get; set; }
    public List<Book> Books { get; set; }
    public int Size => Books.Count;
    public List<Book> GetFirstBooks(int x) => Books.Take(x).ToList();
    public bool IsComplete { get; set; }
}