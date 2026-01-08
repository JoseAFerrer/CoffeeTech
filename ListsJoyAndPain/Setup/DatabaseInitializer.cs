using ListsJoyAndPain.Models;
using ListsJoyAndPain.Utilities;
using RandomNameGeneratorLibrary;

namespace ListsJoyAndPain.Setup;

public static class DatabaseInitializer
{
    /// <summary>
    /// Generates all authors from scratch
    /// </summary>
    public static void ResetAuthors()
    {
        if (File.Exists(Constants.AuthorPath)) File.Delete(Constants.AuthorPath);

        GenerateAllAuthors();
    }

    private static void GenerateAllAuthors()
    {
        const int numberOfPlaces = 4;
        var placeGenerator = new PlaceNameGenerator();
        var places = placeGenerator.GenerateMultiplePlaceNames(numberOfPlaces).ToArray();
        
        var personGenerator = new PersonNameGenerator();
        var random = new Random();
        var authors = new List<Author>();
        for (var i = 0; i < 40; i++)
        {
            var name = personGenerator.GenerateRandomFirstAndLastName();
            var bornIn = random.Next(1, numberOfPlaces);
            var publishedIn = random.Next(bornIn, numberOfPlaces);
            authors.Add(new Author(name, places[bornIn], places[publishedIn]));
        }
        
        Database.SaveAuthors(authors);
    }

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

    /// <summary>
    /// Generates bookshops from scratch, using collections
    /// </summary>
    public static void ResetBookshops()
    {
        if (File.Exists(Constants.BookshopA)) File.Delete(Constants.BookshopA);
        if (File.Exists(Constants.BookshopB)) File.Delete(Constants.BookshopB);
        GenerateBookshopsBasedOnCollections();
    }

    private static void GenerateBookshopsBasedOnCollections()
    {
        var bookshopA = new Models.Bookshop();
        var bookshopB = new Models.Bookshop();
        var collections = Database.GetCollections();
        var random = new Random();
        foreach (var collection in collections)
        {
            if (random.NextBoolean()) bookshopA.AddPartialCollectionToBookshop(collection, random);
            if (random.NextBoolean()) bookshopB.AddPartialCollectionToBookshop(collection, random);
        }
        
        Database.SaveBookshop(Constants.BookshopA, bookshopA);
        Database.SaveBookshop(Constants.BookshopB, bookshopB);
    }

    private static void AddPartialCollectionToBookshop(this Models.Bookshop bookshop, Collection collection, Random random)
    {
        var partialCollectionIndex = Math.Min(collection.Size, random.NextOneToTen());
        var booksToAddToPartialCollection = collection.GetFirstBooks(partialCollectionIndex);
        var collectionToAdd = new Collection(booksToAddToPartialCollection, collection.Size);
        bookshop.Collections.Add(collectionToAdd);
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
            var collectionAmount = Math.Min(random.NextOneToTen(), books.Count);
            var booksForCollection = books.Take(collectionAmount).ToList();
            collections.Add(new Collection(booksForCollection, booksForCollection.Count));
            books.RemoveRange(0, collectionAmount);
            safetyCounter++;
        }
        
        Database.SaveCollections(collections);
    }
}