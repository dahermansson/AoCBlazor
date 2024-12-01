using System.Reflection;
using AoC.InputHandling.Interfaces;

namespace AoC.Solvers;

public class SolversManager(IInputHandler inputHandler)
{
    public const int Y2015 = 2015;
    public const int Y2016 = 2016;
    public const int Y2017 = 2017;
    public const int Y2021 = 2021;
    public const int Y2023 = 2023;
    public const int Y2024 = 2024;
    public static List<string> GetDays(int year)
    {
        var theList = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(t => t.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>() == null &&
                        t != null &&
                        t.Namespace == $"AoC.Solvers.Y{year}" && 
                        !t.AssemblyQualifiedName!.Contains('+')).Select(t => t.Name.Remove(0,3)).ToList();
        return theList;
    }

    public async Task<IDay?> GetDay(int year, string day)
    {
        var t = Assembly.GetExecutingAssembly().GetTypes()
                      .SingleOrDefault(t => t.Namespace == $"AoC.Solvers.Y{year}" && t.Name == $"Day{day}");
        if( t != null)
            return (IDay?)Activator.CreateInstance(t, await inputHandler.GetInput(year, int.Parse(day)));
        return  await GetDay(year, GetDays(year).Last());
    }
}