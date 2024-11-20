namespace AoC.Solvers.Y2021;

public class Day15(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    public Matrix<int> Matrix = new(InputParsers.GetInputLines(input), false);

    public int Star1()
    {
        MatrixPathFinding<int> graph = new(Matrix);
        var start = Matrix.Get(0, 0);
        var end = Matrix.Get(Matrix.Rows - 1, Matrix.Columns - 1);
        graph.Dijkstra(start, (t, p) => p.Value);
        return graph.GetCost(end);
    }

    public int Star2()
    {
        var orgRows = Matrix.Rows;
        var orgCols = Matrix.Columns;
        for (int i = 0; i < 4; i++)
            Matrix = Matrix.ExpandToRigth(i * orgCols, orgRows, orgCols, t => t + 1 == 10 ? 1 : t + 1);
        for (int i = 0; i < 4; i++)
            Matrix = Matrix.ExpandDown(i * orgRows, orgRows, Matrix.Columns, t => t + 1 == 10 ? 1 : t + 1);

        MatrixPathFinding<int> graph = new MatrixPathFinding<int>(Matrix);
        var start = Matrix.Get(0, 0);
        var end = Matrix.Get(Matrix.Rows - 1, Matrix.Columns - 1);
        graph.Dijkstra(start, (t, p)=> p.Value);
        return graph.GetCost(end);
    }
}