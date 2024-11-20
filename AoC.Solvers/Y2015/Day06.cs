using AoC.AoCUtils;
using Microsoft.Toolkit.HighPerformance;
namespace AoC.Solvers.Y2015;

public class Day06(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        Span2D<bool> lights = new bool[1000,1000];

        foreach (var instruction in Input.Select(t => ParseRow(t)))
        {
            if(instruction.Toggle)
                Toggle(lights, instruction);
            else
                TurnOnOff(lights, instruction);
        }


        return lights.ToArray().Cast<bool>().Count(t => t);
    }

    public int Star2()
    {
        Span2D<int> lights = new int[1000,1000];

        foreach (var instruction in Input.Select(t => ParseRow(t)))
            IncDec(lights, instruction);
        return lights.ToArray().Cast<int>().Sum(t => t);
    }

    private void TurnOnOff(Span2D<bool> lights, Instruction i)
    {
        var rowCount = i.EndRow-i.StartRow+1;
        var colCount = i.EndCol-i.StartCol+1;
        if(rowCount == 0)
            rowCount = 1;
        if(colCount == 0)
            colCount = 1;
        var box = lights.Slice(i.StartRow, i.StartCol, rowCount, colCount);
        box.Fill(i.OnOrOff);
    }

    private void Toggle(Span2D<bool> lights, Instruction i)
    {
        var rowCount = i.EndRow-i.StartRow+1;
        var colCount = i.EndCol-i.StartCol+1;
        if(rowCount == 0)
            rowCount = 1;
        if(colCount == 0)
            colCount = 1;
        var box = lights.Slice(i.StartRow, i.StartCol, rowCount, colCount);
        for(int row=0;row < box.Height;row++)
            for(int col=0;col < box.Width;col++)
                box[row, col] = !box[row, col];
    }

    private void IncDec(Span2D<int> lights, Instruction i)
    {
        var rowCount = i.EndRow-i.StartRow+1;
        var colCount = i.EndCol-i.StartCol+1;
        if(rowCount == 0)
            rowCount = 1;
        if(colCount == 0)
            colCount = 1;
        var box = lights.Slice(i.StartRow, i.StartCol, rowCount, colCount);
        for(int row=0;row < box.Height;row++)
            for(int col=0;col < box.Width;col++)
                if(i.Toggle)
                    box[row, col] = box[row, col]+2;
                else
                    if(i.OnOrOff)
                        box[row, col] = box[row, col]+1;
                    else
                        box[row, col] = box[row, col] == 0 ? 0 : box[row, col] -1;
    }

    private Instruction ParseRow(string row)
    {
        var splits = row.Split(" ");
        var startRow = int.Parse(splits[^3].Split(",")[0]);
        var startCol = int.Parse(splits[^3].Split(",")[1]);
        var endRow = int.Parse(splits[^1].Split(",")[0]);
        var endCol = int.Parse(splits[^1].Split(",")[1]);
        
        if(row.StartsWith("turn"))
            return new Instruction(startRow, startCol, endRow, endCol, false, row.Contains("on"));
        return new Instruction(startRow, startCol, endRow, endCol, true, false);
        
    }

    private record Instruction(int StartRow, int StartCol, int EndRow, int EndCol, bool Toggle, bool OnOrOff);
}
