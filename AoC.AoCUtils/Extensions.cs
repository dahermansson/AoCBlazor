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
}
