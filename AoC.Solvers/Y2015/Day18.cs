namespace AoC.Solvers.Y2015;

public class Day18: IDay
{
    public Day18(string input)
    {
        Input = InputParsers.GetInputLines(input);
        Steps = 100;
    }
    public Day18(string input, int steps)
    {
        Input = InputParsers.GetInputLines(input);
        Steps = steps;
    }
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}
    private int Steps { get; init; }

    public int Star1()
    {
        var m = new Matrix<char>(Input, false);
        for (int i = 0; i < Steps; i++)
        {
            var turnOff = m.GetAllPositions().Where(t => t.Value == '#' && m.GetNeighbours(t.Row, t.Column).Count(n => n.Value == '#') != 2 && m.GetNeighbours(t.Row, t.Column).Count(n => n.Value == '#') != 3).ToList();
            var turnOn = m.GetAllPositions().Where(t => t.Value == '.' && m.GetNeighbours(t.Row, t.Column).Count(n => n.Value == '#') == 3).ToList();
            foreach (var l in turnOff)
                m.Update(l.Row, l.Column, t => '.');
            foreach (var l in turnOn)
                m.Update(l.Row, l.Column, t => '#');
        }
        
        return m.GetAllValues().Count(t => t == '#');
    }

    public int Star2()
    {
        var m = new Matrix<char>(Input, false);
        List<(int Row, int Col)> corners = [(0, 0), (0, m.Columns - 1), (m.Rows - 1, 0), (m.Rows - 1, m.Columns - 1)];
        foreach (var corner in corners)
            m.Update(corner.Row, corner.Col, t => '#');

        for (int i = 0; i < Steps; i++)
        {
            var turnOff = m.GetAllPositions().Where(t => t.Value == '#' && m.GetNeighbours(t.Row, t.Column).Count(n => n.Value == '#') != 2 && m.GetNeighbours(t.Row, t.Column).Count(n => n.Value == '#') != 3).ToList();
            var turnOn = m.GetAllPositions().Where(t => t.Value == '.' && m.GetNeighbours(t.Row, t.Column).Count(n => n.Value == '#') == 3).ToList();
            foreach (var l in turnOff.Where(t => !corners.Any(c => c.Row == t.Row && c.Col == t.Column)).ToList())
                m.Update(l.Row, l.Column, t => '.');
            foreach (var l in turnOn)
                m.Update(l.Row, l.Column, t => '#');
        }
        
        return m.GetAllValues().Count(t => t == '#');
    }
}
