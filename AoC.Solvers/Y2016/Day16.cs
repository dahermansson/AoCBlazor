namespace AoC.Solvers.Y2016;

public class Day16 : IDay
{
    public Day16(string input) => Input = input;
    public string Output => output;
    private string output { get; set; } = default!;
    private string Input { get; set; }
    private static readonly bool[] Zero = [false];
    public int Star1()
    {
        int length = 272;
        var data = CreateBitArray(Input);
        while (data.Count < length)
            data = DragonCurveish(data);

        output = CreateChecksum(data.GetRange(0, length)).Print();
        return -1;
    }
    public int Star2()
    {
        int length = 35651584;

        var data = CreateBitArray(Input);
        while (data.Count < length)
            data = DragonCurveish(data);

        output = CreateChecksum(data.GetRange(0, length)).Print();
        return -1;
    }

    private BitArray CreateBitArray(string s) => new(s.Select(t => t == '1').ToArray());

    private BitArray DragonCurveish(BitArray a)
    {
        BitArray b = new(a);
        b.Reverse();
        var t = b.Xor(new(b.Length, true));
        a = a.Append(new(Zero));
        a = a.Append(t);
        return a;
    }
    private BitArray CreateChecksum(BitArray a)
    {
        if (a.Length % 2 == 1)
            return a;
        var res = new bool[a.Count / 2];
        int resIndex = 0;
        for (int i = 0; i < a.Length - 1; i += 2)
            res[resIndex++] = a[i] == a[i + 1];
        return CreateChecksum(new(res));
    }
}
