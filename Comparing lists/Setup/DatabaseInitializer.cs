using CrypticWizard.RandomWordGenerator;

namespace Comparing_lists.Setup;

public static class DatabaseInitializer
{
    public static void GenerateAllBooks()
    {
        var myWordGenerator = new WordGenerator();
        var pattern = new List<WordGenerator.PartOfSpeech>
        {
            WordGenerator.PartOfSpeech.art,
            WordGenerator.PartOfSpeech.adv,
            WordGenerator.PartOfSpeech.adj,
            WordGenerator.PartOfSpeech.adj,
            WordGenerator.PartOfSpeech.noun
        };
        
        var bookTitles = myWordGenerator
                .GetPatterns(pattern, ' ', 1000)
                .Select(x => string.Concat(Guid.NewGuid().ToString().Remove(13), "/", x))
                .ToList();
        
        File.AppendAllLines(Constants.BookListPath, bookTitles);
    }
    
    public static void GenerateAllCollections()
    {
        var bookTitles = File.ReadAllLines(Constants.BookListPath);
        
        var myWordGenerator = new WordGenerator();
    }
}