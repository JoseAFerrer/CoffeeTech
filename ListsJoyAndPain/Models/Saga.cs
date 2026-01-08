namespace ListsJoyAndPain.Models;

public class Saga
{
    public Saga()
    {
        
    }
    public Saga(List<Book> books, List<string> authorIds)
    {
        Books = books;
        CompleteSize = books.Count;
        AuthorIds = authorIds;
    }
    
    public Saga(Saga parentSaga, int takeFirstBooks)
    {
        Books = parentSaga.GetFirstBooks(takeFirstBooks);
        CompleteSize = parentSaga.CompleteSize;
        AuthorIds = parentSaga.AuthorIds;
    }

    public List<string> AuthorIds { get; set; } = [];
    public List<Book> Books { get; set; }
    public int CompleteSize { get; set; }
    public int Size => Books.Count;
    public List<Book> GetFirstBooks(int x) => Books.Take(x).ToList();
    public bool IsComplete => Size == CompleteSize;
}