using Comparing_lists.Models;
using CrypticWizard.RandomWordGenerator;

namespace Comparing_lists.Setup;

public static class DatabaseInitializer
{
    public static void ResetBooks()
    {
        File.Delete(Constants.BookPath);
        const int numberOfBooks = 1000;
        var books = BookService.GetBookTitles(numberOfBooks)
                .Select(x => new Book(Guid.NewGuid().ToString().Remove(13),x))
                .ToList();
        
        Database.SaveAllBooks(books);
    }

    public static void GenerateAllCollections()
    {
        var books = Database.GetBooksFromDb();
        
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