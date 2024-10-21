namespace AoC.Solvers.Y2016;

public class Day25(string input) : IDay
{
    public string Output { get; set;} = string.Empty;
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        int testValue = 0;
        string validSignal = "01010101";
        Computer computer;
        do
        {
            computer = new Computer(Input);
            computer.Register["a"] = ++testValue;
            computer.Run();
        } while (string.Concat(computer.Outputs) != validSignal);
        return testValue;
    }

    public int Star2()
    {
        Output = string.Concat(Enumerable.Repeat("*", 50));
        return -1;
    }

    private class Computer
    {
        public Dictionary<string, int> Register { get; set; } = new() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        private int Pointer { get; set; } = 0;
        private string[] Instructions { get; set; }
        public List<int> Outputs { get; set; } = [];
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
                if (inst[0] == "out")
                    Pointer = Out(Pointer, inst[1]);

                if (InvalidOutput())
                    break;
            }
        }

        private bool InvalidOutput()
        {
            var strOut = string.Concat(Outputs);
            if (strOut.Contains("00") || strOut.Contains("11"))
                return true;
            if (strOut.Length > 7)
                return true;
            return false;
        }


        private int Out(int pointer, string v)
        {
            Outputs.Add(int.TryParse(v, out int value) ? value : Register[v]);
            return pointer + 1;
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
            if (int.TryParse(x, out int v) )
            {
                if(v != 0)
                    return p + int.Parse(y, System.Globalization.NumberStyles.AllowLeadingSign);
            }
            else if (Register[x] != 0)
                return p + int.Parse(y, System.Globalization.NumberStyles.AllowLeadingSign);
            return p + 1;
        }
    }
}
