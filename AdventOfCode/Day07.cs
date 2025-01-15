using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string input;

    public Day07()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    //Both solutions could use omtimization/simplification
    public long SolvePartOne()
    {
        return input.Split('\n').Where(l => l.Length > 0).Select(l => canBeSolved(l, "+*")).Sum();
    }

    public long SolvePartTwo()
    {
        return input.Split('\n').Where(l => l.Length > 0).Select(l => canBeSolved(l, "+*|")).Sum();
    }

    public long canBeSolved(string equation, string operators)
    {
        long ans = Int64.Parse(equation.Split(":")[0]);
        List<long> nums = equation.Split(":")[1].Split(" ").Where(s => s.Length > 0).Select(s => Int64.Parse(s)).ToList();
        int numCombinations = Convert.ToInt32(Math.Pow(operators.Length, nums.Count() - 1));
        int size = fromDeci(operators.Length, numCombinations).Length;

        List<string> combinations = Enumerable.Range(0, numCombinations).Select(n => fromDeci(operators.Length,n).PadLeft(size-1,'0')).ToList();

        foreach (var combination in combinations)
        {
            if(ans.Equals(evaluate(combination, nums))){
                return ans;
            }
        }
        return 0;
    }
    public long evaluate(string combination, List<long> nums)
    {
        long value = nums[0];
        for(int i = 1; i < nums.Count(); i++)
        {
            if (combination[i-1] == '0')
            {
                value += nums[i];
            }
            if (combination[i - 1] == '1')
            {
                value *= nums[i];
            }
            if (combination[i - 1] == '2')
            {
                value = Convert.ToInt64(value.ToString() + nums[i].ToString());
            }
        }
        return value;
    }

    public char reVal(int num)
    {
        if (num >= 0 && num <= 9)
            return (char)(num + 48);
        else
            return (char)(num - 10 + 65);
    }

    // Function to convert a given decimal number
    // to a base 'base' // This code is contributed by mits
    public string fromDeci(int base1, int inputNum)
    {
        string s = "";

        while (inputNum > 0)
        {
            s += reVal(inputNum % base1);
            inputNum /= base1;
        }
        char[] res = s.ToCharArray();

        Array.Reverse(res);
        return new string(res);
    }
}
