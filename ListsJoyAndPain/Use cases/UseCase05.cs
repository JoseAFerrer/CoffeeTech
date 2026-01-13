using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public partial class UseCases
{
    public static void UC5_WhoHasMoreOfDifferentAuthors()
    {
        Quick.InsertUseCaseTitle(5);
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();
        
        var authorIds = bookshopA
            .Sagas.SelectMany(x => x.AuthorIds)
            .Concat(bookshopB.Sagas.SelectMany(x => x.AuthorIds))
            .Distinct()
            .ToArray();

        var dictOfAuthors = new Dictionary<string, (int a, int b)>();
        foreach (var authorId in authorIds)
        {
            var countA = bookshopA.Sagas
                .Where(x => x.AuthorIds.Contains(authorId))
                .Sum(x => x.Size);
            var countB = bookshopB.Sagas
                .Where(x => x.AuthorIds.Contains(authorId))
                .Sum(x => x.Size);
            dictOfAuthors.Add(authorId, (countA, countB));
        }
        
        var authorsWhereAHasMoreBooks = dictOfAuthors.Count(pair => pair.Value.a > pair.Value.b);
        var authorsWhereBHasMoreBooks = dictOfAuthors.Count(pair => pair.Value.a < pair.Value.b);
        Console.WriteLine($"Bookshop A has more books for {authorsWhereAHasMoreBooks} common authors");
        Console.WriteLine($"Bookshop B has more books for {authorsWhereBHasMoreBooks} common authors");
   }
}