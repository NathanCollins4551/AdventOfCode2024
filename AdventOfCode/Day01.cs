namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string input;

    public Day01()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        foreach (var line in input.Split("\n").Where(x => x.Length > 0))
        {
            list1.Add(Int32.Parse(line.Split("   ")[0]));
            list2.Add(Int32.Parse(line.Split("   ")[1]));
        }
        list1 = list1.Order().ToList();
        list2 = list2.Order().ToList();

        int sum = 0;
        for(int i = 0; i < list1.Count(); i++)
        {
            sum += Math.Abs(list1[i] - list2[i]);
        }
        return sum;
    }

    public int SolvePartTwo()
    {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        foreach (var line in input.Split("\n").Where(x => x.Length > 0))
        {
            list1.Add(Int32.Parse(line.Split("   ")[0]));
            list2.Add(Int32.Parse(line.Split("   ")[1]));
        }

        return list1.Select(num => list2.Where(x => x.Equals(num)).Count() * num).Sum();
    }
}
