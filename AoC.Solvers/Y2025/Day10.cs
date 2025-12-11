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
                .Select(b => b with { Leds = currentButton.Leds, Presses = currentButton.Presses, PressedButtons = [..currentButton.PressedButtons] })
                .ToList()
                .ForEach(buttons.Enqueue);
        }
        return currentButton?.Presses ?? 0;
    }


    public int Star2()
    {
        return 0;
    }

    public record Machine(bool[] Leds, List<Button> Buttons);
    public record Button(List<int> Wires, int Presses, bool[] Leds, HashSet<string> PressedButtons)
    {
        public Button Press()
        {
            return this with { Presses = Presses + 1, Leds = SwitchLeds(), PressedButtons = [..PressedButtons, ButtonIdentifier] };
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
