using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day12: IDay
{
    public Day12(string input)
    {
        Input = input;
    }
    public string Output => throw new NotImplementedException();

    private string Input {get; set;}

    //public int Star1() => Regex.Matches(Input, "-?\\d+").Sum(t => int.Parse(t.Value));
    public int Star1() => TraverseJson(JsonArray.Parse(Input) ?? JsonObject.Parse(Input), false);
    public int Star2() => TraverseJson(JsonArray.Parse(Input) ?? JsonObject.Parse(Input), true);
    
    private int TraverseJson(JsonNode o, bool removeRed)
    {
        int value = 0;
        var valueType = o.GetValueKind();
        if(valueType == System.Text.Json.JsonValueKind.Number)
        {
            value = o.AsValue().GetValue<int>();
            return value;
        }

        if(valueType == System.Text.Json.JsonValueKind.Object)
        {
            if(removeRed && o.AsObject().Any(t => t.Value != null && t.Value.GetValueKind() == System.Text.Json.JsonValueKind.String && t.Value.GetValue<string>() == "red"))
                return 0;
            foreach (var node in o.AsObject().Where(t => t.Value != null))
            {
                if (node.Value!.GetValueKind() == System.Text.Json.JsonValueKind.Number)
                    value += node.Value.GetValue<int>();
                else
                    value += TraverseJson(node.Value, removeRed);
            }
        }
        if(valueType == System.Text.Json.JsonValueKind.Array)
            foreach (var node in o.AsArray())
            {   
                if( node != null)
                    value += TraverseJson(node, removeRed);
            }
        return value;
    }
}
