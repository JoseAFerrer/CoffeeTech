namespace ListsJoyAndPain.Utilities;

public static class RandomExtensions
{
    public static bool NextBoolean(this Random random)
    {
        return random.Next(0, 101) > 50;
    }
    
    public static int NextOneToTen(this Random random)
    {
        return random.Next(1, 11);
    }
    
    public static int NextZeroToTen(this Random random)
    {
        return random.Next(0, 11);
    }
    
    public static int NextPercentage(this Random random)
    {
        return random.Next(0, 101);
    }
}