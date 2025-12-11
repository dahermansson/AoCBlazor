namespace AoC.Solvers.Y2025;

public class Day11(string input) : IDay
{
    public string Output => _output;

    private string _output = string.Empty;

    private Dictionary<string, List<string>> ServerRack { get; set; } = InputParsers.GetInputLines(input).Select(t => {
            var key = string.Concat(t.TakeWhile(p => p != ':'));
            var values = t.Split(" ").Skip(1);
            return new KeyValuePair<string, List<string>>(key, values.ToList());
        }).ToDictionary();

    private Dictionary<string, long> Cache { get; set; } =  [];

    public int Star1()
    {
        _output = CountPaths("you", "out").ToString();
        return -1;
    }

    public int Star2()
    {
        Cache = [];
        var svr_to_fft = CountPaths("svr", "fft");
        Cache = [];
        var fft_to_dac = CountPaths("fft", "dac");
        Cache = [];
        var dac_to_out = CountPaths("dac", "out");
    
        _output = (dac_to_out * fft_to_dac*svr_to_fft).ToString();
        return -1;
    }

    private long CountPaths(string a, string goal)
    {
        if(a == goal)
            return 1;
        else
        {
            if(Cache.TryGetValue(a, out long res))
                return res;
            if(!ServerRack.ContainsKey(a))
                return 0;
            Cache[a] = ServerRack[a].Sum(next => CountPaths(next, goal));
            return Cache[a];
        }
    }
}