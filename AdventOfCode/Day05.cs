using System.Runtime.InteropServices;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string input;

    public Day05()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        var rules = input.Split("\n\n")[0].Split("\n").Where(l => l.Length > 0);
        var updates = input.Split("\n\n")[1].Split("\n").Where(l => l.Length > 0).Select(l => l.Split(","));

        var correctUpdates = updates.Where(l => followsRules(l, rules));

        return correctUpdates.Select(l => Int32.Parse(l[l.Length / 2])).Sum();
    }

    public int SolvePartTwo()
    {
        var rules = input.Split("\n\n")[0].Split("\n").Where(l => l.Length > 0);
        var updates = input.Split("\n\n")[1].Split("\n").Where(l => l.Length > 0).Select(l => l.Split(","));

        var incorrectUpdates = updates.Where(l => !followsRules(l, rules));

        var correctedUpdates = incorrectUpdates.Select(l => fixOrder(l,rules));

        return correctedUpdates.Select(l => Int32.Parse(l[l.Length / 2])).Sum();
    }

    public bool followsRules(string[] update, IEnumerable<string> rules)
    {
        for(int i = 0; i < update.Length - 1; i++)
        {
            for(int j = i + 1; j < update.Length; j++)
            {
                if (!rules.Contains(update[i] + "|" + update[j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public string[] fixOrder(string[] oldUpdate, IEnumerable<string> rules)
    {
        rules = rules.Where(s => oldUpdate.Contains(s.Split("|")[0]) && oldUpdate.Contains(s.Split("|")[1]));
        rules = rules.Select(s => s.Split("|")[0]);

        string[] newUpdate = new string[oldUpdate.Length];
        Array.Copy(oldUpdate, newUpdate, oldUpdate.Length);

        foreach(string s in oldUpdate)
        {
            int index = Math.Abs(rules.Where(x => x.Equals(s)).Count() - oldUpdate.Length) - 1;
            newUpdate[index] = s;
        }    
        return newUpdate;
    }
}
