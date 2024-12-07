namespace AoC.Solvers.Y2024;

public class Day07(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private List<Calibration> Input { get; set; } = InputParsers.GetInputLines(input).Select(t => new Calibration(t.ExtractLongs().First(), t.ExtractLongs().Skip(1).ToArray())).ToList();
    private readonly char[] operators = ['+', '*', '|'];

    public int Star1()
    {
        output = Input.Where(calibration => IsCalibrationTrueRecStar1(calibration.TestValue, calibration.Numbers[0], calibration.Numbers[1..])).Sum(t => t.TestValue).ToString();
        return -1;
    }

    public int Star2()
    {
        output = Input.Where(calibration => IsCalibrationTrueRecStar2(calibration.TestValue, calibration.Numbers[0], calibration.Numbers[1..])).Sum(t => t.TestValue).ToString();
        return -1;
    }

    private static bool IsCalibrationTrueRecStar2(long testValue, long sum, long[] values)
    {
        if (values is []) return testValue == sum;
        if (sum > testValue) return false;
        return IsCalibrationTrueRecStar2(testValue, sum + values[0], values[1..])
            || IsCalibrationTrueRecStar2(testValue, sum * values[0], values[1..])
            || IsCalibrationTrueRecStar2(testValue, long.Parse($"{sum}{values[0]}"), values[1..]);
    }

    private static bool IsCalibrationTrueRecStar1(long testValue, long sum, long[] values)
    {
        if (values is []) return testValue == sum;
        if (sum > testValue) return false;
        return IsCalibrationTrueRecStar1(testValue, sum + values[0], values[1..])
            || IsCalibrationTrueRecStar1(testValue, sum * values[0], values[1..]);
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
            for (int i = 0; i < op.Length; i++)
            {
                if (op[i] == operators[0])
                    sum += numbers[i];
                else if (op[i] == operators[1])
                    sum *= numbers[i];
                else
                    sum = long.Parse(string.Concat(sum, numbers[i]));
            }
            if (sum == calibration.TestValue)
                return true;
        }
        return false;
    }

    record Calibration(long TestValue, long[] Numbers);
}
