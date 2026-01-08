namespace ListsJoyAndPain.Models;

public class Collection
{
    public Collection()
    {
        
    }
    public Collection(List<Book> books, List<string> authorIds)
    {
        Books = books;
        CompleteSize = books.Count;
        AuthorIds = authorIds;
    }
    
    public Collection(Collection parentCollection, int takeFirstBooks)
    {
        Books = parentCollection.GetFirstBooks(takeFirstBooks);
        CompleteSize = parentCollection.CompleteSize;
        AuthorIds = parentCollection.AuthorIds;
    }

    public List<string> AuthorIds { get; set; } = [];
    public List<Book> Books { get; set; }
    public int CompleteSize { get; set; }
    public int Size => Books.Count;
    public List<Book> GetFirstBooks(int x) => Books.Take(x).ToList();
    public bool IsComplete => Size == CompleteSize;
}