using System.Security.Cryptography;
using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    public int Star1() => FindHash(5, Input);
    public int Star2() => FindHash(6, Input);

    private int FindHash(int zeros, string input)
    {
        var md5 = MD5.Create();
        int number = 0;
        string hash;
        do
        {
            var inputBytes = System.Text.Encoding.ASCII.GetBytes($"{input}{number++}");
            var hashBytes = md5.ComputeHash(inputBytes);
            hash = Convert.ToHexString(hashBytes);
        } while( hash.Take(zeros).Any(t => t != '0'));
        return number-1;
    }
}
