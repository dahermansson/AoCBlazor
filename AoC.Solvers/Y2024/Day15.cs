using Microsoft.Toolkit.HighPerformance;

namespace AoC.Solvers.Y2024;

public class Day15(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = input.Split($"{Environment.NewLine}{Environment.NewLine}");
    private string Instructions {get; init;} = input.Split($"{Environment.NewLine}{Environment.NewLine}").Last().Replace(Environment.NewLine, "");
    private char[,]? Warehouse { get; set;}

    private record Pos(int X, int Y);

    public int Star1()
    {
        Warehouse = new char[Input.First().Split(Environment.NewLine).Count(), Input.First().Split(Environment.NewLine)[0].Length];
        Pos robotPos = new(0,0);
        input.Split($"{Environment.NewLine}{Environment.NewLine}").First().Split(Environment.NewLine).TakeWhile(t => t.Length != 0).SelectMany((row, rowIndex) => 
        {
            return row.Select((value, columnIndex) => (rowIndex, columnIndex, value));
        }).ToList().ForEach(o => {
            Warehouse[o.rowIndex, o.columnIndex] = o.value;
            if(o.value == '@')
                robotPos = new(o.rowIndex, o.columnIndex);
        });

        
        Pos MoveRight(Pos robot)
        {
            Span2D<char> warehouse = Warehouse;
            var moveTo =  warehouse.GetRow(robot.X).ToArray().Skip(robot.Y).TakeWhile(t => t == '@'|| t == '.' || t == 'O').ToList().FindIndex(t => t == '.')+1;
            if(moveTo == 0)
                return robot;
            var row = warehouse.GetRowSpan(robot.X).Slice(robot.Y, moveTo);
            char[] moved = ['.', .. row.ToArray().SkipLast(1)];
            for(int i = 0; i < moved.Length; i++ )
                row[i] = moved[i];
            return robot with { Y = robot.Y+1};
        }

        Pos MoveDown(Pos robot)
        {
            Span2D<char> warehouse = Warehouse;
            var moveTo =  warehouse.GetColumn(robot.Y).ToArray().Skip(robot.X).TakeWhile(t =>  t == '@'||  t ==  '.' || t == 'O').ToList().FindIndex(t => t == '.')+1;
            if(moveTo == 0)
                return robot;
            var column = warehouse.Slice(robot.X, robot.Y, moveTo, 1).GetColumn(0);
            char[] moved = ['.', .. column.ToArray().SkipLast(1)];
            for(int i = 0; i < moved.Length; i++ )
                column[i] = moved[i];
            return robot with { X = robot.X+1};
        }

        Pos MoveLeft(Pos robot)
        {
            Span2D<char> warehouse = Warehouse;
            var moveTo =  warehouse.GetRow(robot.X).ToArray().Reverse().SkipWhile(t => t != '@').Skip(1).TakeWhile(t => t == '.' || t == 'O').ToList().FindIndex(t => t == '.')+1;
            if(moveTo == 0)
                return robot;
            var row = warehouse.GetRowSpan(robot.X).Slice(robot.Y - moveTo, moveTo+1);
            char[] moved = [.. row.ToArray().Skip(1), '.'];
            for(int i = 0; i < moved.Length; i++ )
                row[i] = moved[i];
            return robot with { Y = robot.Y-1};
        }

        Pos MoveUp(Pos robot)
        {
            Span2D<char> warehouse = Warehouse;
            var moveTo =  warehouse.GetColumn(robot.Y).ToArray().Reverse().SkipWhile(t => t != '@').Skip(1).TakeWhile(t => t == '.' || t == 'O').ToList().FindIndex(t => t == '.')+1;
            if(moveTo == 0)
                return robot;
            var column = warehouse.Slice(robot.X - moveTo, robot.Y, moveTo+1, 1).GetColumn(0);
            char[] moved = [.. column.ToArray().Skip(1), '.'];
            for(int i = 0; i < moved.Length; i++ )
                column[i] = moved[i];
            return robot with { X = robot.X-1};
        }
        
        Pos Move(Pos robot, char i) => i switch
        {
            '>' => MoveRight(robot),
            'v' => MoveDown(robot),
            '<' => MoveLeft(robot),
            '^' => MoveUp(robot),
            _ => robot
        };

        new Span2D<char>(Warehouse).Span2DToConsoleOutput();

        Instructions.ToList().ForEach(i => {
            robotPos = Move(robotPos, i);
            //new Span2D<char>(Warehouse).Span2DToConsoleOutput();
        });
        
        var warehouse = new Span2D<char>(Warehouse);
        int sumOfBoxesGPSCoordinates = 0;
        for (int rowIndex = 0; rowIndex < warehouse.Height; rowIndex++)
        {
            var row = warehouse.GetRowSpan(rowIndex);
            for(int columnIndex = 0; columnIndex < row.Length;columnIndex++)
                sumOfBoxesGPSCoordinates += row[columnIndex] == 'O' ? 100 * rowIndex + columnIndex : 0;
        }

        return sumOfBoxesGPSCoordinates;
    }

    public int Star2()
    {
        return 0;
    }
}
