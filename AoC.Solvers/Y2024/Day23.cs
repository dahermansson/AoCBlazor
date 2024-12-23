namespace AoC.Solvers.Y2024;

public class Day23(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var g = new Dictionary<string, HashSet<string>>();
        Input.ToList().ForEach(x => {
            var p = x.Split('-');
            if(g.ContainsKey(p[0]))
                g[p[0]].Add(p[1]);
            else
                g[p[0]] = [p[1]];
            if(g.ContainsKey(p[1]))
                g[p[1]].Add(p[0]);
            else
                g[p[1]] = [p[0]];
        });

        var triplets = new HashSet<string>();

        foreach(var a in g)
        {
            foreach (var b in a.Value)
            {
                if(a.Key == b) continue;

                a.Value.Intersect(g[b]).ToList().ForEach(c => {
                    if(a.Key.StartsWith('t') || b.StartsWith('t') || c.StartsWith('t'))
                    {
                        List<string> tri = [a.Key, b, c];
                        triplets.Add(string.Join(',', tri.OrderBy(t => t)));
                    }
                });
            }
        }

        return triplets.Count;
    }


    public int Star2()
    {
        var g = new Dictionary<string, HashSet<string>>();
        Input.ToList().ForEach(x => {
            var p = x.Split('-');
            if(g.ContainsKey(p[0]))
                g[p[0]].Add(p[1]);
            else
                g[p[0]] = [p[1]];
            if(g.ContainsKey(p[1]))
                g[p[1]].Add(p[0]);
            else
                g[p[1]] = [p[0]];
        });


        Dictionary<string, HashSet<string>> networks = [];
        foreach (var pc in g)
        {
            networks[pc.Key] = [pc.Key];
            foreach (var connected in pc.Value)
            {
                if (networks[pc.Key].All(n => g[connected].Contains(n)))
                {
                    networks[pc.Key].Add(connected);
                }
            }
        }
        output = string.Join(',', networks.MaxBy(t => t.Value.Count).Value.OrderBy(t => t));
        return -1;
    }
}
