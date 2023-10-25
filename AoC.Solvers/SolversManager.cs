using System.Reflection;
using AoC.Utils;

namespace AoC.Solvers;

public static class SolversManager
{
    public const int Y2016 = 2016;
    public const int Y2015 = 2015;
    public static List<string> GetDays(int year)
    {
        var theList = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(t => t.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>() == null &&
                        t != null &&
                        t.Namespace == $"AoC.Solvers.Y{year}" && 
                        !t.AssemblyQualifiedName!.Contains("+")).Select(t => t.Name.Remove(0,3)).ToList();
        return theList;
    }

    public static IDay? GetDay(int year, string day)
    {
        var t = Assembly.GetExecutingAssembly().GetTypes()
                      .SingleOrDefault(t => t.Namespace == $"AoC.Solvers.Y{year}" && t.Name == $"Day{day}");
        if( t != null)
            return (IDay?)Activator.CreateInstance(t, InputReader.GetInput(year, day));
        return  GetDay(year, GetDays(year).Last());
    }
}