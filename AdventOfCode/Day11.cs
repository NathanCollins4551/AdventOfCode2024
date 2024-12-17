using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string[] input;
    int count;

    public Day11()
    {
        input = File.ReadAllText(InputFilePath).Split(' ').Where(item => item.Length > 0).ToArray();
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        count = 0;
        foreach (var num in input)
        {
            eval(num, 25);
            count++;
        }
        return count;
    }

    public int SolvePartTwo()
    {
        count = 0;
        foreach (var num in input)
        {
            eval(num, 75);
            count++;
        }
        return count;
    }
    public void eval(string num, int itr)
    {
        if (itr <= 0)
        {
            return;
        }
        if (num.Equals("0"))
        {
            eval("1", itr - 1);
        }
        else if (num.Length % 2 == 0)
        {
            //Console.WriteLine($"Splitting: {num} into  ->  {num.Substring(0, num.Length / 2)}  and  {Regex.Replace(num.Substring(num.Length / 2, num.Length / 2), "^0+(?!$)", "")}");
            eval(num.Substring(0, num.Length / 2), itr - 1);
            eval(Regex.Replace(num.Substring(num.Length / 2, num.Length / 2), "^0+(?!$)", ""), itr - 1);
            count++;  
        }
        else
        {
            eval((long.Parse(num)*2024).ToString(), itr-1);
        }
    }
}
