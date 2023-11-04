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
    private class ActiveSpell(int turnsLeft, Spell spell)
    {
        public int TurnsLeft { get; set; } = turnsLeft;
        public Spell Spell { get; set; } = spell;
        public ActiveSpell Clone() => new ActiveSpell(TurnsLeft, Spell);
    }

    public int Star1()
    {
        var you = new Character(50, 0, 0, 500);
        var boss = new Character(59, 9, 0, 0);

        PlayGame(you, boss, [], 0, false);
        return MinSpentWin;
    }

    public int Star2()
    {
        MinSpentWin=10000;
        var you = new Character(50, 0, 0, 500);
        var boss = new Character(59, 9, 0, 0);

        PlayGame(you, boss, [], 0, true);
        return MinSpentWin;
    }
    public int MinSpentWin { get; set; }= 100000;
    private void PlayGame(Character you, Character boss, List<ActiveSpell> activeSpells, int spentMana, bool star2)
    {
        if(star2)
            you.Hitpoint-=1;
        if(!you.IsAlive || !boss.IsAlive)
        {
            if(you.IsAlive)
                MinSpentWin = spentMana;
            return;
        }
        var currentYou = you.Clone();
        var currentBoss = boss.Clone();
        var spellsToChose = Spells.Where(t => t.Cost < you.Mana && !activeSpells.Any(a => a.TurnsLeft >0 && a.Spell.Name == t.Name)).ToList();
        
        foreach (var spell in spellsToChose)
        {
            var doneSpells = activeSpells.IndexOfMany(t => t.TurnsLeft <= 0 || t.TurnsLeft == t.Spell.Rounds).ToArray();
            for (int i = doneSpells.Length-1; i >=0 ; i--)
            {
                activeSpells[doneSpells[i]].Spell.EndSpell(you, boss);
                activeSpells.RemoveAt(doneSpells[i]);
            }
            you.Mana -= spell.Cost;
            spentMana += spell.Cost;

            activeSpells.Add(new ActiveSpell(spell.Rounds, spell));

            activeSpells.Where(t => t.TurnsLeft > 0).ToList().ForEach(t => _ = t.Spell.ApplySpell(you, boss));
            if(star2)
                you.Hitpoint-=1;
            activeSpells.Where(t => t.Spell.Rounds > 1 && t.TurnsLeft > 1).ToList().ForEach(t => _ = t.Spell.ApplySpell(you, boss));
            if(!boss.IsAlive || !you.ApplyDamage(boss.Attack(you))) //Boss attack
            {
                if(you.IsAlive && spentMana < MinSpentWin)
                    MinSpentWin = spentMana;
                spentMana -=spell.Cost;
                you = currentYou.Clone();
                boss = currentBoss.Clone();
                continue;
            }
            var newActiveSpellState = activeSpells.Select(t => new ActiveSpell(t.TurnsLeft-2, t.Spell)).ToList();
            var newYou = you.Clone();
            newYou.Armor = 0;
            if(spentMana < MinSpentWin)
                PlayGame(newYou, boss.Clone(), newActiveSpellState, spentMana, star2);
            spentMana -=spell.Cost;
            you = currentYou.Clone();
            boss = currentBoss.Clone();
        }
        //Not enough mana to cast spell, You lose
        return;
    }

    private List<Spell> Spells => [
        new Spell{
            Name = "Magic Missile",
            Cost = 53,
            ApplySpell = (you, boss) => boss.ApplyDamage(4),
            Instant = true,
            Rounds = 1,
            EndSpell = (you, boss) => {},
        },
        new Spell{
            Name = "Drain",
            Cost = 73,
            ApplySpell = (you, boss) => {you.Hitpoint += 2; return boss.ApplyDamage(2);},
            Instant = true,
            Rounds = 1,
            EndSpell = (you, boss) => {}
        },
        new Spell{
            Name = "Shield",
            Cost = 113,
            ApplySpell = (you, boss) => {you.Armor = 7; return true;},
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
            ApplySpell = (you, boss) => {you.Mana += 101; return true;},
            Instant = true,
            Rounds = 5,
            EndSpell = (you, boss) => {}
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



    private class Character(int hitpoint, int damage, int armor, int mana)
    {
        public int Hitpoint { get; set; } = hitpoint;
        public int Damage { get; set; } = damage;
        public int Armor { get; set; } = armor;
        public int GetArmor => Armor + RingDefens;
        public int RingDamage { get; set; } = 0;
        public int RingDefens { get; set; } = 0;
        public int Mana { get; set; } = mana;

        public Character Clone() => new Character(this.Hitpoint, this.Damage, this.Armor, this.Mana); //Damage and Armor always starts on 0 on each turn

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
