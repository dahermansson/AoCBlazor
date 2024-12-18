using System.Security.Cryptography.X509Certificates;

namespace AoC.Solvers;

public class Day18 : IDay
{
    public Day18(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; }

    record Boundce(int YMin, int YMax, int XMin = 0, int XMax = 0)
    {
        public bool InBoundce((int X, int Y) pos) => pos.X >= XMin && pos.X <= XMax && pos.Y >= YMin && pos.Y <= YMax;
    }

    public int Star1()
    {
        Input = """
        5,4
        4,2
        4,5
        3,0
        2,1
        6,3
        2,4
        1,5
        0,6
        3,3
        2,6
        5,1
        1,2
        5,5
        2,5
        6,5
        1,4
        0,4
        6,4
        1,1
        6,1
        1,0
        0,5
        1,6
        2,0
        """.Split(Environment.NewLine);

        List<(int X, int Y)> dir = [(0, -1), (1, 0), (0, 1), (-1, 0)];

        var boundce = new Boundce(6, 6);
        var map = new Dictionary<(int X, int Y), char>();
        
        return 0;
    }

    public int Star2()
    {
        return 0;
    }
}
