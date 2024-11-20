namespace AoC.Solvers.Y2015;

public class Day14 : IDay
{
    public Day14(string input)
    {
        Input = InputParsers.GetInputLines(input);
    }
    public Day14(string input, int ticks)
    {
        Ticks = ticks;
        Input = InputParsers.GetInputLines(input);
    }
    public string Output => throw new NotImplementedException();
    public int Ticks { get; set; } = 2503;
    private string[] Input { get; set; }

    public int Star1() => RunOlympics().Max(t => t.Distance);
    public int Star2() => RunOlympics().Max(t => t.Points);

    private List<Reindeer> RunOlympics()
    {
        var raindeers = Input.Select(t => new Reindeer(t)).ToList();
        for (int time = 0; time < Ticks; time++)
        {
            raindeers.ForEach(t => t.Tick());
            var leadDistance = raindeers.Max(t => t.Distance);
            raindeers.ForEach(t => t.Points = t.Distance == leadDistance ? t.Points + 1 : t.Points);
        }
        return raindeers;
    }

    private class Reindeer
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int RunningTime { get; set; }
        public int SleepingTime { get; set; }
        public int Distance { get; set; }
        public int Time { get; set; } = 0;
        public int TimeToSleep { get; set; }
        public int TimeToRun { get; set; }
        public int Points { get; set; } = 0;
        public Reindeer(string str)
        {
            var s = str.Split(" ");
            Name = s[0];
            Speed = int.Parse(s[3]);
            RunningTime = int.Parse(s[6]);
            SleepingTime = int.Parse(s[13]);
            TimeToRun = RunningTime;
        }
        public void Tick()
        {
            if (TimeToSleep > 0)
            {
                Time++;
                TimeToSleep--;
                if (TimeToSleep == 0)
                    TimeToRun = RunningTime;
            }
            else
            {
                Distance += Speed;
                TimeToRun--;
                if (TimeToRun == 0)
                    TimeToSleep = SleepingTime;
            }
        }
    }
}
