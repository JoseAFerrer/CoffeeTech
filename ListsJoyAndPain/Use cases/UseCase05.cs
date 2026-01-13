using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public partial class UseCases
{
    public static void UC5_WhoHasMoreOfDifferentAuthors()
    {
        Quick.InsertUseCaseTitle(5);
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();
    }
}