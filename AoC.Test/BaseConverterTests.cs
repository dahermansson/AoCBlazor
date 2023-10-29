using AoC.Utils;

namespace AoC.Test;

public class BaseConverterTests
{

    [InlineData(1, "a")]
    [InlineData(2, "b")]
    [InlineData(26, "z")]
    [InlineData(28, "aa")]
    [InlineData(29, "ab")]
    [Theory]
    public void NumericToBase27(long i, string res)
    {
        var alfaBase = Enumerable.Range(0, 26).Select(t => (char)(t+97)).ToList();
        alfaBase.Insert(0, '-');
        BaseConverter bc = new BaseConverter(alfaBase.ToArray());
        Assert.Equal(res, bc.ToBase(i));
    }

    [InlineData(1, "a")]
    [InlineData(2, "b")]
    [InlineData(26, "z")]
    [InlineData(28, "aa")]
    [InlineData(29, "ab")]
    [Theory]
    public void BaseToNumeric(long res, string b)
    {
        var alfaBase = Enumerable.Range(0, 26).Select(t => (char)(t+97)).ToList();
        alfaBase.Insert(0, '-');
        BaseConverter bc = new BaseConverter(alfaBase.ToArray());
        Assert.Equal(res, bc.ToNumber(b));
    }
    
}