using ListsJoyAndPain.Bookshop;
using ListsJoyAndPain.Utilities;

namespace ListsJoyAndPain.Use_cases;

public partial class UseCases
{
    public static void UC6_WhoHasMoreAndOrderTheOnesWeDoNotHave()
    {
        Quick.InsertUseCaseTitle(6);
        var (bookshopA, bookshopB) = BookshopService.GetBookshops();
        
        var authorIds = bookshopA
            .Sagas.SelectMany(x => x.AuthorIds)
            .Concat(bookshopB.Sagas.SelectMany(x => x.AuthorIds))
            .Distinct()
            .ToArray();

        var dictOfAuthors = new Dictionary<string, (int a, int b, string[] missingBooks)>();
        foreach (var authorId in authorIds)
        {
            var sagasFromAuthorInA = bookshopA.Sagas
                .Where(x => x.AuthorIds.Contains(authorId)).ToArray();
            var countA = sagasFromAuthorInA.Sum(x => x.Size);

            var sagasFromAuthorInB = bookshopB.Sagas
                .Where(x => x.AuthorIds.Contains(authorId)).ToArray();
            var countB = sagasFromAuthorInB.Sum(x => x.Size);
            
            var missingBookIds = countA > countB 
                ? [] 
                : sagasFromAuthorInB.SelectMany(s => s.Books.Select(b => b.Id))
                    .Except(sagasFromAuthorInA.SelectMany(s => s.Books.Select(b => b.Id)))
                    .ToArray();
            
            dictOfAuthors.Add(authorId, (countA, countB, missingBookIds));
        }
        
        var authorsWhereAHasMoreBooks = dictOfAuthors.Count(pair => pair.Value.a > pair.Value.b);
        var authorsWhereBHasMoreBooks = dictOfAuthors.Count(pair => pair.Value.a < pair.Value.b);
        Console.WriteLine($"Bookshop A has more books for {authorsWhereAHasMoreBooks} common authors");
        Console.WriteLine($"Bookshop B has more books for {authorsWhereBHasMoreBooks} common authors");
        Console.WriteLine($"Bookshop A needs to buy {dictOfAuthors.Values.Sum(x => x.missingBooks.Length)} books");
   }
}