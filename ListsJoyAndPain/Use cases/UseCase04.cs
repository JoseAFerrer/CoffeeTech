using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public partial class UseCases
{
    public static void UC4_ExchangeCompleteCollections()
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

        var incompleteToRemoveFromBAndAddToA = bookshopB.Sagas
            .Where(x => completeInANotCompleteInB.Select(y => y.Id)
                .Contains(x.Id)).ToArray();

        var incompleteToRemoveFromAAndAddToB = bookshopA.Sagas
            .Where(x => completeInBNotCompleteInA.Select(y => y.Id)
                .Contains(x.Id)).ToArray();
        
        var updatedBooksInA = bookshopA.Sagas
            .Except(completeInANotCompleteInB)
            .Except(incompleteToRemoveFromAAndAddToB)
            .Concat(completeInBNotCompleteInA)
            .Concat(incompleteToRemoveFromBAndAddToA)
            .ToList();
        
        bookshopA.Sagas = updatedBooksInA;
        
        var updatedBooksInB = bookshopB.Sagas
            .Except(completeInBNotCompleteInA)
            .Except(incompleteToRemoveFromBAndAddToA)
            .Concat(completeInANotCompleteInB)
            .Concat(incompleteToRemoveFromAAndAddToB).ToList();
        bookshopB.Sagas = updatedBooksInB;
        
        BookshopService.SaveBookshops(bookshopA, bookshopB);

        Console.WriteLine("Calling use case 2 to see if numbers are reversed:");
        UC2_WhoHasMoreCompleteSagasOutOfTheCommonOnes();
    }
}