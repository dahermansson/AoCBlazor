namespace AoC.Solvers.Y2024;

public class Day09(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private string Input { get; set; } = input;

    private List<long> CreateDiskMap() => Input.Chunk(2)
            .Select(
                (File, Id) => (Id, Used: (int)char.GetNumericValue(File[0]), Free: File.Length == 1 ? 0 : (int)char.GetNumericValue(File[1]))
            )
            .Aggregate(new List<long>(), (res, a) =>
            {
                res.AddRange([.. Enumerable.Repeat(a.Id, a.Used), .. Enumerable.Repeat(-1, a.Free)]);
                return res;
            });

    public int Star1()
    {
        var disk = CreateDiskMap();

        while (disk.FindLastIndex(c => c > -1) > disk.FindIndex(c => c == -1))
        {
            var indexToMove = disk.FindLastIndex(c => c > -1);
            disk[disk.FindIndex(c => c == -1)] = disk[indexToMove];
            disk[indexToMove] = -1;
        }

        output = disk.Select((id, position) => id is not -1 ? position * id : 0).Sum().ToString();
        return -1;
    }

    public int Star2()
    {
        var diskMap = CreateDiskMap();

        var disk = diskMap.SkipWhile(c => c == diskMap.First()).Aggregate(new List<List<long>>() { diskMap.TakeWhile(t => t == diskMap.First()).ToList() }, (acc, id) =>
        {
            if (acc.Last().First() == id)
                acc.Last().Add(id);
            else
                acc.Add([id]);
            return acc;
        });

        for (int indexToMove = disk.Count - 1; indexToMove > 0; indexToMove--)
        {
            if (disk[indexToMove].Any(t => t == -1))
                continue;
                
            var newIndex = disk.FindIndex(t => t.Count(t => t == -1) >= disk[indexToMove].Count);
            if (newIndex > 0 && newIndex < indexToMove)
            {
                var subIndex = disk[newIndex].IndexOf(-1);
                disk[newIndex].InsertRange(subIndex, disk[indexToMove]);
                Enumerable.Range(0, disk[indexToMove].Count).ToList().ForEach(t => disk[newIndex].Remove(-1));
                disk[indexToMove] = [.. Enumerable.Repeat(-1, disk[indexToMove].Count)];
            }
        }

        output = disk.SelectMany(t => t).Select((id, position) => id is not -1 ? id * position : 0).Sum().ToString();
        return -1;
    }
}
