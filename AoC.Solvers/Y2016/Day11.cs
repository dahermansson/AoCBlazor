using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day11 : IDay
{

    public Day11(string input) => Input = input;
    public string Output => throw new NotImplementedException();

    private string Input { get; set; }

    public int Star1() => Find(CreateIinitStateStar1());

    public int Star2() => Find(CreateIinitStateStar2());

    private int Find(State startState)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<State>();
        queue.Enqueue(startState);
        while (queue.Count != 0)
        {
            var next = queue.Dequeue();
            if (next.Floors[0].Count == 0 && next.Floors[1].Count == 0 && next.Floors[2].Count == 0)
                return next.Steeps;
            var nextState = next.GetValidNextStates();
            foreach (var state in nextState)
            {
                if (!visited.Contains(state.FlooreState()))
                {
                    visited.Add(state.FlooreState());
                    queue.Enqueue(state);
                }
            }
        }
        return 0;
    }

    private State CreateIinitTestState()
    {
        //h =1
        //l = 2
        List<Item>[] floors = [
            new List<Item>() { new Item { ElementType = 1, Type = Type.Chip}, new Item { ElementType = 2, Type = Type.Chip}},
            new List<Item>() { new Item { ElementType = 1, Type = Type.Generator}},
            new List<Item>() { new Item { ElementType = 2, Type = Type. Generator}},
            new List<Item>()
        ];
        return new State(0, floors, 0);
    }

    private State CreateIinitStateStar1()
    {
        //pol =1
        //thul = 2
        //pro = 3
        //rut =4
        //cob = 5
        List<Item>[] floors = [
            new List<Item>() {
                new Item { ElementType = 1, Type = Type.Generator},
                new Item { ElementType = 2, Type = Type.Generator},
                new Item { ElementType = 2, Type = Type.Chip},
                new Item { ElementType = 3, Type = Type.Generator},
                new Item { ElementType = 4, Type = Type.Generator},
                new Item { ElementType = 4, Type = Type.Chip},
                new Item { ElementType = 5, Type = Type.Generator},
                new Item { ElementType = 5, Type = Type.Chip},
                },
            new List<Item>() {
                new Item { ElementType = 1, Type = Type.Chip},
                new Item { ElementType = 3, Type = Type.Chip}
                },
            new List<Item>(),
            new List<Item>(),
        ];
        return new State(0, floors, 0);
    }

    private State CreateIinitStateStar2()
    {
        //pol =1
        //thul = 2
        //pro = 3
        //rut =4
        //cob = 5
        //ele = 6
        //dil = 7
        List<Item>[] floors = [
            new List<Item>() {
                new Item { ElementType = 1, Type = Type.Generator},
                new Item { ElementType = 2, Type = Type.Generator},
                new Item { ElementType = 2, Type = Type.Chip},
                new Item { ElementType = 3, Type = Type.Generator},
                new Item { ElementType = 4, Type = Type.Generator},
                new Item { ElementType = 4, Type = Type.Chip},
                new Item { ElementType = 5, Type = Type.Generator},
                new Item { ElementType = 5, Type = Type.Chip},
                new Item { ElementType = 6, Type = Type.Generator},
                new Item { ElementType = 6, Type = Type.Chip},
                new Item { ElementType = 7, Type = Type.Generator},
                new Item { ElementType = 7, Type = Type.Chip},
                },
            new List<Item>() {
                new Item { ElementType = 1, Type = Type.Chip},
                new Item { ElementType = 3, Type = Type.Chip}
                },
            new List<Item>(),
            new List<Item>(),
        ];
        return new State(0, floors, 0);
    }

    private record State
    {
        public State(int elevator, List<Item>[] floors, int steeps)
        {
            Elevator = elevator;
            Floors = floors;
            Steeps = steeps;
        }
        public List<Item>[] Floors { get; set; }
        public int Elevator { get; set; }
        public int Steeps { get; set; }
        public List<State> GetValidNextStates()
        {
            var res = new List<State>();
            if (Elevator != 3) // Go up first
            {
                var pairToMoveUp = FindMatchedPairOnFloor();
                if (pairToMoveUp != null)
                    res.Add(Move(Elevator + 1, pairToMoveUp));
                else
                {
                    var nextPossibleItemsToMoveUp = FindValidItemsToMovesUp().Take(2);
                    res.Add(Move(Elevator + 1, [.. nextPossibleItemsToMoveUp]));
                    return res;
                }
            }
            if (Elevator != 0 || res.Count == 0)
            {
                var nextPossibleItemsToMoveDown = FindValidItemsToMovesDown();
                foreach (var item in nextPossibleItemsToMoveDown)
                {
                    res.Add(Move(Elevator - 1, [item]));
                    return res;
                }
                if (res.Count == 0)
                {
                    var pairToMoveDown = FindMatchedPairOnFloor();
                    if (pairToMoveDown != null)
                        res.Add(Move(Elevator - 1, pairToMoveDown));
                }
            }
            return res;
        }

        private Item[] FindValidItemsToMovesDown()
        {
            if (ItemsOnLowerFloor())
                return Floors[Elevator].Where(i => !i.IsFriedByAny([.. Floors[Elevator - 1]])).ToArray();
            else
                return [];
        }
        private bool ItemsOnLowerFloor()
        {
            for (int i = 0; i < Elevator; i++)
                if (Floors[i].Count != 0)
                    return true;
            return false;

        }

        private Item[] FindValidItemsToMovesUp()
        {
            return Floors[Elevator].Where(i => !i.IsFriedByAny([.. Floors[Elevator + 1]])).ToArray();
        }
        private Item[]? FindMatchedPairOnFloor()
        {
            var t = Floors[Elevator].GroupBy(t => t.ElementType).Where(g => g.Count() == 2).FirstOrDefault();
            if (t != null)
                return [.. t];
            return null;
        }
        private State Move(int newFloor, Item[] items)
        {
            var newFloors = Floors.Select(t => new List<Item>(t)).ToArray();
            foreach (var item in items)
                newFloors[Elevator].Remove(item);
            newFloors[newFloor].AddRange(items);
            return new State(newFloor, newFloors, Steeps + 1);
        }
        public string FlooreState()
        {
            return string.Concat(Floors.Select((f, i) => $"{i}: {string.Concat(f.Select(i => i.ToString()))}"));
        }
    }

    private record Item
    {
        public Type Type { get; set; }
        public int ElementType { get; set; }
        public bool IsFriedByAny(Item[] item) => Type == Type.Chip && item.Any(i => i.ElementType != ElementType);
        public bool ValidPairToMove(Item item) => item.ElementType == ElementType || (item.Type == Type.Generator && Type == Type.Generator);
    }
    private enum Type
    {
        Chip,
        Generator
    }
}
