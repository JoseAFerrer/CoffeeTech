using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public partial class UseCases
{
    public static void UC4_ExchangeCompleteCollections() // not working as expected
    {
        Quick.InsertUseCaseTitle(4);
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();

        var completeSagasInA = bookshopA.Sagas.Where(x => x.IsComplete).ToArray();
        var completeSagasInB = bookshopB.Sagas.Where(x => x.IsComplete).ToArray();
        
        // Note: this does not work since they are complex objects & we are not using a custom EqualityComparer
        // var test = completeSagasInB.Except(completeSagasInA).ToArray();
        
        var completeInANotCompleteInB = completeSagasInA
            .ExceptBy(completeSagasInB.Select(s => s.Id), s => s.Id).ToArray();
        var completeInBNotCompleteInA = completeSagasInB
            .ExceptBy(completeSagasInA.Select(s => s.Id), s => s.Id).ToArray();
        
        var updatedBooksInA = bookshopA.Sagas
            .Except(completeInANotCompleteInB)
            .Concat(completeInBNotCompleteInA).ToList();
        bookshopA.Sagas = updatedBooksInA;
        
        var updatedBooksInB = bookshopB.Sagas
            .Except(completeInBNotCompleteInA)
            .Concat(completeInANotCompleteInB).ToList();
        bookshopB.Sagas = updatedBooksInB;
        
        BookshopService.SaveBookshops(bookshopA, bookshopB);

        Console.WriteLine("Calling use case 2 to see if numbers are reversed:");
        UC2_WhoHasMoreCompleteSagasOutOfTheCommonOnes();
    }
}