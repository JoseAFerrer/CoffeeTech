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
    /// Generates all books and sagas from scratch
    /// </summary>
    public static void ResetBookDatabase()
    {
        if (File.Exists(Constants.BookPath)) File.Delete(Constants.BookPath);
        if (File.Exists(Constants.SagaPath)) File.Delete(Constants.SagaPath);
        
        GenerateAllBooks();
        GenerateBaseSagas();
    }

    /// <summary>
    /// Generates bookshops from scratch, using sagas
    /// </summary>
    public static void ResetBookshops()
    {
        if (File.Exists(Constants.BookshopA)) File.Delete(Constants.BookshopA);
        if (File.Exists(Constants.BookshopB)) File.Delete(Constants.BookshopB);
        GenerateBookshopsBasedOnSagas();
    }

    private static void GenerateBookshopsBasedOnSagas()
    {
        var bookshopA = new Models.Bookshop();
        var bookshopB = new Models.Bookshop();
        var sagas = Database.GetSagas();
        var random = new Random();
        foreach (var collection in sagas)
        {
            if (random.NextBoolean()) bookshopA.AddPartialCollectionToBookshop(collection, random);
            if (random.NextBoolean()) bookshopB.AddPartialCollectionToBookshop(collection, random);
        }
        
        Database.SaveBookshop(Constants.BookshopA, bookshopA);
        Database.SaveBookshop(Constants.BookshopB, bookshopB);
    }

    private static void AddPartialCollectionToBookshop(this Models.Bookshop bookshop, Saga saga, Random random)
    {
        var partialCollectionIndex = Math.Min(saga.Size, random.NextOneToTen());
        var collectionToAdd = new Saga(saga, partialCollectionIndex);
        bookshop.Sagas.Add(collectionToAdd);
    }

    private static void GenerateAllBooks()
    {
        const int numberOfBooks = 1000;
        var books = BookService.GetBookTitles(numberOfBooks)
            .Select(x => new Book(Guid.NewGuid().ToString().Remove(13),x))
            .ToList();
        
        Database.SaveAllBooks(books);
    }
    
    private static void GenerateBaseSagas()
    {
        var books = Database.GetBooks();
        var authors = Database.GetAuthors().ToArray();
        
        var random = new Random();
        var baseSagas = new List<Saga>();
        var safetyCounter = 0;
        while (books.Count > 0 && safetyCounter < 1000)
        {
            var bookAmount = Math.Min(random.NextOneToTen(), books.Count);
            var booksForSaga = books.Take(bookAmount).ToList();

            var authorIndex = random.Next(0, authors.Length);
            var authorIdForSaga = authors[authorIndex].Id;

            var sagaName = BookService.GetSagaName();
            
            baseSagas.Add(new Saga(sagaName, booksForSaga, [authorIdForSaga]));
            books.RemoveRange(0, bookAmount);
            safetyCounter++;
        }
        
        Database.SaveSagas(baseSagas);
    }
}