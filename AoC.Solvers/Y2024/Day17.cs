namespace AoC.Solvers.Y2024;

public class Day17(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var registerA = Input.Take(3).Select(t => t.ExtractIntegers().Single()).First();

        long[] program = [.. Input.Last().ExtractIntegers()];

        var res = new ChronospatialComputer(program, registerA).RunProgram();

        output = string.Join(',', res);
        return -1;
    }

    public int Star2()
    {
        long[] program = [.. Input.Last().ExtractIntegers()];

        long[] Run(long a)
        {
            var res = new ChronospatialComputer(program, a).RunProgram();
            return [.. res];
        }

        long value = 0;
        for (var i = 0; i < program.Length; i++)
        {
            var num = value * 8;
            for (var j = 0; ; j++)
            {
                var values = Run(num + j);
                if (program.TakeLast(values.Length).SequenceEqual(values))
                {
                    value = num + j;
                    break;
                }
            }
        }

        output = value.ToString();
        return -1;
    }

    class ChronospatialComputer(long[] program, long a)
    {
        private long A { get; set; } = a;
        private long B { get; set; }
        private long C { get; set; }
        private long Pointer { get; set; } = 0;
        private List<long> Out { get; set; } = [];

        public List<long> RunProgram()
        {
            long Do(long pointer)
            {
                long Combo(long c) => c switch
                {
                    <= 3 => c,
                    4 => A,
                    5 => B,
                    6 => C,
                    _ => -1
                };

                Func<long> instr = program[pointer] switch
                {
                    0 => () =>
                    {
                        A = (long)(A / Math.Pow(2, Combo(program[pointer + 1])));
                        return pointer + 2;
                    }
                    ,
                    1 => () =>
                    {
                        B ^= program[pointer + 1];
                        return pointer + 2;
                    }
                    ,
                    2 => () =>
                    {
                        B = Combo(program[pointer + 1]) % 8;
                        return pointer + 2;
                    }
                    ,
                    3 => () => A == 0 ? pointer + 2 : program[pointer + 1],
                    4 => () =>
                    {
                        B ^= C;
                        return pointer + 2;
                    }
                    ,
                    5 => () =>
                    {
                        Out.Add(Combo(program[pointer + 1]) % 8);
                        return pointer + 2;
                    }
                    ,
                    6 => () =>
                    {
                        B = (long)(A / Math.Pow(2, Combo(program[pointer + 1])));
                        return pointer + 2;
                    }
                    ,
                    7 => () =>
                    {
                        C = (long)(A / Math.Pow(2, Combo(program[pointer + 1])));
                        return pointer + 2;
                    }
                    ,
                    _ => () => 1
                };
                return instr();
            }

            while (Pointer < program.Length)
            {
                Pointer = Do(Pointer);
            }
            return Out;
        }
    }
}
