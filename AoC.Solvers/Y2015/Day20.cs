using System.Text;
using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day20: IDay
{
    public Day20(string input)
    {
        Input = int.Parse(input);
    }
    public string Output => throw new NotImplementedException();

    private int Input {get; set;}

    public int Star1()
    {
        int targetPackage = Input;
        int limit = targetPackage>1000 ? 10: 1;
        int[] arr = Enumerable.Repeat(10, targetPackage/limit).ToArray();
        for (int i = 2; i < arr.Length; i++)
            for (int c = i; c < arr.Length; c+=i)
                arr[c]+=10*i;
   
        
        return arr.ToList().FindIndex(t => t >= targetPackage);
    }

    public int Star2()
    {
        int targetPackage = Input;
        int limit = targetPackage>1000 ? 10: 1;
        int[] arr = Enumerable.Repeat(11, targetPackage/limit).ToArray();
        for (int i = 2; i < arr.Length; i++)
        {
            int packageDroped = 0;
            for (int c = i; c < arr.Length; c+=i)
            {
                arr[c]+=11*i;
                packageDroped++;
                if(packageDroped>=50)
                    break;
            }
        }
        return arr.ToList().FindIndex(t => t >= targetPackage);
    }
}
