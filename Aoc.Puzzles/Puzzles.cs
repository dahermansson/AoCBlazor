using System.Reflection;
using AoC.Utils;

namespace AoC.Puzzles;

public static class Puzzles
{
    public const int Y2016 = 2016;
    public static Dictionary<int, IDay?> GetDays(int year)
    {
        var theList = Assembly.GetExecutingAssembly().GetTypes()
                      .Where(t => t.Namespace == $"Aoc.Puzzles.Y{year}").Select(t => new{Day = int.Parse(t.Name.Remove(0,3)), Puzzle = (IDay?)Activator.CreateInstance(t)})
                      .ToDictionary(k => k.Day, v => v.Puzzle);
        return theList;
    }

    public static IDay? GetDay(int year, int day)
    {
        var t = Assembly.GetExecutingAssembly().GetTypes()
                      .SingleOrDefault(t => t.Namespace == $"Aoc.Puzzles.Y{year}" && t.Name == $"Day{day.ToString("D2")}");
        if( t != null)
            return (IDay?)Activator.CreateInstance(t);
        return GetDays(year).Values.Last();
    }
}