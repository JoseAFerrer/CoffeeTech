using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public partial class UseCases
{
    public static void UC3_WhoHasMoreBooksInCommonSagas()
    {
        Quick.InsertUseCaseTitle(3);
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();
        var intersection = bookshopA.Sagas
            .Select(s => s.Id)
            .IntersectBy<string, string>(bookshopB.Sagas
                .Select(s => s.Id), x => x)
            .ToArray();

        var amountOfBooksInCommonSagasInBookshopA = bookshopA.Sagas
            .Where(s => intersection.Contains(s.Id))
            .ToDictionary(k => k.Id, v => v.Size);
        
        var amountOfBooksInCommonSagasInBookshopB = bookshopB.Sagas
            .Where(s => intersection.Contains(s.Id))
            .ToDictionary(k => k.Id, v => v.Size);
        
        var sagasWhereAHasMoreThanB = intersection.Select(x =>
        {
            var countA = amountOfBooksInCommonSagasInBookshopA[x];
            var countB = amountOfBooksInCommonSagasInBookshopB[x];
            return countA > countB;
        })
        .Count(x => x is true);
        
        var sagasWhereBHasMoreThanA = intersection.Length - sagasWhereAHasMoreThanB;
        
        Console.WriteLine("Bookshop A has more books in {0} common collections.", sagasWhereAHasMoreThanB);
        Console.WriteLine("Bookshop B has more books in {0} common collections.", sagasWhereBHasMoreThanA);
        Console.WriteLine(
            sagasWhereAHasMoreThanB > sagasWhereBHasMoreThanA
                ? "Bookshop A has more books in common collections!"
                : "Bookshop B has more books in common collections!");
    }
}