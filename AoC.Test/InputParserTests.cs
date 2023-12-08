using AoC.AoCUtils;

namespace AoC.Test;

public class InputParserTests
{
    [Fact]
    public void Test_GetInputLines()
    {
        var testData = File.ReadAllText(@"Testdata\inputLineBreake.txt");
        var result = InputParsers.GetInputLines(testData);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void Test_GetInputLines_t()
    {
        var testData = File.ReadAllText(@"Testdata\inputlines_numeric.txt");
        var result = InputParsers.GetInputLines<int>(testData);
        Assert.Equal(3, result.Length);
        Assert.Collection(result, 
            i => Assert.Equal(1, i),
            i => Assert.Equal(2, i),
            i => Assert.Equal(3, i)
        );
        
    }

}