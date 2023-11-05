using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day23 : IDay
{
    public Day23(string input)
    {
        Input = InputParsers.GetInputLines(input).Select((t, i) =>
        {
            var s = t.Split(" ");
            int off = s.Length == 3 ? int.Parse(s[2], System.Globalization.NumberStyles.AllowLeadingSign) : 0;
            off = s[0] == "jmp" ? int.Parse(s[1], System.Globalization.NumberStyles.AllowLeadingSign) : off;
            return new KeyValuePair<int, Insruction>(i, new Insruction(s[0], s[1].Trim(','), off));
        }).ToDictionary();
    }
    public Day23(string input, string register)
    {
        Register = register;
        Input = InputParsers.GetInputLines(input).Select((t, i) =>
        {
            var s = t.Split(" ");
            int off = s.Length == 3 ? int.Parse(s[2], System.Globalization.NumberStyles.AllowLeadingSign) : 0;
            off = s[0] == "jmp" ? int.Parse(s[1], System.Globalization.NumberStyles.AllowLeadingSign) : off;
            return new KeyValuePair<int, Insruction>(i, new Insruction(s[0], s[1].Trim(','), off));
        }).ToDictionary();
    }
    public string Output => throw new NotImplementedException();
    private Dictionary<int, Insruction> Input { get; set; }
    private Dictionary<string, int> Memory { get; set; } = [];
    private string Register { get; set; } = "b";
    public int Star1()
    {
        Memory.Add("a", 0);
        Memory.Add("b", 0);
        var pointer = 0;
        while (Input.ContainsKey(pointer))
            pointer = Execute((pointer, Input[pointer])).Invoke();
        return Memory[Register];
    }
    public int Star2()
    {
        Memory.Clear();
        Memory.Add("a", 1);
        Memory.Add("b", 0);
        var pointer = 0;
        while (Input.ContainsKey(pointer))
            pointer = Execute((pointer, Input[pointer])).Invoke();
        return Memory[Register];
    }

    private Func<int> Execute((int pointer, Insruction inst) t) => t.inst.Ins switch
    {
        "hlf" => () => { Memory[t.inst.Register] /= 2; return t.pointer + 1; },
        "tpl" => () => { Memory[t.inst.Register] *= 3; return t.pointer + 1; },
        "inc" => () => { Memory[t.inst.Register] += 1; return t.pointer + 1; },
        "jmp" => () => t.pointer + t.inst.Offset,
        "jie" => () => Memory[t.inst.Register] % 2 == 0 ? t.pointer + t.inst.Offset : t.pointer + 1,
        "jio" => () => Memory[t.inst.Register] == 1 ? t.pointer + t.inst.Offset : t.pointer + 1,
        _ => () => -1
    };
    public record Insruction(string Ins, string Register, int Offset);
}
