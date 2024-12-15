using System.Text;

namespace AoC.AoCUtils;

public static class Extensions
{
    /// <summary>
    /// Some sort of modulus to keep wrap array index
    /// Ex. 
    /// range: 0 to 5
    /// value: 3
    /// arrayLength: 5
    /// returns: 3
    /// 
    /// /// Ex. 
    /// range: 0 to 5
    /// value: -1
    /// arrayLength: 5
    /// returns: 4
    /// 
    /// /// /// Ex. 
    /// range: 0 to 5
    /// value: 6
    /// arrayLength: 5
    /// returns: 1
    /// 
    /// </summary>
    /// <param name="value">Value outside of index</param>
    /// <param name="arrayLength"></param>
    /// <returns></returns>
    public static int GetWrappingIndex(this int value, int arrayLength) => ((value % arrayLength) + arrayLength) % arrayLength;

    public static string ToPrintableString(this Dictionary<(int X, int Y), char> values)
    {
        var sb = new StringBuilder();
        var rowsIndex = values.Keys.Max(t => t.X)+1;
        var colIndex = values.Keys.Max(t => t.Y)+1;
        for (int x = 0; x < rowsIndex; x++)
        {
            for (int y = 0; y < colIndex; y++)
                sb.Append(values[(x, y)]);
            sb.AppendLine();
        }
        sb.AppendLine();
        return sb.ToString();
    }
}
