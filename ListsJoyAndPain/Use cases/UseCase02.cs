using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Models;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public static partial class UseCases
{
    public static void UC2_WhoHasMoreCompleteSagasOutOfTheCommonOnes()
    {
        Quick.InsertUseCaseTitle(2);
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();
        var intersection = bookshopA.Sagas
            .Select(s => s.Id)
            .IntersectBy<string, string>(bookshopB.Sagas
                .Select(s => s.Id), x => x);

        var commonSagasCompleteInBookshopA = bookshopA.Sagas
            .Where(s => intersection.Contains(s.Id))
            .Count(s => s.IsComplete);
        
        var commonSagasCompleteInBookshopB = bookshopB.Sagas
            .Where(s => intersection.Contains(s.Id))
            .Count(s => s.IsComplete);
        
        Console.WriteLine("Bookshop A has {0} complete collections out of the common.", commonSagasCompleteInBookshopA);
        Console.WriteLine("Bookshop B has {0} complete collections out of the common.", commonSagasCompleteInBookshopB);
        Console.WriteLine(commonSagasCompleteInBookshopA > commonSagasCompleteInBookshopB ? "Bookshop A has more complete collections!" : "Bookshop B has more complete collections!");
    }
}