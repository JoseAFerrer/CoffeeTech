namespace Comparing_lists.Models;

public class Book
{
    public Book()
    {
        
    }
    
    public Book(string id, string title)
    {
        Id = id;
        Title = title;
    }
    public string Id { get; set; }
    public string Title { get; set; }
}