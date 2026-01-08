using ListsJoyAndPain.Models;

namespace ListsJoyAndPain.Setup;

public static class DatabaseInitializer
{
    /// <summary>
    /// Generates all books and collections from scratch
    /// </summary>
    public static void ResetBookDatabase()
    {
        if (File.Exists(Constants.BookPath)) File.Delete(Constants.BookPath);
        if (File.Exists(Constants.CollectionPath)) File.Delete(Constants.CollectionPath);
        
        GenerateAllBooks();
        GenerateAllCollections();
    }

    public static void ResetBookshops()
    {
        if (File.Exists(Constants.BookshopA)) File.Delete(Constants.BookshopA);
        if (File.Exists(Constants.BookshopB)) File.Delete(Constants.BookshopB);
        GenerateBookshopsBasedOnCollections();
    }

    private static void GenerateBookshopsBasedOnCollections()
    {
        var bookshopA = new Bookshop();
        var bookshopB = new Bookshop();
        var collections = Database.GetCollections();
        var random = new Random();
        foreach (var collection in collections)
        {
            var bookshopAHasCollection = random.Next(0, 100);
            var bookshopBHasCollection = random.Next(0, 100);

            if (bookshopAHasCollection is > 50) bookshopA.Collections.Add(collection);
            if (bookshopBHasCollection is > 50) bookshopB.Collections.Add(collection);
        }
        
        Database.SaveBookshop(Constants.BookshopA, bookshopA);
        Database.SaveBookshop(Constants.BookshopB, bookshopB);
    }

    private static void GenerateAllBooks()
    {
        const int numberOfBooks = 1000;
        var books = BookService.GetBookTitles(numberOfBooks)
            .Select(x => new Book(Guid.NewGuid().ToString().Remove(13),x))
            .ToList();
        
        Database.SaveAllBooks(books);
    }
    
    private static void GenerateAllCollections()
    {
        var books = Database.GetBooks();
        
        var random = new Random();
        var collections = new List<Collection>();
        var safetyCounter = 0;
        while (books.Count > 0 && safetyCounter < 1000)
        {
            var collectionAmount = Math.Min(random.Next(1, 10), books.Count);
            var booksForCollection = books.Take(collectionAmount).ToList();
            collections.Add(new Collection(booksForCollection));
            books.RemoveRange(0, collectionAmount);
            safetyCounter++;
        }
        
        Database.SaveCollections(collections);
    }
}