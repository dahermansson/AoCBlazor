namespace AoC.AoCUtils;

public class Graph<T> where T: notnull
{
    public Dictionary<T, HashSet<Edge<T>>> Nodes {get; set;} = [];
    public Graph(IEnumerable<T> nodes, IEnumerable<Edge<T>> edges, bool doubleDirected = true)
    {
        foreach (var node in nodes)
            Nodes[node] = [];

        foreach (var edge in edges)
        {
            Nodes[edge.Start].Add(edge);
            if(doubleDirected)
                Nodes[edge.End].Add(new Edge<T>(edge.End, edge.Start));
        }
    }
}

public class Edge<T>(T start, T end)
{
    public T Start { get; set; } = start;
    public T End { get; set; } = end;
}