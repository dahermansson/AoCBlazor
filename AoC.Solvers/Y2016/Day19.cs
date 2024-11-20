namespace AoC.Solvers.Y2016;

public class Day19(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    public int Star1()
    {
        LinkedList<int> elves = new(Enumerable.Range(1,int.Parse(Input)).ToArray());
        var remove = elves.First!.Next!;
        while(elves.Count > 1)
        {
            var nextToRemove = GetNext(remove);
            elves.Remove(remove);
            remove = GetNext(nextToRemove);
        }
        return elves.First.Value;

        LinkedListNode<int> GetNext(LinkedListNode<int> i) => i.Next ?? elves.First;
    }

    public int Star2()
    {
        LinkedList<int> elves = new(Enumerable.Range(1,int.Parse(Input)).ToArray());
        var remove = elves.Find(elves.Count/2)!.Next!;
        while(elves.Count > 1)
        {
            var nextToRemove = elves.Count % 2 == 0 ? GetNext(remove) : GetNext(GetNext(remove));
            elves.Remove(remove);
            remove = nextToRemove;
        }
        return elves.First!.Value;

        LinkedListNode<int> GetNext(LinkedListNode<int> i) => i?.Next ?? elves.First!;
    }
}
