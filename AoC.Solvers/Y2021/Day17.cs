namespace AoC.Solvers.Y2021;

public class Day17(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private int yMin = input.Split(["..", ","], StringSplitOptions.RemoveEmptyEntries)[2].ExtraxtInteger();
    private int yMax = input.Split(["..", ","], StringSplitOptions.RemoveEmptyEntries).Last().ExtraxtInteger();
    private int xMin = input.Split(["..", ","], StringSplitOptions.RemoveEmptyEntries).First().ExtraxtInteger();
    private int xMax = input.Split(["..", ","], StringSplitOptions.RemoveEmptyEntries)[1].ExtraxtInteger();
    public int Star1()
    {
        var hits = new List<int[]>();
        for (int i = Math.Abs(yMax); i < Math.Abs(yMin); i++)
        {
            var yn = CalcY(i).ToArray();
            if (yn.Any(y => InYRange(y)))
                hits.Add(yn);
        }
        return hits.Max(m => m.Max(y => y));
    }

    public int Star2()
    {
        var hits = 0;
        for (int y = yMin; y <= Math.Abs(yMin); y++)
            for (int x = 1; x <= xMax + 1; x++)
                if (Trejectory(x, y))
                    hits++;
        return hits;
    }
    public bool InYRange(int i) => i >= yMin && i <= yMax;
    public bool InXRange(int i) => i >= xMin && i <= xMax;
    public bool InRange(int x, int y) => InXRange(x) && InYRange(y);
    public bool Trejectory(int vx, int vy)
    {
        var yn = CalcY(vy).ToArray();
        var xn = CalcX(vx).ToArray();
        for (int i = 0; i < yn.Length; i++)
            if (InRange(i >= xn.Length ? xn.Last() : xn[i], yn[i]))
                return true;
        return false;
    }

    public IEnumerable<int> CalcY(int i)
    {
        var v = i;
        while (i >= yMin)
        {
            yield return i;
            i += --v;
        }
    }

    public IEnumerable<int> CalcX(int i)
    {
        var v = i;
        while (v != 0)
        {
            yield return i;
            i += --v;
        }
    }
}