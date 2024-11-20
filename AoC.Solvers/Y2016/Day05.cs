using AoC.AoCUtils;
using System.Security.Cryptography;
using System.Text;

namespace AoC.Solvers.Y2016;

public class Day05(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private string Input { get; set; } = input;
    private Dictionary<byte, string> Lut = new() {
        { 0, "0"},
        { 1, "1"},
        { 2, "2"},
        { 3, "3"},
        { 4, "4"},
        { 5, "5"},
        { 6, "6"},
        { 7, "7"},
        { 8, "8"},
        { 9, "9"},
        { 10, "a"},
        { 11, "b"},
        { 12, "c"},
        { 13, "h"},
        { 14, "e"},
        { 15, "f"}
    };

    public int Star1()
    {
        var  hasher = MD5.Create();
        long index = 2231254;
        while(output.Length < 8)
        {
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes($"{Input}{index++}"));
            if(hash[0] == 0 && hash[1] == 0 && hash[2]<16)
                output+=Lut[hash[2]];
        }
        return -1;
    }
    public int Star2()
    {
        output = string.Empty;
            
        var  hasher = MD5.Create();
        long index = 2231254;
        string[] password = new string[8];
        while(output.Length < 8)
        {
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes($"{Input}{index++}"));
            if(hash[0] == 0 && hash[1] == 0 && hash[2] < 8 && string.IsNullOrEmpty(password[hash[2]]))
            {
                var hexHash = string.Concat(hash.Select(t => t.ToString("X2"))).ToLower();
                password[hash[2]] = hexHash[6].ToString();
                output = string.Concat(password.Where(t => t != ""));
            }
        }
        return -1;
    }
}
