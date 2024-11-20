namespace AoC.Solvers.Y2023;

public class Day15(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = input.Split(",");

    public int Star1() => Input.Sum(t => Hash(t));


    public int Star2()
    {
        var hashMap = Enumerable.Range(0, 256).Select(t => new List<Lens>()).ToArray();
        var lenses = Input.Select(i => new Lens(i)).ToArray();
        foreach (var lens in lenses)
        {
            var existing = hashMap[lens.Hash].FindIndex(t => t.Label == lens.Label);
            if (existing != -1)
            {
                hashMap[lens.Hash].RemoveAt(existing);
                if (lens.Opertation == "=")
                    hashMap[lens.Hash].Insert(existing, lens);
            }
            else if (lens.Opertation == "=")
                hashMap[lens.Hash].Add(lens);
        }
        return hashMap.Select((box, boxnr) => (1 + boxnr) * box.Select((lens, slot) => (slot + 1) * lens.FocalLength).Sum()).Sum();
    }

    private static int Hash(string str)
    {
        int value = 0;
        foreach (var c in str)
        {
            value += c;
            value *= 17;
            value %= 256;
        }
        return value;
    }

    record Lens
    {
        public string Label { get; set; }
        public string Opertation { get; set; } = string.Empty;
        public int FocalLength { get; set; } = 0;
        public int Hash { get; set; }
        public Lens(string s)
        {
            if (s.Contains('='))
            {
                FocalLength = int.Parse(s.Last().ToString());
                Opertation = "=";
                Label = s[0..^2];
            }
            else
                Label = s[0..^1];
            Hash = Hash(Label);
        }
    }
}
