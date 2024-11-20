namespace AoC.AoCUtils;

public class GraphPathFinding<T> where T : notnull
{
    public delegate bool IsAllowedDelegate(List<T> visited, T node);
    public delegate int CostDelegate(int currentCost, T node);
    public int BreadthFirst(Graph<T> graph, T start, T end, IsAllowedDelegate IsAllowed )
    {
        var path = new List<T>(){start};
        var queue = new Queue<List<T>>();
        queue.Enqueue(path);
        int paths = 0;
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            if(current.Last().Equals(end))
            {
                paths++;
                continue;
            }
            foreach (var node in graph.Nodes[current.Last()])
            {
                if(IsAllowed(current, node.End))
                {
                    var newPath = new List<T>(current)
                    {
                        node.End
                    };
                    queue.Enqueue(newPath);
                }
            }
        }
        return paths;
    }

    public int DepthFirst (Graph<T> graph, T start, T end, IsAllowedDelegate IsAllowed )
    {
        var path = new List<T>(){start};
        var stack = new Stack<List<T>>();
        stack.Push(path);
        int paths = 0;
        while (stack.Count != 0)
        {
            var current = stack.Pop();
            if(current.Last().Equals(end))
            {
                paths++;
                continue;
            }
            foreach (var node in graph.Nodes[current.Last()])
            {
                if(IsAllowed(current, node.End))
                {
                    var newPath = new List<T>(current)
                    {
                        node.End
                    };
                    stack.Push(newPath);
                }
            }
        }
        return paths;
    }

    public (Dictionary<T, int> costs, Dictionary<T, T> paths) Dijkstra(Dictionary<T, List<T>> graph, T start, CostDelegate costFunc)
    {
        if(start == null)
            throw new ArgumentNullException("start");
        var costs = new Dictionary<T, int>();
        var candidates = new Stack<T>();
        var visited = new HashSet<T>();
        var previous = new Dictionary<T, T>();

        foreach (var n in graph.Keys)
            costs[n] = int.MaxValue;

        costs[start] = 0;
        candidates.Push(start);
        while (candidates.Count != 0)
        {
            T current = candidates.Pop();
            foreach (var node in graph[current].Where(t => !visited.Contains(t)))
            {
                var cost = costFunc(costs[current], node);
                if(cost < costs[node])
                { 
                    costs[node] = cost;
                    previous[node] = current;
                    candidates.Push(node);
                }
            }
            visited.Add(current);
        }

        return (costs, previous);
    }
}
