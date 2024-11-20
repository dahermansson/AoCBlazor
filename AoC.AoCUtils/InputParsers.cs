namespace AoC.AoCUtils;

public static class InputParsers
{
    public static string[] GetInputLines(string input) => input.Split(Environment.NewLine);
    public static T[] GetInputLines<T>(string input) => GetInputLines(input).Select( s => (T) Convert.ChangeType(s, typeof(T))).ToArray();
}