namespace AoC.AoCUtils;

public class TreeNode<T>(T item) where T: notnull
{
    public TreeNode<T> Parent { get; set; } = null!;
    public List<TreeNode<T>> Children { get; set; } = [];
    public T Item { get; set; } = item;
    public TreeNode<T> AddChild(T item)
    {
        var child = new TreeNode<T>(item){ Parent = this};
        Children.Add(child);
        return child;
    }

    public void GetTreeNodes(ref HashSet<TreeNode<T>> nodes)
    {
        _ = nodes.Add(this);
        foreach (var child in Children)
            child.GetTreeNodes(ref nodes);
    }

    public IEnumerable<T> EnumerateNodes()
    {
        yield return Item;
        foreach (var child in Children)
            child.EnumerateNodes();
    }
}