namespace AoC.Solvers.Y2021;

public class Day16 : IDay
{
    private string Input { get; init; }
    public string Output => _output;
    private string _output { get; set; } = string.Empty;

    private Dictionary<char, bool[]> HexToBin;
    public Day16(string input)
    {
        Input = input;

        HexToBin = new Dictionary<char, bool[]>
    {
        { '0', new bool[] { false, false, false, false } },
        { '1', new bool[] { false, false, false, true } },
        { '2', new bool[] { false, false, true, false } },
        { '3', new bool[] { false, false, true, true } },
        { '4', new bool[] { false, true, false, false } },
        { '5', new bool[] { false, true, false, true } },
        { '6', new bool[] { false, true, true, false } },
        { '7', new bool[] { false, true, true, true } },
        { '8', new bool[] { true, false, false, false } },
        { '9', new bool[] { true, false, false, true } },
        { 'A', new bool[] { true, false, true, false } },
        { 'B', new bool[] { true, false, true, true } },
        { 'C', new bool[] { true, true, false, false } },
        { 'D', new bool[] { true, true, false, true } },
        { 'E', new bool[] { true, true, true, false } },
        { 'F', new bool[] { true, true, true, true } }
    };
    }


    public int Star1()
    {
        var bits = new BitArray(Input.Select(h => HexToBin[h]).SelectMany(p => p).ToArray());
        ReadPackge(bits);
        return TotalVersion;
    }

    public int Star2()
    {
        var bits = new BitArray(Input.Select(h => HexToBin[h]).SelectMany(p => p).ToArray());
        _output = ReadPackge(bits).value.ToString();
        return -1;
    }

    private int TotalVersion = 0;

    private (int index, long value) ReadPackge(BitArray bits)
    {
        var header = ReadPackageHeader(bits);
        TotalVersion += header.version;
        var index = 0;
        long res = 0;
        if (header.typeId == 4)
        {
            var literal = ReadLiteralPackage(bits);
            return (literal.index, literal.value);
        }
        else if (header.typeId == 0) //Sum
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.Sum();
            index = r.index;
        }
        else if (header.typeId == 1) //Product
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.Aggregate((a, b) => a * b);
            index = r.index;
        }
        else if (header.typeId == 2) //Min
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.Min();
            index = r.index;
        }
        else if (header.typeId == 3) //Max
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.Max();
            index = r.index;
        }
        else if (header.typeId == 5) // >
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.First() > r.values.Last() ? 1 : 0;
            index = r.index;
        }
        else if (header.typeId == 6) // <
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.First() < r.values.Last() ? 1 : 0;
            index = r.index;
        }
        else if (header.typeId == 7) // =
        {
            var r = ReadOperatorPackage(bits);
            res = r.values.First() == r.values.Last() ? 1 : 0;
            index = r.index;
        }

        return (index, res);
    }

    private (int index, List<long> values) ReadTotalLengthPackages(BitArray bits, int totalLength)
    {
        int readBits = 0;
        List<long> values = [];
        while (readBits < totalLength)
        {
            var subPackbits = bits.GetRange(readBits, bits.Length - readBits);
            var res = ReadPackge(subPackbits);
            readBits += res.index;
            values.Add(res.value);
        }
        return (readBits, values);
    }

    private (int index, List<long> values) ReadNumberOfPackages(BitArray bits, int count)
    {
        int readBits = 0;
        int readPackages = 0;
        List<long> values = [];
        while (readPackages < count)
        {
            var subPackBits = bits.GetRange(readBits, bits.Length - readBits);
            var res = ReadPackge(subPackBits);
            readPackages++;
            readBits += res.index;
            values.Add(res.value);
        }
        return (readBits, values);
    }

    private static (int index, long value) ReadLiteralPackage(BitArray bits)
    {
        int groupSize = 4;
        var bitLiteral = new BitArray(groupSize);
        int readGrops = 0;
        int bitIndex = 6;
        bool literalType;
        do
        {
            literalType = bits[bitIndex++];
            bitLiteral = bitLiteral.Append(bits.GetRange(bitIndex, groupSize));
            readGrops++;
            bitIndex += groupSize;
        } while (literalType);

        return (bitIndex, bitLiteral.ToInt64Rev());
    }

    private (int index, List<long> values) ReadOperatorPackage(BitArray bits)
    {
        int readBits;
        List<long>? values;
        if (!bits[6])
        {
            var packagesLength = bits.GetRange(7, 15).ToIntRev();
            var res = ReadTotalLengthPackages(bits.GetRange(22, packagesLength), packagesLength);
            readBits = 22 + res.index;
            values = res.values;
        }
        else
        {
            var subPackage = bits.GetRange(7, 11).ToIntRev();
            var res = ReadNumberOfPackages(bits.GetRange(18, bits.Length - 18), subPackage);
            readBits = 18 + res.index;
            values = res.values;
        }
        return (readBits, values);
    }

    private static (int version, int typeId) ReadPackageHeader(BitArray bitArray) =>
         new(bitArray.ToInt(0, 3), bitArray.ToInt(3, 3));
}