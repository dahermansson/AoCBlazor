using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day15: IDay
{
    public Day15(string input)
    {
        /*
        Input = InputParsers.GetInputLines("""
        Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
        Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
        """);
        */
        Input = InputParsers.GetInputLines(input);
    }
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}
    
    public int Star1()
    {
        var ingredients = Input.Select(t=> new Ingredient(t)).ToList();
        var cookie = new Cookie(ingredients);
        int maxScore = 0;

        for(int a = 1; a<97; a++)
            for(int b = 1; b<97; b++)
                for(int c = 1; c<97; c++)
                {
                    int d = 100-a-b-c;
                    if(a+b+c+d != 100)
                        continue;
                    var score = cookie.GetTotalScore([a,b,c,d]);
                    if(score > maxScore)
                        maxScore = score;
                }

        return maxScore;
    }

    public int Star2()
    {
        var ingredients = Input.Select(t=> new Ingredient(t)).ToList();
        var cookie = new Cookie(ingredients);
        int maxScore = 0;

        for(int a = 1; a<97; a++)
            for(int b = 1; b<97; b++)
                for(int c = 1; c<97; c++)
                {
                    int d = 100-a-b-c;
                    if(a+b+c+d != 100)
                        continue;
                    var calories = cookie.GetCalories([a,b,c,d]);
                    if(calories == 500)
                    {
                        var score = cookie.GetTotalScore([a,b,c,d]);
                        if(score > maxScore)
                            maxScore = score;
                    }
                }

        return maxScore;
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
