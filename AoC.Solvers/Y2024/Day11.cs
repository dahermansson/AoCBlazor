namespace AoC.Solvers.Y2024;

public class Day11: IDay
{
    public Day11(string input) => Input = input.Split(' ').Select(long.Parse).ToList();
    public string Output => throw new NotImplementedException();

    private List<long> Input {get; set;}

    public int Star1()
    {
        List<long> numbers = [..Input];
        List<(int loop, int count)> counts = [(0, numbers.Count)];
        for (int i = 0; i< 25;i++)
        {
        numbers = numbers.Aggregate(new List<long>(), (a, b) => {
            if(b == 0)
                a.Add(1);
            else if(b.ToString().Length % 2 == 0)
            {
                var s = b.ToString();
                var first = s[..(s.Length / 2)];
                var secound = s[(s.Length/2)..];                
                a.AddRange([long.Parse(first), long.Parse(secound)]);
            }
            else
                a.Add(b * 2024);
            return a;
        });
        counts.Add((i, numbers.Count));
        }

        counts.ForEach(t => Console.WriteLine(t));
        return numbers.Count;
    }

    public int Star2()
    {
        List<long> numbers = [..Input];
        
        long zeros = numbers.Count(c => c ==0 );
        long splits = numbers.Count(c => c.ToString().Length % 2 ==0 );
        long multiply = numbers.Count(c => c > 0 && c.ToString().Length % 2 != 0);
        long total = zeros + splits + multiply;
        for (int i = 0; i< 25;i++)
        {
            
        }
        return 0;
    }
}
