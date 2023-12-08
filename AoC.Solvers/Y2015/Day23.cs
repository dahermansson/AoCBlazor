using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day23 : IDay
{
    public Day23(string input) => Input = Parse(input);
    public Day23(string input, string register)
    {
        Register = register;
        Input = Parse(input);
    }
    private Dictionary<int, Insruction> Parse(string input) => InputParsers.GetInputLines(input).Select((t, i) =>
        {
            var s = t.Split(" ");
            int off = s.Length == 3 ? int.Parse(s[2], System.Globalization.NumberStyles.AllowLeadingSign) : 0;
            off = s[0] == "jmp" ? int.Parse(s[1], System.Globalization.NumberStyles.AllowLeadingSign) : off;
            return new KeyValuePair<int, Insruction>(i, new Insruction(s[0], s[1].Trim(','), off));
        }).ToDictionary();

    public string Output => throw new NotImplementedException();
    private Dictionary<int, Insruction> Input { get; set; }
    private Dictionary<string, int> Memory { get; set; } = new Dictionary<string, int>();
    private string Register { get; set; } = "b";
    public int Star1()
    {
        Memory["a"] = 0;
        Memory["b"] = 0;
        Run(0);
        return Memory[Register];
    }
    public int Star2()
    {
        Memory["a"] = 1;
        Memory["b"] = 0;
        Run(0);
        return Memory[Register];
    }

    private void Run(int pointer)
    {
        while (Input.ContainsKey(pointer))
            pointer = Execute((pointer, Input[pointer])).Invoke();
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