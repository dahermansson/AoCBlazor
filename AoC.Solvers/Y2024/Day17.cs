using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Identity.Client;

namespace AoC.Solvers.Y2024;

public class Day17(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var initRegister = Input.Take(3).Select(t => t.ExtractIntegers().Single()).ToArray();
        long[] program = [..Input.Last().ExtractIntegers()];
        var c = new ChronospatialComputer(program, initRegister[0]);
        c.RunProgram();
        Console.WriteLine("A: " + c.A);
        Console.WriteLine("B: " + c.B);
        Console.WriteLine("C: " + c.C);
        Console.WriteLine(string.Join(',', c.Out));

        return 0;
    }

    public int Star2()
    {
        long[] program = [..Input.Last().ExtractIntegers()];

        long[] Run(long a)
        {
            var c = new ChronospatialComputer(program, a);
            c.RunProgram();
            return [..c.Out];
        }

        var test2 = 0L;
        for (var i = 0; i < program.Length; i++)
		{
			var num = test2 * 8;
			for (var j = 0; ; j++)
			{
				var values = Run(num + j);
				if (program.TakeLast(values.Length).SequenceEqual(values))
				{
					test2 = num + j;
					break;
				}
			}
		}

        var o = test2.ToString();

        






        List<long> FindStuff(long curVal, int depth)
        {
            List<long> res = new();
            if (depth > program.Length) return res;
            var tmp = curVal << 3;
            for(int i = 0; i < 8; i++)
            {
                var tmpRes = Run(tmp + i);
                if (tmpRes.SequenceEqual(program.TakeLast(depth + 1)))
                {
                    if (depth + 1 == program.Length) res.Add(tmp + i);
                    res.AddRange(FindStuff(tmp + i, depth + 1));
                }
            }
            return res;
        }
            
        var aoeu = FindStuff(0,0);
        
        
        var k = FindStuff(0,0).Min();
        return 0;
    }

    class ChronospatialComputer(long[] program, long a = 0, long b = 0, long c = 0)
    {
        public long A { get; set; } = a;
        public long B { get; set; } = b;
        public long C { get; set; } = c;
        public long Pointer { get; set;} = 0;
        public List<long> Out {get; set;} = [];
        

        public void RunProgram()
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

                Func<long> instr = program[pointer] switch {
                    0 =>() => {
                        A = (int)(A / Math.Pow(2, Combo(program[pointer+1])));
                        return pointer +2;
                    },
                    1 =>() => {
                        B ^= program[pointer+1];
                        return pointer +2;
                    },
                    2 =>() => {
                        B = Combo(program[pointer+1]) % 8;
                        return pointer +2;
                    },
                    3 =>() => {
                        return A == 0 ? pointer + 2 : program[pointer+1];
                    },
                    4 =>() => {
                        B ^= C;
                        return pointer + 2;
                    },
                    5 =>() => {
                        Out.Add(Combo(program[pointer+1]) % 8);
                        return pointer + 2;
                    },
                    6 =>() => {
                        B = (int)(A / Math.Pow(2, Combo(program[pointer+1])));
                        return pointer +2;
                    },
                    7 =>() => {
                        C = (int)(A / Math.Pow(2, Combo(program[pointer+1])));
                        return pointer +2;
                    },
                    _ => () => 1
                };
                return instr();
            }

            while(Pointer < program.Length)
            {
                 Pointer = Do(Pointer);
            }
        }
    }
}
