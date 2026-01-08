using CrypticWizard.RandomWordGenerator;

namespace ListsJoyAndPain.Setup;

public static class BookService
{
    public static List<string> GetBookTitles(int numberOfBooks)
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
            .GetPatterns(pattern, ' ', numberOfBooks);
        return bookTitles;
    }
    
    public static string GetSagaName()
    {
        var myWordGenerator = new WordGenerator();
        var pattern = new List<WordGenerator.PartOfSpeech>
        {
            WordGenerator.PartOfSpeech.art,
            WordGenerator.PartOfSpeech.adj,
            WordGenerator.PartOfSpeech.noun
        };

        var sagaName = myWordGenerator
            .GetPattern(pattern, ' ');
        return sagaName;
    }
}