using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day10(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    Dictionary<int, Bot> Bots = new();
    Dictionary<int, int> Bins = new();
    private int Star1Value = 0;

    public int Star1()
    {
        SetUpBots();
        RunBots();
        return Star1Value;
    }

    public int Star2()
    {
        SetUpBots();
        RunBots();
        return Enumerable.Range(0, 3).Aggregate(1, (sum, current) => sum * Bins[current]);
    }

    private void RunBots()
    {
        while (Bots.Any(t => t.Value.HasTwo))
        {
            foreach (var botsReadyForAction in Bots.Where(t => t.Value.HasTwo).ToList())
            {
                if (botsReadyForAction.Value.LowBot > -1)
                    Bots[botsReadyForAction.Value.LowBot].Add(botsReadyForAction.Value.GetLow());
                else
                    Bins[botsReadyForAction.Value.LowBin] = botsReadyForAction.Value.GetLow();
                if (botsReadyForAction.Value.HighBot > -1)
                    Bots[botsReadyForAction.Value.HighBot].Add(botsReadyForAction.Value.GetHigh());
                else
                    Bins[botsReadyForAction.Value.HighBin] = botsReadyForAction.Value.GetHigh();
                botsReadyForAction.Value.Microchip.Clear();
                var b = Bots.SingleOrDefault(t => t.Value.Microchip.Any(m => m == 17 && t.Value.Microchip.Any(m => m == 61))).Value;
                if (b != null)
                    Star1Value = b.Id;
            }
        }
    }
    private void SetUpBots()
    {
        Bots.Clear();
        Bins.Clear();
        foreach (string row in Input)
        {
            var splits = row.Split(" ");
            if (splits[0] == "value")
            {
                var bot = int.Parse(splits.Last());
                if (!Bots.ContainsKey(bot))
                    Bots[bot] = new Bot(bot);
                Bots[bot].Add(int.Parse(splits[1]));
            }
            else
            {
                var bot = int.Parse(splits[1]);
                if (!Bots.ContainsKey(bot))
                    Bots[bot] = new Bot(bot);
                var low = int.Parse(splits[6]);
                var high = int.Parse(splits.Last());
                if (splits[5] == "bot")
                    Bots[bot].LowBot = low;
                else
                    Bots[bot].LowBin = low;
                if (splits[10] == "bot")
                    Bots[bot].HighBot = high;
                else
                    Bots[bot].HighBin = high;
            }
        }
    }

    private class Bot
    {
        public Bot(int id)
        {
            Id = id;
        }
        public Bot(int id, int highBot = -1, int lowBot = -1, int highBin = -1, int lowBin = -1)
        {
            Id = id;
            HighBin = highBin;
            HighBot = highBot;
            LowBin = lowBin;
            LowBot = lowBot;
        }
        public int Id { get; set; }
        public int HighBot { get; set; } = -1;
        public int LowBot { get; set; } = -1;
        public int HighBin { get; set; } = -1;
        public int LowBin { get; set; } = -1;
        public List<int> Microchip { get; set; } = new();
        public void Add(int i) => Microchip.Add(i);
        public bool HasTwo => Microchip.Count == 2;
        public int GetLow() => Microchip.Min();
        public int GetHigh() => Microchip.Max();
    }
}