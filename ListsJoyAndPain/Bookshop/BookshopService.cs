using ListsJoyAndPain.Setup;

namespace ListsJoyAndPain.Bookshop;

public static class BookshopService
{
    public static (Models.Bookshop bookshopA, Models.Bookshop bookshopB) GetBookshops()
    {
        var bookshopA = Database.GetBookshop(Constants.BookshopA);
        var bookshopB = Database.GetBookshop(Constants.BookshopB);
        return (bookshopA, bookshopB);
    }
    
    public static void SaveBookshops(Models.Bookshop bookshopA, Models.Bookshop bookshopB)
    {
        Database.SaveBookshop(Constants.BookshopA, bookshopA);
        Database.SaveBookshop(Constants.BookshopB, bookshopB);
    }
}