namespace AoC.Solvers.Y2024;

public class Day07(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private List<Calibration> Input { get; set; } = InputParsers.GetInputLines(input).Select(t => new Calibration(t.ExtractLongs().First(), t.ExtractLongs().Skip(1).ToArray())).ToList();
    private readonly char[] operators = ['+', '*', '|'];

    public int Star1()
    {
        output = Input.Where(calibration => IsCalibrationTrue(calibration, 2)).Sum(t=> t.TestValue).ToString();
        return -1;
    }

    public int Star2()
    {
        output = Input.Where(calibration => IsCalibrationTrue(calibration, 3)).Sum(t => t.TestValue).ToString();
        return -1;
    }

    private bool IsCalibrationTrue(Calibration calibration, int numbersOfOperators)
    {
        BaseConverter b = new([.. operators.Take(numbersOfOperators)]);
        var numbers = calibration.Numbers.Skip(1).ToArray();
        var possibleOperators = Enumerable.Range(0, (int)Math.Pow(numbersOfOperators, numbers.Length))
            .Select(t => b.ToBase(t).PadLeft(numbers.Length, operators[0]));
        foreach (var op in possibleOperators)
        {
            var sum = calibration.Numbers.First();
            for(int i = 0; i < op.Length; i++)
            {
                if(op[i] == '+')
                    sum += numbers[i];
                else if(op[i] == '*')
                    sum *= numbers[i];
                else
                    sum = long.Parse(string.Concat(sum, numbers[i]));
            }
            if(sum == calibration.TestValue)
                return true;
        }
        return false;
    }

    record Calibration(long TestValue, long[] Numbers);
}
