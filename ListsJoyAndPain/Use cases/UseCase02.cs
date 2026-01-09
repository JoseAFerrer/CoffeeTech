using ListsJoyAndPain.Bookshop;

namespace ListsJoyAndPain.Use_cases;

public static partial class UseCases
{
    public static void UC2_WhoHasMoreBooksOfTheCommonCollections()
    {
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();
        
        
    }
}