using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Toolkit.HighPerformance;


namespace AoC.AoCUtils;

public static class Utils
{
    public static string RemoveWhiteSpaces(this string s) => new string(s.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
    public static char[] ABC { get { return Enumerable.Range(97, 26).Select(n => (char)n).ToArray(); } }
    public static readonly string NL = "\r\n";
    public static readonly string DNL = $"{NL}{NL}";
    public static int ManhattanDistance(int x, int y) => Math.Abs(x) + Math.Abs(y);
    public static int ManhattanDistance(Point x, Point y) => Math.Abs(x.X - y.X) + Math.Abs(x.Y - y.Y);
    public static int ManhattanDistance((int x,  int y) x, (int x, int y ) y) => Math.Abs(x.x - y.x) + Math.Abs(x.y - y.y);
    public static long ManhattanDistance((long x,  long y) x, (long x, long y ) y) => Math.Abs(x.x - y.x) + Math.Abs(x.y - y.y);
    public static int ManhattanDistance(int x1, int x2, int y1, int y2) => Math.Abs(x1 - y1) + Math.Abs(x2 - y2);
    public static long LCM(long a, long b) => a / GCD(a, b) * b;
    public static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static IEnumerable<int> IndexOfMany<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int i = 0;
        foreach (var element in source)
        {
            if (predicate(element))
                yield return i;
            i++;
        }
    }

    public static BitArray ToBitArray(this string s) => new(s.Reverse().Select(t => t == '1').ToArray());
    public static int ToInt(this BitArray bits)
    {
        int[] res = new int[1];
        bits.CopyTo(res, 0);
        return res[0];
    }

    public static int ToInt(this BitArray bits, int startIndex, int count)
    {
        return bits.GetRange(startIndex, count).ToIntRev();
    }
    public static BitArray GetRange(this BitArray bits, int startIndex, int count)
    {
        var temp = new BitArray(count);
        int bitIndex = 0;
        for (int i = startIndex; i < startIndex + count; i++)
            temp[bitIndex++] = bits[i];
        return temp;
    }

    public static BitArray Append(this BitArray bits, BitArray append)
    {
        var res = new BitArray(bits.Length + append.Length);
        for (int i = 0; i < bits.Length; i++)
            res[i] = bits[i];
        for (int i = bits.Length; i < bits.Length + append.Length; i++)
            res[i] = append[i - bits.Length];
        return res;
    }

    public static int ToIntRev(this BitArray bits)
    {
        var temp = new BitArray(bits.Length);
        int index = 0;
        for (int i = bits.Length - 1; i > -1; i--)
            temp[index++] = bits[i];
        int[] res = new int[1];
        temp.CopyTo(res, 0);
        return res[0];
    }
    public static long ToInt64Rev(this BitArray bits)
    {
        var temp = new BitArray(bits.Length);
        int index = 0;
        for (int i = bits.Length - 1; i > -1; i--)
            temp[index++] = bits[i];
        var bArray = new Byte[8];
        temp.CopyTo(bArray, 0);
        return BitConverter.ToInt64(bArray, 0);
    }

    public static string Print(this BitArray bits)
    {
        StringBuilder sb = new();
        for (int i = 0; i < bits.Length; i++)
            sb.Append(bits[i] ? 1 : 0);
        return sb.ToString();
    }

    public static void Reverse(this BitArray array)
    {
        int length = array.Length;
        int mid = length / 2;

        for (int i = 0; i < mid; i++)
        {
            bool bit = array[i];
            array[i] = array[length - i - 1];
            array[length - i - 1] = bit;
        }    
    }

    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException("source");
        // Ensure that the source IEnumerable is evaluated only once
        return permutations(source.ToArray());
    }
    private static IEnumerable<IEnumerable<T>> permutations<T>(IEnumerable<T> source)
    {
        var c = source.Count();
        if (c == 1)
            yield return source;
        else
            for (int i = 0; i < c; i++)
                foreach (var p in permutations(source.Take(i).Concat(source.Skip(i + 1))))
                    yield return source.Skip(i).Take(1).Concat(p);
    }

    public static List<List<string>> GetPermutations(this List<string> list)
    {
        var res = new List<List<string>>();
        return DoPermutations(list, 0, list.Count - 1, res);
    }

    public static List<List<string>> DoPermutations(List<string> list, int start, int end, List<List<string>> res)
    {
        if (start == end)
            res.Add(new List<string>(list));
        else
        {
            for (int i = start; i <= end; i++)
            {
                var temp = list[start];
                list[start] = list[i];
                list[i] = temp;
                DoPermutations(list, start + 1, end, res);
                temp = list[start];
                list[start] = list[i];
                list[i] = temp;
            }
        }
        return res;
    }

    public static IEnumerable<int> ExtractIntegers(this string s) => Regex.Matches(s, @"\d+").Where(t => t.Success).Select(t => int.Parse(t.Value));
    
    public static IEnumerable<long> ExtractLongs(this string s) => Regex.Matches(s, @"\d+").Where(t => t.Success).Select(t => long.Parse(t.Value));

    public static int ExtraxtInteger(this string s) => int.Parse(Regex.Match(s, @"-?\d+").Value);
    public static int ExtraxtPositivInteger(this string s) => int.Parse(Regex.Match(s, @"\d+").Value);

    public static void Print(this Span2D<bool> span2D)
    {
        for (int i = 0; i < span2D.Height; i++)
        {
            var row = span2D.GetRowSpan(i);
            for(int p = 0; p < row.Length;p++)
                Console.Write(row[p] ? "#" : ".");
            Console.WriteLine();
        }
    }

    public static string Output(this Span2D<bool> span2D)
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        for (int i = 0; i < span2D.Height; i++)
        {
            var row = span2D.GetRowSpan(i);
            for(int p = 0; p < row.Length;p++)
                sb.Append(row[p] ? "#" : ".");
            sb.AppendLine();
        }
        return sb.ToString();
    }
}