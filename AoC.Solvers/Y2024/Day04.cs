namespace AoC.Solvers.Y2024;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        int CountXMAS((int x, int y) pos)
        {
            List<(int x, int y)> directions = [
            (0,1),
            (1,1),
            (1,0),
            (1,-1),
            (0, -1),
            (-1, -1),
            (-1, 0),
            (-1, 1)
            ];

            return directions.Count(d => 
            {
                string xmas = "";
                int x = pos.x;
                int y = pos.y;
                for (int i = 0; i < 4; i++)
                {
                    if (x > -1 && x < Input.Length &&
                        y > -1 && y < Input[0].Length)
                            xmas += Input[x][y];
                    y += d.y;
                    x += d.x;
                }
                return xmas is "XMAS";
            });
        }

        return Enumerable.Range(0, Input.Length).Sum(x =>
                    Enumerable.Range(0, Input[0].Length).Sum(y => Input[x][y] == 'X' ?
                        CountXMAS((x, y))
                        : 0));
    }

    public int Star2()
    {
        bool IsX_Mas((int x, int y) pos)
        {
            List<(int x, int y)> xmas = [
            (-1,-1),
            (0,0),
            (1,1),
            (-1,1),
            (0, 0),
            (1, -1)
            ];

            string mas1 = "";
            foreach (var p in xmas.Take(3))
            {
                var x = pos.x + p.x;
                var y = pos.y + p.y;

                if (x > -1 && x < Input.Length &&
                    y > -1 && y < Input[0].Length)
                        mas1 += Input[x][y];
            }

            if (mas1 is "MAS" or "SAM")
            {
                string mas2 = "";
                foreach (var p in xmas.Skip(3).Take(3))
                {
                    var x = pos.x + p.x;
                    var y = pos.y + p.y;

                    if (x > -1 && x < Input.Length &&
                        y > -1 && y < Input[0].Length)
                            mas2 += Input[x][y];
                }
                return mas2 is "MAS" or "SAM";
            }
            return false;
        }

        return Enumerable.Range(1, Input.Length-1).Sum(x =>
                    Enumerable.Range(1, Input[0].Length-1).Count(y => Input[x][y] == 'A' && IsX_Mas((x, y))));
    }
}
