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
        List<int> list1 = input.Split("\n").Where(x => x.Length > 0).Select(line => Int32.Parse(line.Split("   ")[0])).Order().ToList();
        List<int> list2 = input.Split("\n").Where(x => x.Length > 0).Select(line => Int32.Parse(line.Split("   ")[1])).Order().ToList();

        return list1.Select((num, index) => Math.Abs(num - list2[index])).Sum();
    }

    public int SolvePartTwo()
    {
        List<int> list1 = input.Split("\n").Where(x => x.Length > 0).Select(line => Int32.Parse(line.Split("   ")[0])).ToList();
        List<int> list2 = input.Split("\n").Where(x => x.Length > 0).Select(line => Int32.Parse(line.Split("   ")[1])).ToList();

        return list1.Select(num => list2.Where(x => x.Equals(num)).Count() * num).Sum();
    }
}
