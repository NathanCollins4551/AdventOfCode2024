using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string input;

    public Day03()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        // Create a pattern for a expressions that contain "mul(  'anything in here'  )"
        Regex rg = new Regex(@"mul\(\d+,\d+\)");
        MatchCollection matchs = rg.Matches(input);

        return matchs.Select(match => mul(match.Value)).Sum();
    }

    public int SolvePartTwo()
    {
        string newInput = instructionParse(input);

        // Create a pattern for a expressions that contain "mul(  'anything in here'  )"
        Regex rg = new Regex(@"mul\(\d+,\d+\)");
        MatchCollection matchs = rg.Matches(newInput);

        return matchs.Select(match => mul(match.Value)).Sum();
    }

    public int mul(string s)
    {
        s = s.Replace("mul(", string.Empty).Replace(")",string.Empty);
        return Int32.Parse(s.Split(',')[0]) * Int32.Parse(s.Split(',')[1]);
    }

    public string instructionParse(string input)
    {
        bool enabled = true;
        int index = 0;
        string newInput = string.Empty;

        while(index < input.Length)
        {
            if (enabled)
            {
                try
                {
                    //check for don't()
                    if (input.Substring(index, 7).Equals("don't()"))
                    {
                        enabled = false;
                        index += 7;
                    }
                    else
                    {
                        newInput += input[index];
                        index += 1;
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    newInput += input[index];
                    index += 1;
                }
            }
            else
            {
                try
                {
                    //check for do()
                    if (input.Substring(index, 4).Equals("do()"))
                    {
                        enabled = true;
                        index += 4;
                    }
                    else
                    {
                        index += 1;
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    index += 1;
                }
            }
        }
        return newInput;
    }
}
