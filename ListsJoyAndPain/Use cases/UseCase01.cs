using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Setup;

namespace ListsJoyAndPain.Use_cases;

public static class UseCase01
{
    public static void WhoHasMoreBooks()
    {
        var bookshopA = Database.GetBookshop(Constants.BookshopA);
        var bookshopB = Database.GetBookshop(Constants.BookshopB);

        var bookCountA = bookshopA.Collections.Sum(x => x.Books.Count);
        var bookCountB = bookshopB.Collections.Sum(x => x.Books.Count);

        Console.WriteLine(bookCountA > bookCountB ? "Bookshop A has more books!" : "Bookshop B has more books!");
    }
}