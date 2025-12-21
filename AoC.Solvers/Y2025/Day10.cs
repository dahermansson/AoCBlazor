using Microsoft.Z3;

namespace AoC.Solvers.Y2025;

public class Day10(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var machines = Input.Select(t =>
        {
            var splits = t.Split(" ");
            var leds = splits[0].Trim('[', ']').Select(t => t != '.').ToArray();
            return new Machine(leds,
                splits.Skip(1).SkipLast(1).Select(b =>
                {
                    var ints = b.Trim('(', ')');
                    return new Button(ints.Split(',').Select(int.Parse).ToList(), 0, Enumerable.Range(0, leds.Length).Select(t => false).ToArray(), []);//new string('.', leds.Length));
                }).ToList());
        });

        return machines.Select(PressButtons).Sum();
    }

    private int PressButtons(Machine machine)
    {
        Queue<Button> buttons = [];
        machine.Buttons.ForEach(buttons.Enqueue);

        Button? currentButton = default;
        while (new BitArray(machine.Leds).ToInt64() != new BitArray(currentButton?.Leds ?? []).ToInt64())
        {
            currentButton = buttons.Dequeue();
            currentButton = currentButton.Press();
            machine.Buttons.Where(t => !currentButton.PressedButtons.Contains(t.ButtonIdentifier))
                .Select(b => b with { Leds = currentButton.Leds, Presses = currentButton.Presses, PressedButtons = [.. currentButton.PressedButtons] })
                .ToList()
                .ForEach(buttons.Enqueue);
        }
        return currentButton?.Presses ?? 0;
    }

    public int Star2()
    {
        int sum = 0;
        foreach (var machineLine in Input)
        {
            var machine = machineLine.Split(" ");
            var buttons = machine[1..^1].Select(p => p.Where(char.IsDigit).Select(i => int.Parse(i.ToString())).ToList()).ToList();
            var joltages = machine.Last().Trim('{', '}').Split(',').Select(i => int.Parse(i.ToString())).ToList();


            var context = new Context();
            var optimizer = context.MkOptimize();

            var z3Buttons = new ArithExpr[buttons.Count];
            for (var i = 0; i < buttons.Count; ++i)
            {
                z3Buttons[i] = context.MkIntConst($"button_{i}");
                optimizer.Add(context.MkGe(z3Buttons[i], context.MkInt(0)));
            }

            foreach (var joltage in joltages.Index())
            {
                List<ArithExpr> buttonToPresse = [];
                var buttons_affecting_joltage = buttons.Index().Where(t => t.Item.Contains(joltage.Index)).ToList();
                buttons_affecting_joltage.ForEach(button => buttonToPresse.Add(z3Buttons[button.Index]));

                var sumPressedButtons = buttonToPresse.Count == 1
                        ? buttonToPresse[0]
                        : context.MkAdd([.. buttonToPresse]);

                optimizer.Add(context.MkEq(sumPressedButtons, context.MkInt(joltage.Item)));
            }

            var totalPressCount = context.MkAdd(z3Buttons);

            optimizer.MkMinimize(totalPressCount);

            var status = optimizer.Check();
            var evaluatedTotalPressCount = optimizer.Model.Evaluate(totalPressCount);
            sum += ((IntNum)evaluatedTotalPressCount).Int;
        }

        return sum;
    }

    public record Machine(bool[] Leds, List<Button> Buttons);
    public record Button(List<int> Wires, int Presses, bool[] Leds, HashSet<string> PressedButtons)
    {
        public Button Press()
        {
            return this with { Presses = Presses + 1, Leds = SwitchLeds(), PressedButtons = [.. PressedButtons, ButtonIdentifier] };
        }

        public string ButtonIdentifier => string.Concat(Wires);

        private bool[] SwitchLeds()
        {
            bool[] b = new bool[Leds.Length];
            Leds.CopyTo(b, 0);
            foreach (var c in Wires)
                b[c] = !b[c];
            return b;
        }
    }
}
