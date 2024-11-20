namespace AoC.AoCUtils;

public class BaseConverter(char[] baseChars)
{
    public char[] BaseChars { get; set; } = baseChars;
    public Dictionary<char, int> CharValues { get; set; } = baseChars.Select((c, i) => (c, i)).ToDictionary(t => t.c, t => t.i);

    public string ToBase(long i)
    {
        Stack<char> result = new();
        int targetBase = BaseChars.Length;
        do
        {
            result.Push(BaseChars[i % targetBase]);
            i = i/targetBase;
        }while(i > 0);
        return string.Concat(result);
    }

    public long ToNumber(string value)
    {
        var chars = value.ToCharArray();
        int targetBase = BaseChars.Length;
        int pow = chars.Length -1;
        int x = 0;
        long res = 0;
        for (int i = 0; i < chars.Length; i++)
        {
            x = CharValues[chars[i]];
            res += x*(long)Math.Pow(targetBase, pow--);
        }
        return res;
    }
}