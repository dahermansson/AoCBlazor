using Microsoft.Toolkit.HighPerformance;

namespace AoC.Solvers.Y2016;

public class Day08(string input) : IDay
{
    public string Output => output;

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        Display = new bool[6,50];
        SwipeCard();
        return Display.Cast<bool>().Count(p => p);
    }

    private string output { get; set; } =string.Empty;

    public int Star2()
    {
        Display = new bool[6,50];
        SwipeCard();
        var a = Display.AsSpan2D().Output().ToArray();
        output = string.Concat(a);
        return -1;
    }

    private void SwipeCard()
    {
        Span2D<bool> display = Display;
        foreach (var i in Input)
        {
            if(i.StartsWith("rect"))
            {
                var rect = i.Split(" ").Last().Split("x").Select(t => int.Parse(t)).ToArray();
                display.Slice(0,0, rect[1], rect[0]);
                display.Slice(0,0, rect[1], rect[0]).Fill(true);
            }
            else if(i.Contains("row"))
            {
                var row = i.Split("=").Last().Split(" ");
                ShiftRow(display, int.Parse(row[0]), int.Parse(row[2]));
            }
            else
            {
                var col = i.Split("=").Last().Split(" ");
                ShiftCol(display, int.Parse(col[0]), int.Parse(col[2]));
            }
        }
    }

    private bool[,] Display { get; set; } = default!;

    private void ShiftCol(Span2D<bool> display, int col, int shift)
    {
        Span<bool> p = display.GetColumn(col).ToArray();
        var n = Shift(p, shift);
        n.TryCopyTo(display.GetColumn(col));
    }
    private void ShiftRow(Span2D<bool> display, int row, int shift)
    {
        Span<bool> p = display.GetRow(row).ToArray();
        var n = Shift(p, shift);
        n.TryCopyTo(display.GetRow(row));
    }

    private Span<bool> Shift(Span<bool> span, int shift)
    {
        var s = shift % span.Length;
        var res = span.ToArray();
        for(int i = 0; i< span.Length; i++)
            res[(i+s) % res.Length] = span[i];   
        return res; 
    }
}
