using System.Text.Json.Nodes;

namespace AoC.Solvers.Y2015;

public class Day12(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    //public int Star1() => Regex.Matches(Input, "-?\\d+").Sum(t => int.Parse(t.Value));
    public int Star1()
    {
        var json = JsonArray.Parse(Input) ?? JsonObject.Parse(Input) ?? throw new FormatException("Input is not Json-format");
        return TraverseJson(json, false);
    }
    public int Star2()
    {
        var json = JsonArray.Parse(Input) ?? JsonObject.Parse(Input) ?? throw new FormatException("Input is not Json-format");
        return TraverseJson(json, true);
    }
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
