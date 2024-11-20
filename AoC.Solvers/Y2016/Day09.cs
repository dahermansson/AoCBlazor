using System.Numerics;
using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day09(string input) : IDay
{
    public string Output => output;

    public string output { get; set; } = string.Empty;
    private string Input { get; set; } = input;

    public int Star1() => Decompress(Input).Count(p => !string.IsNullOrWhiteSpace(p.ToString()));
    private string Decompress(string s)
    {
        int index = FindMarker(s, 0);
        while(index > -1)
        {
            var marker = CreateMarker(s, index);
            var e = ReplaceMarker(s, index, marker);
            s = e.s;
            index = FindMarker(s, e.nextIndex);
        }
        return s;
    }

    public int Star2()
    {
        int[] values = Enumerable.Range(0, Input.Length).Select(t => 1).ToArray();
        BigInteger length = 0;
        for (int i = 0; i < Input.Length; i++)
            if(Input[i] == '(')
            {
                var marker = CreateMarker(Input, i);
                for (int p = marker.markerIndex + marker.MarkerLength; p < marker.markerIndex + marker.MarkerLength + marker.Chars; p++)
                    values[p] *= marker.Repeat;
                i += marker.MarkerLength-1;
            }
            else
                length+=values[i];

        output = length.ToString();
        return -1;
    }

    
    private int FindMarker(string s, int startIndex) => s.IndexOf('(', startIndex);
    
    private Marker CreateMarker(string s, int markerIndex)
    {
        var markerEndIndex = s.IndexOf(')', markerIndex);
        var marker = s.Substring(markerIndex, markerEndIndex - markerIndex+1);
        int markerLength = marker.Length;
        var a = marker.Split("x").Select(t => int.Parse(t.Trim('(', ')'))).ToArray();
        return new Marker(a[0], a[1], markerLength, markerIndex);
    }
    private (string s, int nextIndex) ReplaceMarker(string s, int markerIndex, Marker marker)
    {
        var replace = s.Substring(markerIndex+marker.MarkerLength, marker.Chars);
        s = s.Remove(markerIndex, marker.MarkerLength + replace.Length);
        for(int i = 0; i<marker.Repeat;i++)
            s = s.Insert(markerIndex, replace);
        return (s, markerIndex + (replace.Length*marker.Repeat));
    }

    private record Marker(int Chars, int Repeat, int MarkerLength, int markerIndex);
}
