namespace ListsJoyAndPain.Models;

public class Author
{
    public Author(string name, string bornIn, string publishedIn)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        BornIn = bornIn;
        PublishedIn = publishedIn;
    }
    public string Id { get; set; }
    public string Name { get; set; }
    public string BornIn { get; set; }
    public string PublishedIn { get; set; }
}