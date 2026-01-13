namespace ListsJoyAndPain.Utilities;

public static class Quick
{
    public static void InsertUseCaseTitle(int useCase)
    {
        InsertLineBreak();
        Console.WriteLine($"USE CASE {useCase}");
    }
    
    public static void InsertLineBreak()
    {
        Console.WriteLine("\r");
    }
}