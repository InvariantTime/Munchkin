namespace Example;

public static class ConsoleDrawer
{
    public static void Draw(string text, ConsoleColor color = ConsoleColor.White)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = prevColor;
    }
}
