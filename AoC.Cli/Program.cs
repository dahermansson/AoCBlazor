using AoC.Solvers;

<<<<<<< HEAD
const int YEAR = SolversManager.Y2023;
string dayToRun = 12.ToString("D2");
=======
const int YEAR = SolversManager.Y2016;
string dayToRun = 17.ToString("D2");
>>>>>>> aa3e708 (day17 2016)

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
