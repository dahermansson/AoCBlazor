using AoC.Solvers;

const int YEAR = SolversManager.Y2023;
string dayToRun = 2.ToString("D2");

var day = SolversManager.GetDay(YEAR, dayToRun);
if (day != null)
{
    var star1 = day.Star1();
    var output = star1 == -1 ? day.Output : star1.ToString();

    Console.WriteLine($"Star1: {output}");
    var star2 = day.Star2();
    output = star2 == -1 ? day.Output : star2.ToString();

    Console.WriteLine($"Star2: {output}");
}
