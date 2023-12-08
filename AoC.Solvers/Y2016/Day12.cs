using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day12 : IDay
{
    public Day12(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; }

    public int Star1()
    {
        var c = new Computer(Input);
        c.Run();
        return c.Register["a"];
    }

    public int Star2()
    {
        var c = new Computer(Input);
        c.Register["c"] = 1;
        c.Run();
        return c.Register["a"];
    }

    private class Computer
    {
        public Dictionary<string, int> Register { get; set; } = new() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        private int Pointer { get; set; } = 0;
        private string[] Instructions { get; set; }
        public Computer(string[] instructions)
        {
            Instructions = instructions;
        }
        public void Run()
        {
            while (Pointer > -1 && Pointer < Instructions.Length)
            {
                var inst = Instructions[Pointer].Split(" ");
                if (inst[0] == "cpy")
                    Pointer = Cpy(Pointer, inst[1], inst[2]);
                if (inst[0] == "jnz")
                    Pointer = Jnz(Pointer, inst[1], inst[2]);
                if (inst[0] == "inc")
                    Pointer = Inc(Pointer, inst[1]);
                if (inst[0] == "dec")
                    Pointer = Dec(Pointer, inst[1]);
            }
        }

        public int Cpy(int p, string x, string y)
        {
            if (int.TryParse(x, out int v))
                Register[y] = v;
            else
                Register[y] = Register[x];
            return p + 1;
        }
        public int Inc(int p, string a)
        {
            Register[a] += 1;
            return p + 1;
        }
        public int Dec(int p, string a)
        {
            Register[a] -= 1;
            return p + 1;
        }
        public int Jnz(int p, string x, string y)
        {
            if (int.TryParse(x, out int v) && v != 0 || Register[x] != 0)
                return p + int.Parse(y, System.Globalization.NumberStyles.AllowLeadingSign);
            return p + 1;
        }
    }
}
