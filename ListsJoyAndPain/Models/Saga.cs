namespace ListsJoyAndPain.Models;

public class Saga
{
    public Saga()
    {
        
    }
    public Saga(string sagaName, List<Book> books, List<string> authorIds)
    {
        Id = Guid.NewGuid().ToString();
        Name = sagaName;
        Books = books;
        CompleteSize = books.Count;
        AuthorIds = authorIds;
    }
    
    public Saga(Saga parentSaga, int takeFirstBooks)
    {
        Id = parentSaga.Id;
        Name = parentSaga.Name;
        Books = parentSaga.GetFirstBooks(takeFirstBooks);
        CompleteSize = parentSaga.CompleteSize;
        AuthorIds = parentSaga.AuthorIds;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> AuthorIds { get; set; } = [];
    public List<Book> Books { get; set; }
    public int CompleteSize { get; set; }
    public int Size => Books.Count;
    public List<Book> GetFirstBooks(int x) => Books.Take(x).ToList();
    public bool IsComplete => Size == CompleteSize;
}