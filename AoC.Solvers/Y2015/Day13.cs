using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day13 : IDay
{
    public Day13(string input)
    {
        Attendees = InputParsers.GetInputLines(input).Select(t => Parse(t)).ToList();
    }
    private List<Attendee> Attendees { get; set; }
    public string Output => throw new NotImplementedException();
    public int Star1()
    {
        Queue<List<string>> queue = new();
        var distAtteendees = Attendees.DistinctBy(t => t.name).OrderBy(t => t.name).Skip(1).ToList();

        var goal = distAtteendees.First().name;

        var seetings = FindAllSeetings(Attendees.OrderBy(t => t.name).First().name, goal);

        List<int> max = CalculateHappiness(distAtteendees, seetings);
        return max.Max();
    }

    public int Star2()
    {
        AddMe();
        Queue<List<string>> queue = new();
        var distAtteendees = Attendees.DistinctBy(t => t.name).OrderBy(t => t.name).Skip(1).ToList();

        var goal = distAtteendees.First().name;
        var seetings = FindAllSeetings(Attendees.OrderBy(t => t.name).First().name, goal);
        
        List<int> max = CalculateHappiness(distAtteendees, seetings);
        return max.Max();
    }

    void AddMe()
    {
        var distAtteendees = Attendees.DistinctBy(t => t.name).ToList();
        foreach (var a in distAtteendees)
        {
            Attendees.Add(new Attendee(a.name, "me", 0, 0));
            Attendees.Add(new Attendee("me", a.name, 0, 0));
        }
    }

    private List<int> CalculateHappiness(List<Attendee> distAtteendees, List<List<string>> seetings)
    {
        var res = seetings.Where(t => t.Count == distAtteendees.Count + 1).ToList(); ;
        List<int> max = new();
        foreach (var r in res)
        {
            var gain = 0;
            var loos = 0;

            for (int i = 0; i < r.Count; i++)
            {
                var n1 = i == r.Count - 1 ? r[0] : r[i + 1];
                var n2 = i == 0 ? r[^1] : r[i - 1];
                var a = r[i];

                gain += Attendees.Single(t => t.name == a && t.next == n1).gain;
                loos += Attendees.Single(t => t.name == a && t.next == n1).loos;
                gain += Attendees.Single(t => t.name == a && t.next == n2).gain;
                loos += Attendees.Single(t => t.name == a && t.next == n2).loos;
            }
            max.Add(gain - loos);
        }
        return max;
    }

    private List<List<string>> FindAllSeetings(string start, string goal)
    {
        List<List<string>> seetings = new();
        Queue<List<string>> queue = new();
        List<string> path = new() { Attendees.OrderBy(t => t.name).First().name };
        
        queue.Enqueue(path);

        while (queue.Count != 0)
        {
            path = queue.Dequeue();
            var last = path[^1];
            if (last == goal)
                seetings.Add(path);

            List<string> next = Attendees.Where(t => t.name == last).Select(t => t.next).ToList();
            for (int i = 0; i < next.Count; i++)
            {
                if (!path.Any(t => t == next[i]))
                {
                    List<string> newPath = [.. path, next[i]];
                    queue.Enqueue(newPath);
                }
            }
        }
        return seetings;
    }

    Attendee Parse(string s)
    {
        var splits = s.Split(" ");
        if (splits[2] == "gain")
            return new Attendee(splits[0], splits[^1].Trim('.'), int.Parse(splits[3]), 0);
        return new Attendee(splits[0], splits[^1].Trim('.'), 0, int.Parse(splits[3]));
    }

    record Attendee(string name, string next, int gain, int loos);
}
