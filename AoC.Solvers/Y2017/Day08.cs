namespace AoC.Solvers.Y2017;

public class Day08(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; init; } = InputParsers.GetInputLines(input);

    private static bool ConditionIsTrue((int condRegisterValue, string @operator, int conditionValue) cond) => cond switch
        {
            { @operator: "==" } => cond.condRegisterValue == cond.conditionValue,
            { @operator: "!=" } => cond.condRegisterValue != cond.conditionValue,
            { @operator: ">" } => cond.condRegisterValue > cond.conditionValue,
            { @operator: "<" } => cond.condRegisterValue < cond.conditionValue,
            { @operator: ">=" } => cond.condRegisterValue >= cond.conditionValue,
            { @operator: "<=" } => cond.condRegisterValue <= cond.conditionValue,
            _ => false
        };

    private List<Instruction> Instructions => Input.Select(t =>
        {
            var s = t.Split(" ");
            return new Instruction(s[0], s[1], int.Parse(s[2]), s[4], s[5], int.Parse(s[6]));
        }).ToList();

    public int Star1()
    {
        Dictionary<string, int> register = Instructions.Select(t => (t.Register, 0)).DistinctBy(t => t.Register).ToDictionary();

        Instructions.ForEach(i => 
        {
            if(ConditionIsTrue((register[i.ConditionRegister], i.Operator, i.ConditionValue)))
            {
                if(i.IncOrDec == "inc")
                {
                    register[i.Register] += i.Value;
                }
                else if(i.IncOrDec == "dec")
                {
                    register[i.Register] -= i.Value;
                }
            }
        });

        return register.Values.Max();
    }

    public int Star2()
    {
        Dictionary<string, int> register = Instructions.Select(t => (t.Register, 0)).DistinctBy(t => t.Register).ToDictionary();
    
        var highestValue = 0;
        Instructions.ForEach(i => 
        {
            if(ConditionIsTrue((register[i.ConditionRegister], i.Operator, i.ConditionValue)))
            {
                if(i.IncOrDec == "inc")
                {
                    register[i.Register] += i.Value;
                }
                else if(i.IncOrDec == "dec")
                {
                    register[i.Register] -= i.Value;
                }
            }

            if(register.Values.Max() > highestValue)
            {
                highestValue = register.Values.Max();
            }
        });

        return highestValue;
    }

    private record Instruction(string Register, string IncOrDec, int Value, string ConditionRegister, string Operator, int ConditionValue);
}