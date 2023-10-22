// See https://aka.ms/new-console-template for more information
using AoC.Puzzles;

const int YEAR = Puzzles.Y2016;
string dayToRun = 1.ToString("D2");


var day = Puzzles.GetDay(YEAR, dayToRun);
if(day != null)
{
var star1 = day.Star1();
var output = star1 == -1 ? day.Output : star1.ToString();

Console.WriteLine($"Star1: {output}");
var star2 = day.Star2();
output = star2 == -1 ? day.Output : star2.ToString();

Console.WriteLine($"Star2: {output}");
}
