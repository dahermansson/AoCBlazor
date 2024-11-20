namespace AoC.Solvers.Y2016;

public class Day21(string input) : IDay
{
    public string Output => _output;
    private string _output {get; set; } = string.Empty;

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var pwd = "abcdefgh".ToCharArray();

        void Parse(string input)
        {
            var parts = input.Split(" ");
            if (input.StartsWith("swap position"))
                SwapPosition(ref pwd, int.Parse(parts[2]), int.Parse(parts[5]));
            else if (input.StartsWith("swap letter"))
                SwapLetter(ref pwd, parts[2][0], parts[5][0]);
            else if (input.StartsWith("reverse"))
                Reverse(ref pwd, int.Parse(parts[2]), int.Parse(parts[4]));
            else if (input.StartsWith("rotate based"))
                RotateOfLetterXPosition(ref pwd, parts.Last()[0]);
            else if (input.StartsWith("rotate left"))
                RotateLeft(ref pwd, int.Parse(parts[2]));
            else if (input.StartsWith("rotate right"))
                RotateRight(ref pwd, int.Parse(parts[2]));
            else if (input.StartsWith("move"))
                Move(ref pwd, int.Parse(parts[2]), int.Parse(parts[5]));
        }

        Input.ToList().ForEach(Parse);

        _output = string.Concat(pwd);
        return -1;
    }

    public int Star2()
    {
        var pwd = "fbgdceah".ToCharArray();

        void Parse(string input)
        {
            var parts = input.Split(" ");
            if (input.StartsWith("swap position"))
                SwapPosition(ref pwd, int.Parse(parts[2]), int.Parse(parts[5]));
            else if (input.StartsWith("swap letter"))
                SwapLetter(ref pwd, parts[2][0], parts[5][0]);
            else if (input.StartsWith("reverse"))
                Reverse(ref pwd, int.Parse(parts[2]), int.Parse(parts[4]));
            else if (input.StartsWith("rotate based"))
                RotateOfLetterXPositionReverse(ref pwd, parts.Last()[0]);
            else if (input.StartsWith("rotate left"))
                RotateRight(ref pwd, int.Parse(parts[2]));
            else if (input.StartsWith("rotate right"))
                RotateLeft(ref pwd, int.Parse(parts[2]));
            else if (input.StartsWith("move"))
                Move(ref pwd, int.Parse(parts[5]), int.Parse(parts[2]));
        }
        Input.Reverse().ToList().ForEach(Parse);
        _output = string.Concat(pwd);
        return -1;
    }

    private static void SwapPosition(ref char[] array, int x, int y)
    {
        (array[y], array[x]) = (array[x], array[y]);
    }

    private static void SwapLetter(ref char[] array, char x, char y)
    {
        SwapPosition(ref array, array.ToList().IndexOf(x), array.ToList().IndexOf(y));
    }

    private static void RotateOfLetterXPosition(ref char[] array, char x)
    {
        var index = array.ToList().IndexOf(x);
        int[] steps = [1, 2, 3, 4, 6, 7, 0, 1];
        RotateRight(ref array, steps[index]);
    }

    private static void RotateOfLetterXPositionReverse(ref char[] array, char x)
    {
        var index = array.ToList().IndexOf(x);
        int[] steps = [7, 7, 2, 6, 1, 5, 0, 4];
        RotateRight(ref array, steps[index]);
    }

    private static void Move(ref char[] array, int x, int y)
    {
        var list = array.ToList();
        var move = list[x];
        list.Remove(move);
        list.Insert(y, move);
        array = [.. list];
    }

    private static void Reverse(ref char[] array, int x, int y)
    {
        var span1 = new Span<char>(array, 0, x);
        var revSpan = new Span<char>(array, x, y - x + 1);
        var endSpan = new Span<char>(array, y + 1, array.Length - (y + 1));
        revSpan.Reverse();
        array = [.. span1, .. revSpan, .. endSpan];
    }

    private static void RotateRight(ref char[] array, int steps)
    {
        for (int i = 0; i < steps; i++)
            RotateRight(ref array);
    }

    private static void RotateLeft(ref char[] array, int steps)
    {
        for (int i = 0; i < steps; i++)
            RotateLeft(ref array);
    }

    private static void RotateRight(ref char[] array)
    {
        List<char> temp = [.. array.SkipLast(1)];
        array = [array.Last(), .. temp];
    }

    private static void RotateLeft(ref char[] array)
    {
        List<char> temp = [.. array.Skip(1)];
        array = [.. temp, array.First()];
    }
}
