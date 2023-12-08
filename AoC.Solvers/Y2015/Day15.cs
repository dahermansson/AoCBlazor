using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day15: IDay
{
    public Day15(string input)
    {
        Input = InputParsers.GetInputLines(input);
    }
    public string Output => throw new NotImplementedException();
    private string[] Input {get; set;}
    public int Star1() => GetScores(new Cookie(Input.Select(t => new Ingredient(t)).ToList()), new List<int>(), 100);
    public int Star2() => GetCalories(new Cookie(Input.Select(t => new Ingredient(t)).ToList()), new List<int>(), 100);
    private int GetScores(Cookie cookie, List<int> amounts, int nummer)
    {
        if(amounts.Count + 1 == cookie.Ingredients.Count)
            return cookie.GetTotalScore([.. amounts, nummer]);
        
        int max = 0;
        for (int i = 1; i <= nummer; i++)
        {
            var rating = GetScores(cookie, [.. amounts, i], nummer -i);
            if(rating>max)
                max = rating;
        }
        return max;
    }

    private int GetCalories(Cookie cookie, List<int> amounts, int nummer)
    {
        if(amounts.Count + 1 == cookie.Ingredients.Count)
            if(cookie.GetCalories([.. amounts, nummer]) == 500)
                return cookie.GetTotalScore([.. amounts, nummer]);
            else
                return 0;
        
        int max = 0;
        for (int i = 1; i <= nummer; i++)
        {
            var rating = GetCalories(cookie, [.. amounts, i], nummer -i);
            if(rating>max)
                max = rating;
        }
        return max;
    }

    private record Amount(int A, int B, int C, int D);
    private class Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }

        public Ingredient(string str)
        {
            var s = str.Split(" ");
            Name = s[0];
            Capacity = int.Parse(s[2], System.Globalization.NumberStyles.Any);
            Durability = int.Parse(s[4], System.Globalization.NumberStyles.Any);
            Flavor = int.Parse(s[6], System.Globalization.NumberStyles.Any);
            Texture = int.Parse(s[8], System.Globalization.NumberStyles.Any);
            Calories = int.Parse(s[10], System.Globalization.NumberStyles.Any);
        }
        
    }
    private class Cookie
    {
        public List<Ingredient> Ingredients { get; set; }
        public Cookie(List<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }

        public int GetTotalScore(int[] amounts)
        {
            if(amounts.Length != Ingredients.Count)
                return 0;
            var capacity = Ingredients.Select((t,i) => t.Capacity*amounts[i]).Sum();
            var durability = Ingredients.Select((t,i) => t.Durability*amounts[i]).Sum();
            var flavor = Ingredients.Select((t,i) => t.Flavor*amounts[i]).Sum();
            var texture = Ingredients.Select((t,i) => t.Texture*amounts[i]).Sum();
            if(capacity < 0|| durability < 0 || flavor < 0 || texture <0)
                return 0;
            return capacity*durability*flavor*texture;
        }
        public int GetCalories(int[] amounts) => 
            Ingredients.Select((t,i) => t.Calories*amounts[i]).Sum();
        
    }
}
