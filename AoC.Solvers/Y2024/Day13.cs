namespace AoC.Solvers.Y2024;

public class Day13(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;

    private string[] Input { get; set; } = input.Split($"{Environment.NewLine}{Environment.NewLine}");

    record Machine
    {
        public Machine(string[] inputs)
        {
            ButtonA = (inputs[0].ExtractIntegers().First(), inputs[0].ExtractIntegers().Last());
            ButtonB = (inputs[1].ExtractIntegers().First(), inputs[1].ExtractIntegers().Last());
            Prize = (inputs[2].ExtractIntegers().First(), inputs[2].ExtractIntegers().Last());
        }
        public (int x, int y) ButtonA {get; set;}
        public (int x, int y) ButtonB {get; set;}
        public (long x, long y) Prize {get; set;}

        public long CostForWin(bool maxHundred)
        {
            var x = ButtonA.x * ButtonB.y;
            var y = ButtonB.x * ButtonA.y;

            var diff = x - y;
            
            var buttonA = (Prize.x * ButtonB.y) - (Prize.y * ButtonB.x);
            
            var numbersOfA = buttonA / diff;

            var buttonB = Prize.x - (ButtonA.x * numbersOfA);
            var numbersOfB = buttonB / ButtonB.x;

            if (maxHundred && (numbersOfA > 100 || numbersOfB > 100))
                return 0;

            if (((ButtonA.x * numbersOfA) + (ButtonB.x * numbersOfB)) != Prize.x || ((ButtonA.y * numbersOfA) + (ButtonB.y * numbersOfB)) != Prize.y)
                return 0;

            return (numbersOfA * 3) + (numbersOfB * 1);
        }
    }

    public int Star1()
    {
        var machines = Input.Select(t => new Machine(t.Split(Environment.NewLine)));
        output = machines.Sum(t => t.CostForWin(true)).ToString();
        return -1;
    }

    public int Star2()
    {
        var machines = Input.Select(t => new Machine(t.Split(Environment.NewLine)))
            .Select(t => t with {Prize = (t.Prize.x + 10000000000000, t.Prize.y + 10000000000000)});
        output = machines.Sum(t => t.CostForWin(false)).ToString();
        return -1;
    }
}
