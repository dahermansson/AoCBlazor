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

}