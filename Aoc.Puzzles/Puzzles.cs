using System.Reflection;
using AoC.Utils;

namespace AoC.Puzzles;

public static class Puzzles
{
    public const int Y2016 = 2016;
    public static List<string> GetDays(int year)
    {
        var theList = Assembly.GetExecutingAssembly().GetTypes()
                      .Where(t => t.Namespace == $"Aoc.Puzzles.Y{year}").Select(t => t.Name.Remove(0,3)).ToList();
        return theList;
    }

    public static IDay? GetDay(int year, string day)
    {
        var t = Assembly.GetExecutingAssembly().GetTypes()
                      .SingleOrDefault(t => t.Namespace == $"Aoc.Puzzles.Y{year}" && t.Name == $"Day{day}");
        if( t != null)
            return (IDay?)Activator.CreateInstance(t, InputReader.GetInput(year, day));
        return  GetDay(year, GetDays(year).Last());
    }
}