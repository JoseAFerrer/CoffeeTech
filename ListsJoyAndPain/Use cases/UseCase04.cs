using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Models;
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
        
        var (completeInANotCompleteInB,
            completeInBNotCompleteInA,
            incompleteToRemoveFromBAndAddToA,
            incompleteToRemoveFromAAndAddToB) = BuildSetsToWork(completeSagasInA, completeSagasInB, bookshopB, bookshopA);

        UpdateBookshops(bookshopA, completeInANotCompleteInB, incompleteToRemoveFromAAndAddToB, completeInBNotCompleteInA, incompleteToRemoveFromBAndAddToA, bookshopB);

        BookshopService.SaveBookshops(bookshopA, bookshopB);

        Console.WriteLine("Calling use case 2 to see if numbers are reversed:");
        UC2_WhoHasMoreCompleteSagasOutOfTheCommonOnes();
    }

    private static (Saga[] completeInANotCompleteInB,
        Saga[] completeInBNotCompleteInA,
        Saga[] incompleteToRemoveFromBAndAddToA,
        Saga[] incompleteToRemoveFromAAndAddToB) BuildSetsToWork(Saga[] completeSagasInA,
            Saga[] completeSagasInB, Models.Bookshop bookshopB, Models.Bookshop bookshopA)
    {
        // Note: this does not work since they are complex objects & we are not using a custom EqualityComparer
        // var completeInBNotCompleteInA = completeSagasInB.Except(completeSagasInA).ToArray();
        
        
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
        return (completeInANotCompleteInB, completeInBNotCompleteInA, incompleteToRemoveFromBAndAddToA, incompleteToRemoveFromAAndAddToB);
    }

    private static void UpdateBookshops(Models.Bookshop bookshopA,
        Saga[] completeInANotCompleteInB,
        Saga[] incompleteToRemoveFromAAndAddToB,
        Saga[] completeInBNotCompleteInA,
        Saga[] incompleteToRemoveFromBAndAddToA,
        Models.Bookshop bookshopB)
    {
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
    }
}