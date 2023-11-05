using System.Numerics;
using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day25(string input) : IDay
{
    private static readonly BigInteger Multiplier = 252533;
    private static readonly BigInteger Divider = 33554393;
    private static readonly BigInteger FirstCode = 20151125;

    public string Output => output;
    private string output = string.Empty;
    private int[] Input { get; set; } = input.Split(" ").Where(t => int.TryParse(t.Trim(',', '.'), out int koord)).Select(t => int.Parse(t.Trim(',', '.'))).ToArray();

    public int Star1()
    {
        output = GetCode(IndexOf(Input[0], Input[1])).ToString();
        return -1;
    }
    public int Star2()
    {
        output = string.Concat(Enumerable.Repeat("*", 50));
        return -1;
    }
    private BigInteger GetCode(int i) => Enumerable.Repeat(0, i-1).Aggregate(FirstCode, (p, n) => p = GenerateCode(p));
    private BigInteger GenerateCode(BigInteger prev) => prev * Multiplier % Divider;
    private int FirstIndexOnRow(int row) => Enumerable.Range(1, row-1).Aggregate(1, (i, row) => i + row);
    private int IndexOf(int row, int col) => FirstIndexOnRow(row + (col-1)) + col-1;
}
