using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Setup;

namespace ListsJoyAndPain.Use_cases;

public static partial class UseCases
{
    public static void UC1_WhoHasMoreBooks()
    {
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();

        var bookCountA = bookshopA.Sagas.Sum(x => x.Books.Count);
        var bookCountB = bookshopB.Sagas.Sum(x => x.Books.Count);

        Console.WriteLine("Bookshop A has {0} books.", bookCountA);
        Console.WriteLine("Bookshop B has {0} books.", bookCountB);
        Console.WriteLine(bookCountA > bookCountB ? "Bookshop A has more books!" : "Bookshop B has more books!");
    }
}