using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string[] input;
    Func<string, int, long> functionToRun;
    Dictionary<(string, int), long> dict;
    public Day11()
    {
        input = File.ReadAllText(InputFilePath).Split(' ').Where(item => item.Length > 0).ToArray();
        functionToRun = evalOptimized;
        dict = new Dictionary<(string, int), long>();
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOneOptimized()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public long SolvePartOneOptimized()
    {
        long count = input.Length;
        const int blinks =25;
        foreach (var num in input)
        {
            count += dict.TryGetKey(num, blinks, functionToRun);
        }
        return count;
    }
    public long SolvePartTwo()
    {
        long count = input.Length;
        const int blinks = 75;
        foreach (var num in input)
        {
            count += dict.TryGetKey(num, blinks, functionToRun);
        }
        return count;
    }
    public int SolvePartOne()
    {
        int count = input.Length;
        const int blinks = 25;
        foreach (var num in input)
        {
            count += eval(num, blinks);
        }
        return count;
    }
    public long evalOptimized(string num, int itr)
    {
        if (itr <= 0)
        {
            return 0;
        }
        if (num.Equals("0"))
        {
            return dict.TryGetKey("1", itr - 1, functionToRun);
        }
        else if (num.Length % 2 == 0)
        {
            return dict.TryGetKey(num.Substring(0, num.Length / 2), itr - 1, functionToRun) +
                   dict.TryGetKey(Regex.Replace(num.Substring(num.Length / 2, num.Length / 2), "^0+(?!$)", ""), itr - 1, functionToRun) +
                   1;
        }
        else
        {
            return dict.TryGetKey((long.Parse(num) * 2024).ToString(), itr - 1, functionToRun);
        }
    }
    public int eval(string num, int itr)
    {
        if (itr <= 0)
        {
            return 0;
        }
        if (num.Equals("0"))
        {
            return eval("1", itr - 1);
        }
        else if (num.Length % 2 == 0)
        {
            //Console.WriteLine($"Splitting: {num} into  ->  {num.Substring(0, num.Length / 2)}  and  {Regex.Replace(num.Substring(num.Length / 2, num.Length / 2), "^0+(?!$)", "")}");
            return eval(num.Substring(0, num.Length / 2), itr - 1) +
                   eval(Regex.Replace(num.Substring(num.Length / 2, num.Length / 2), "^0+(?!$)", ""), itr - 1) +
                   1;
        }
        else
        {
            return eval((long.Parse(num) * 2024).ToString(), itr - 1);
        }
    }
}
