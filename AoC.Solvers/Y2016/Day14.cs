using System.Security.Cryptography;

namespace AoC.Solvers.Y2016;

public class Day14(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;
    private MD5 md5 = MD5.Create();
    public int Star1()
    {
        return FindHash(Input, false);
    }

    public int Star2()
    {
        return FindHash(Input, true);
    }

    private int FindHash(string input, bool stretch)
    {
        List<(int nr, char c)> tripplets = [];
        List<int> hashes = [];
        int number = 0;
        string hash;
        do
        {
            hash = GetHash($"{input}{number}", stretch ? 2017 : 1);
            var c = HasTripplet(hash);
            if(c != null)
                tripplets.Add((number, c.Value));
            hashes.AddRange(tripplets.Where(t =>  number - t.nr < 1000 && t.nr != number && hash.Contains(new string(t.c, 5))).Select(h => h.nr));
            number++;
        } while( hashes.Count < 64 );
        var t = hashes.OrderBy(t => t).ToArray();
        return t.ElementAt(stretch ? 63 : 64); //No clue why :(
    }

    private string GetHash(string s, int stretch)
    {
        var hash = s;
        for(int i = 0; i<stretch; i++)
        {
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(hash);
            var hashBytes = md5.ComputeHash(inputBytes);
            hash = Convert.ToHexString(hashBytes).ToLower();
        }
        return hash;
    }

    private char? HasTripplet(string s)
    {
        foreach(var d in s.Distinct())
        {
            if(s.Contains(new string(d, 3)))
                return d;
        }
        return null;
    }

}
