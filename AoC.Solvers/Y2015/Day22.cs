using System.Collections.Concurrent;
using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day22: IDay
{
    public Day22(string input)
    {
        Input = input;
    }
    public string Output => throw new NotImplementedException();

    private string Input {get; set;}

    public int Star1()
    {
        return 0;
    }

    public int Star2()
    {
        return 0;
    }

    private List<Spell> Spells => [
        new Spell{
            Name = "Magic Missile",
            Cost = 57,
            ApplySpell = (you, boss) => boss.ApplyDamage(4),
            Instant = true,
            EndSpell = (you, boss) => {},
        },
        new Spell{
            Name = "Drain",
            Cost = 73,
            ApplySpell = (you, boss) => {you.Hitpoint += 2; return boss.ApplyDamage(2);},
            Instant = true,
            EndSpell = (you, boss) => {}
        },
        new Spell{
            Name = "Shield",
            Cost = 113,
            ApplySpell = (you, boss) => {you.Armor = 7; return boss.ApplyDamage(0);},
            Instant = false,
            Rounds = 6,
            EndSpell = (you, boss) => you.Armor = 0
        },
        new Spell{
            Name = "Poison",
            Cost = 173,
            ApplySpell = (you, boss) => {return boss.ApplyDamage(3);},
            Instant = false,
            Rounds = 6,
            EndSpell = (you, boss) => {}
        },
        new Spell{
            Name = "Recharge",
            Cost = 229,
            ApplySpell = (you, boss) => {you.Hitpoint+=2; return boss.ApplyDamage(2);},
            Instant = true,
            Rounds = 5,
            EndSpell = (you, boss) => you.Mana = 101
        }
    ];

    private class Spell
    {
        public string Name { get; set; } = default!;
        public int Cost { get; set; }
        public int Rounds { get; set; }
        public bool Instant { get; set; }
        public Func<Character, Character, bool> ApplySpell { get; set; } =default!;
        public Action<Character, Character> EndSpell { get; set; } = default!;
    }



    private class Character(int hitpoint, int damage, int armor)
    {
        public int Hitpoint { get; set; } = hitpoint;
        public int Damage { get; set; } = damage;
        public int Armor { get; set; } = armor;
        public int GetArmor => Armor + RingDefens;
        public int RingDamage { get; set; } = 0;
        public int RingDefens { get; set; } = 0;
        public int Mana { get; set; }

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
}
