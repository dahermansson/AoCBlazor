using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day07: IDay
{
    public Day07(string input)
    {
        Input = InputParsers.GetInputLines(input);
    }
    public string Output => throw new NotImplementedException();

    private string[] Input {get;set;}
    //private List<Instruction> Instructions {get; set;}
    private Instruction ParseInstruction(string s)
    {
        var splits = s.Split(" ");
        if(splits.Length == 3)
            return new Instruction(splits[0], null, null, splits[2], "SET");
        if(splits.Length == 4)
            return new Instruction(null, splits[1], null, splits[3], splits[0]);
        return new Instruction(null, splits[0], splits[2], splits[4], splits[1]);
    }
    private record Instruction(string? value, string? w_in1, string? w_in2, string w_out, string? opt)
    {
        public Instruction OverrideB(int overrideValue)
        {
            if(w_out == "b")
                return new Instruction(overrideValue.ToString(), this.w_in1, this.w_in2, this.w_out, this.opt);
            return this;
        }
    }
    private Dictionary<string, int> Wires {get; set;} = new();
    
    private bool RunInstruction(Instruction i)
    {
        int? value = i.value != null ? int.TryParse(i.value, out int v) ? v : Wires.ContainsKey(i.value) ? Wires[i.value] : null : null;
        int? value1 = i.w_in1 != null ? int.TryParse(i.w_in1, out int v1) ? v1 : Wires.ContainsKey(i.w_in1) ? Wires[i.w_in1] : null : null;
        int? value2 = i.w_in2 != null ? int.TryParse(i.w_in2, out int v2) ? v2 : Wires.ContainsKey(i.w_in2) ? Wires[i.w_in2] : null : null;
        

        if(i.opt == "SET" && value != null)
        {
            Wires[i.w_out] = value.Value;
            return true;
        }
        else if(i.opt == "AND" && value1 != null && value2 != null)
        {
            Wires[i.w_out] = value1.Value & value2.Value;
            return true;
        }
        else if(i.opt == "OR" && value1 != null && value2 != null)
        {
            Wires[i.w_out] = value1.Value | value2.Value;
            return true;
        }
        else if(i.opt == "LSHIFT" && value1 != null && value2 != null)
        {    
            Wires[i.w_out] = value1.Value << value2.Value;
            return true;
        }
        else if(i.opt == "RSHIFT" && value1 != null && value2 != null)
        {    
            Wires[i.w_out] = value1.Value >> value2.Value;
            return true;
        }
        else if(i.opt == "NOT" && value1.HasValue)
        {
            Wires[i.w_out] = (ushort)~(ushort)value1.Value;
            return true;
        }
        return false;
    }
    
    public int Star1()
    {
        var instructions = Input.Select(t => ParseInstruction(t)).Reverse().ToList();
        while(instructions.Count>0)
            for (int i = instructions.Count-1;i> -1;i--)
            {
                if(RunInstruction(instructions[i]))
                    instructions.Remove(instructions[i]);
            }
        a_wires = Wires.ContainsKey("a") ? Wires["a"]: Wires["i"];
        return a_wires;
    }

    private int a_wires {get;set;}

    public int Star2()
    {
        Wires.Clear();
        var instructions = Input.Select(t => ParseInstruction(t)).Reverse().ToList();
        
        while(instructions.Count>0)
            for (int i = instructions.Count-1;i> -1;i--)
            {
                if(RunInstruction(instructions[i].OverrideB(a_wires)))
                    instructions.Remove(instructions[i]);
            }
        return Wires["a"];
    }
}
