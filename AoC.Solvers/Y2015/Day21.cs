namespace AoC.Solvers.Y2015;

public class Day21(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private string Input { get; set; } = input;
    public int Star1() => RunConfigurations().leastAmountWin;
    public int Star2() => RunConfigurations().mostAmountLost;
    private bool RunGame(Character you, Character boss)
    {
        while (you.IsAlive && boss.IsAlive)
        {
            var damage = you.Attack(boss);
            if (!boss.ApplyDamage(damage))
                return true;
            damage = boss.Attack(you);
            if (!you.ApplyDamage(damage))
                return false;
        }
        return true;
    }

    private (int leastAmountWin, int mostAmountLost) RunConfigurations()
    {
        int leastAmountWin = int.MaxValue;
        int mostAmountLost = int.MinValue;

        foreach (var weapon in Weapons)
            foreach (var armor in Armors)
                for (int r1 = 0; r1 < Rings.Length; r1++)
                    for (int r2 = 0; r2 < Rings.Length; r2++)
                    {
                        if (r2 == r1)
                            continue;
                        var you = new Character(100,
                            weapon.Damage + Rings[r1].Damage + Rings[r2].Damage,
                            armor.Defens + Rings[r1].Defens + Rings[r2].Defens
                            );

                        var cost = weapon.Cost + armor.Cost + Rings[r1].Cost + Rings[r2].Cost;
                        if (RunGame(you, new Character(109, 8, 2)))
                            if (cost < leastAmountWin)
                                leastAmountWin = cost;
                            else
                            if (cost > mostAmountLost)
                                mostAmountLost = cost;
                    }
        return (leastAmountWin, mostAmountLost);
    }

    private class Character(int hitpoint, int damage, int armor)
    {
        public int Hitpoint { get; set; } = hitpoint;
        public int Damage { get; set; } = damage;
        public int Armor { get; set; } = armor;
        public int GetArmor => Armor + RingDefens;
        public int RingDamage { get; set; } = 0;
        public int RingDefens { get; set; } = 0;

        public int Attack(Character oponent)
        {
            if (Damage + RingDamage <= oponent.GetArmor)
                return 1;
            else
                return Damage + RingDamage - oponent.GetArmor;
        }
        public bool IsAlive => Hitpoint > 0;
        public bool ApplyDamage(int damage)
        {
            Hitpoint -= damage;
            return IsAlive;
        }
    }

    private record Weapon(int Cost, int Damage, int Defens);
    private record Armor(int Cost, int Damage, int Defens);
    private record Ring(int Cost, int Damage, int Defens);
    private List<Weapon> Weapons = [
        new Weapon(8,4,0),
        new Weapon(10,5,0),
        new Weapon(25,6,0),
        new Weapon(40,7,0),
        new Weapon(72,8,0)
    ];
    private List<Armor> Armors = [
        new Armor(0, 0, 0),
        new Armor(13, 0, 1),
        new Armor(31, 0, 2),
        new Armor(53, 0, 3),
        new Armor(75, 0, 4),
        new Armor(102, 0, 5),
    ];
    private Ring[] Rings = [
        new Ring(0, 0, 0),
        new Ring(25, 1, 0),
        new Ring(50, 2, 0),
        new Ring(100, 3, 0),
        new Ring(0, 0, 0),
        new Ring(20, 0, 1),
        new Ring(40, 0, 2),
        new Ring(80, 0, 3),
    ];
}
