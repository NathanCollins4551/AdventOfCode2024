using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string input;

    public Day02()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        return input.Split('\n').Where(x => x.Length > 0)
            .Select(line => isSafe(line.Split(' '), false))
            .Sum();
    }

    public int SolvePartTwo()
    {
        return input.Split('\n').Where(x => x.Length > 0)
           .Select(line => isSafe(line.Split(' '), true))
           .Sum();
    }

    public int isSafe(string[] line, bool dampener)
    {
        if (isIncreasing(line) || isDecreasing(line))
        {
            if (difference(line))
            {
                return 1;
            }
        }
        if (dampener)
        {
            for (int i = 0; i < line.Length; i++)
            {
                var tmp = line.ToList();
                tmp.RemoveAt(i);
                if (isSafe(tmp.ToArray(),false) == 1)
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    public bool difference(string[] s)//returns true if the difference between two adjacent elements is between 1 and 3
    {
        for(int i = 0; i < s.Length-1; i++)
        {
            if (Math.Abs(Int32.Parse(s[i]) - Int32.Parse(s[i+1])) < 1)
            {
                return false;
            }
            if (Math.Abs(Int32.Parse(s[i]) - Int32.Parse(s[i + 1])) > 3)
            {
                return false;
            }
        }
        return true;
    }

    public bool isIncreasing(string[] s)
    {
        int[] nums = Array.ConvertAll(s, int.Parse);
        if (nums.SequenceEqual(nums.Order()))
        {
            return true;
        }
        return false;
    }

    public bool isDecreasing(string[] s)
    {
        int[] nums = Array.ConvertAll(s, int.Parse);
        if (nums.SequenceEqual(nums.OrderDescending()))
        {
            return true;
        }
        return false;
    }
}
