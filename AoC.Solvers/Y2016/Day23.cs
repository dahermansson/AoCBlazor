namespace AoC.Solvers.Y2016;

public class Day23(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var computer = new Computer(Input);
        computer.Register["a"] = 7;
        computer.Run();
        return computer.Register["a"];
    }

    public int Star2()
    {
        var computer = new Computer(Input);
        computer.Register["a"] = 12;
        computer.Run();
        return computer.Register["a"];
    }

    private class Computer(string[] instructions)
    {
        public Dictionary<string, int> Register { get; set; } = new() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        private int Pointer { get; set; } = 0;
        private string[] Instructions { get; set; } = [.. instructions];

        public void Run()
        {
            while (Pointer > -1 && Pointer < Instructions.Length)
            {
                if (Pointer == 4) //Just skip the multiply loop and multiply
                {
                    Register["a"] = Register["b"] * Register["d"];
                    Register["c"] = 0;
                    Register["d"] = 0;
                    Pointer += 6;
                }
                var inst = Instructions[Pointer].Split(" ");
                if (inst[0] == "cpy")
                    Pointer = Cpy(Pointer, inst[1], inst[2]);
                else if (inst[0] == "jnz")
                    Pointer = Jnz(Pointer, inst[1], inst[2]);
                else if (inst[0] == "inc")
                    Pointer = Inc(Pointer, inst[1]);
                else if (inst[0] == "dec")
                    Pointer = Dec(Pointer, inst[1]);
                else if (inst[0] == "tgl")
                    Pointer = Tgl(Pointer, inst[1]);
            }
        }

        public int Cpy(int p, string x, string y)
        {
            if (int.TryParse(y, out int _))
                return p + 1;
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
            if (!int.TryParse(y, out int j))
                j = Register[y];
            if (int.TryParse(x, out int v) && v != 0 || Register[x] != 0)
                return p + j;
            return p + 1;
        }

        public int Tgl(int p, string a)
        {
            var togleInstruction = p + Register[a];
            if (togleInstruction < 0 || togleInstruction >= Instructions.Length)
                return p + 1;
            if (togleInstruction == 0)
            {
                Instructions[togleInstruction] = Instructions[togleInstruction].Replace("tgl", "inc");
                return p + 1;
            }
            var togle = Instructions[togleInstruction].Split(" ");
            if (togle.Length == 2)
            {
                if (togle[0] == "inc")
                {
                    Instructions[togleInstruction] = Instructions[togleInstruction].Replace("inc", "dec");
                    return p + 1;
                }
                else
                {
                    Instructions[togleInstruction] = Instructions[togleInstruction].Replace(togle[0], "inc");
                    return p + 1;
                }
            }
            if (togle.Length == 3)
            {
                if (togle[0] == "jnz")
                {
                    Instructions[togleInstruction] = Instructions[togleInstruction].Replace("jnz", "cpy");
                    return p + 1;
                }
                else
                {
                    Instructions[togleInstruction] = Instructions[togleInstruction].Replace(togle[0], "jnz");
                    return p + 1;
                }
            }
            return p + 1;
        }
    }
}
