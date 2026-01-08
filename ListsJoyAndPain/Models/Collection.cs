namespace ListsJoyAndPain.Models;

public class Collection
{
    public Collection()
    {
        
    }
    public Collection(List<Book> books, int completeSize)
    {
        Books = books;
        CompleteSize = completeSize;
    }
    public List<string>? AuthorIds { get; set; }
    public List<Book> Books { get; set; }
    public int Size => Books.Count;
    public int CompleteSize { get; set; }
    public List<Book> GetFirstBooks(int x) => Books.Take(x).ToList();
    public bool IsComplete => Size == CompleteSize;
}