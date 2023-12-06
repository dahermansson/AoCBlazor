namespace AoC.Test;

using System.Text;
using AoC.Solvers.Y2016;
public class Tests_2016
{
    
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("R8, R4, R4, R8", 4)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("abc", "18f47a30")]
    [Theory]
    public void Day5_Star1(string input, string res)
    {
        var actor = new Day05(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("abc", "05ace8e3")]
    [Theory]
    public void Day5_Star2(string input, string res)
    {
        var actor = new Day05(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    eedadn
    drvtee
    eandsr
    raavrd
    atevrs
    tsrnev
    sdttsa
    rasrtv
    nssdts
    ntnada
    svetve
    tesnvt
    vntsnd
    vrdear
    dvrsen
    enarar
    """, "easter")]
    [Theory]
    public void Day6_Star1(string input, string res)
    {
        var actor = new Day06(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    eedadn
    drvtee
    eandsr
    raavrd
    atevrs
    tsrnev
    sdttsa
    rasrtv
    nssdts
    ntnada
    svetve
    tesnvt
    vntsnd
    vrdear
    dvrsen
    enarar
    """, "advent")]
    [Theory]
    public void Day6_Star2(string input, string res)
    {
        var actor = new Day06(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    abba[mnop]qrst
    abcd[bddb]xyyx
    aaaa[qwer]tyui
    ioxxoj[asdfgh]zxcvbn
    """, 2)]
    [Theory]
    public void Day7_Star1(string input, int res)
    {
        var actor = new Day07(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    aba[bab]xyz
    xyx[xyx]xyx
    aaa[kek]eke
    zazbz[bzb]cdb
    """, 3)]
    [Theory]
    public void Day7_Star2(string input, int res)
    {
        var actor = new Day07(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    rect 1x1
    """, 1)]
    [Theory]
    public void Day8_Star1(string input, int res)
    {
        var actor = new Day08(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    rect 1x1
    """)]
    [Theory]
    public void Day8_Star2(string input)
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.Append("#");
        sb.AppendLine(new string('.', 49));
        sb.AppendLine(new string('.', 50));
        sb.AppendLine(new string('.', 50));
        sb.AppendLine(new string('.', 50));
        sb.AppendLine(new string('.', 50));
        sb.AppendLine(new string('.', 50));
        var actor = new Day08(input);
        _ = actor.Star2();
        Assert.Equal(sb.ToString(), actor.Output);
    }

    [InlineData("A(2x2)BCD(2x2)EFG", 11)]
    [InlineData("(6x1)(1x3)A", 6)]
    [InlineData("ADVENT", 6)]
    [InlineData("A(1x5)BC", 7)]
    [InlineData("X(8x2)(3x3)ABCY", 18)]
    [Theory]
    public void Day9_Star1(string input, int res)
    {
        var actor = new Day09(input);
        Assert.Equal(res, actor.Star1());
    }


    [InlineData("(3x3)XYZ", "9")]
    [InlineData("(27x12)(20x12)(13x14)(7x10)(1x12)A", "241920")]
    [InlineData("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", "445")]
    [Theory]
    public void Day9_Star2(string input, string res)
    {
        var actor = new Day09(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("10 7 4", 11)]
    [Theory]
    public void Day13_Star1(string input, int res)
    {
        var actor = new Day13(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("abc", 22728)]
    [Theory]
    public void Day14_Star1(string input, int res)
    {
        var actor = new Day14(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("abc", 22122)]
    [Theory]
    public void Day14_Star2(string input, int res)
    {
        var actor = new Day14(input);
        Assert.Equal(res, actor.Star2());
    }
}