using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string inputString;
    //char[][] input;
    string[] input;

    public Day04()
    {
        inputString = File.ReadAllText(InputFilePath);
        input = inputString.Split('\n').Where(line => line.Length > 0).ToArray();
        //input = inputString.Split('\n').Where(line => line.Length > 0).Select(line => line.ToCharArray()).ToArray();    
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        return solveWordSearch(input);
    }

    public int SolvePartTwo()
    {
        return 0;
    }

    public int solveWordSearch(string[] a)
    {
        string s = string.Join('-', a);
        int len = a[0].Length;//length of each line
        Console.WriteLine(s);
        string[] patterns = { @"XMAS" ,//horizontal
                              @"SAMX" ,//horizontal backwards
                              $@"X.{{{len}}}M.{{{len}}}A.{{{len}}}S" ,//vertical
                              $@"S.{{{len}}}A.{{{len}}}M.{{{len}}}X" ,//vertical backwards
                              $@"X[^-]{{{3}}}.{{{len - 2}}}M.{{{len + 1}}}A.{{{len + 1}}}S" ,//diaginol NW-SE
                              $@"S[^-]{{{3}}}.{{{len - 2}}}A.{{{len + 1}}}M.{{{len + 1}}}X" ,//diaginol NW-SE backwards
                              $@"[^-]{{{3}}}X.{{{len - 1}}}M.{{{len - 1}}}A.{{{len - 1}}}S" ,//diaginol NE-SW
                              $@"[^-]{{{3}}}S.{{{len - 1}}}A.{{{len - 1}}}M.{{{len - 1}}}X" ,//diaginol NE-SW backwards
        };
        return patterns.Select(pattern => Regex.Matches(s, pattern).Count()).Sum();
    }
    public int findHorizontalXMASImproved(string[] input)
    {
        string s = string.Join("-", input);
        Console.WriteLine(s);
        Regex rg = new Regex(@"XMAS");
        Regex rgReverse = new Regex(@"SAMX");

        MatchCollection matchs = rg.Matches(s);
        MatchCollection reversedMatches = rgReverse.Matches(s);

        return matchs.Count() + reversedMatches.Count();
    }
    public int findVerticalXMASImproved(string[] input)
    {
        int len = input[0].Length;//length of each line
        string s = string.Join("-", input);

        Regex rg = new Regex($@"X.{{{len}}}M.{{{len}}}A.{{{len}}}S");
        Regex rgReverse = new Regex($@"S.{{{len}}}A.{{{len}}}M.{{{len}}}X");

        MatchCollection matchs = rg.Matches(s);
        MatchCollection reversedMatches = rgReverse.Matches(s);

        return matchs.Count() + reversedMatches.Count();
    }
    public int findDiaginolXMASImproved(string[] input)
    {
        int len = input[0].Length;//length of each line
        string s = string.Join("-", input);

        Regex rg = new Regex($@"X[^-]{{{3}}}.{{{len - 2}}}M.{{{len + 1}}}A.{{{len + 1}}}S"); 
        Regex rgReverse = new Regex($@"dfgsdfg");

        MatchCollection matchs = rg.Matches(s);
        MatchCollection reversedMatches = rgReverse.Matches(s);

        return Regex.Matches(s, $@"X[^-]{{{3}}}.{{{len - 2}}}M.{{{len + 1}}}A.{{{len + 1}}}S").Count();
    }

    public int findHorizontalXMAS(char[][] matrix)
    {    
        IEnumerable<string> s = matrix.Select(line => new string(line));
        return s.Select(line => numOfXMAS(line)).Sum();
    }
    public int findVerticalXMAS(char[][] matrix)
    {
        char[][] rotatedMatrix = rotate(Array.ConvertAll(matrix, a => (char[])a.Clone()));
        IEnumerable<string> s = rotatedMatrix.Select(line => new string(line));
        return s.Select(line => numOfXMAS(line)).Sum();
    }
    public int findDiaginolXMAS(char[][] matrix)
    {    
        return getDiaginols(matrix).Select(line => new string(line.ToArray())).Select(line => numOfXMAS(line)).Sum()
            + getDiaginols(matrix.Select(line => line.Reverse().ToArray()).ToArray()).Select(line => new string(line.ToArray())).Select(line => numOfXMAS(line)).Sum();
    }
    public int numOfXMAS(string s)
    {
        Regex rg = new Regex(@"XMAS");
        Regex rgReverse = new Regex(@"SAMX");

        MatchCollection matchs = rg.Matches(s);
        MatchCollection reversedMatches = rgReverse.Matches(s);
        return matchs.Count() + reversedMatches.Count();
    }
    public List<List<char>> getDiaginols(char[][] matrix)
    {
        List<List<char>> diaginols = new List<List<char>>();
        int length = matrix.Length;
        int width = matrix[0].Length;
        int column = 0;
        int row = 0;
        int len = 0;
        List<char> tmp;

        while (row != length || column != 0)//traverses from top left down to bottom left of matrix then across the bottom
        {
            tmp = new List<char>();
            len = 0;
            while (true)
            {
                tmp.Add(matrix[row - len][column + len]);
                len++;
                if(column + len >= width || row - len < 0)
                {
                    if (row == length - 1)
                    {
                        column++;
                    }
                    else
                    {
                        row++;
                    }
                    break;
                }
            }
            diaginols.Add(tmp);
            if (column == width && row == length-1)
            {
                break;
            }
        }
        return diaginols;
    }

    public char[][] rotate(char[][] matrix)
    {
        int size = matrix.Length;
        int layer_count = size / 2;

        for(int first = 0; first < layer_count; first++)
        {
            int last = size - first - 1;
            for(int element = first; element < last; element++)
            {
                int offset = element - first;
                char top = matrix[first][element];
                char right_side = matrix[element][last];
                char bottom = matrix[last][last - offset];
                char left_side = matrix[last - offset][first];

                matrix[first][element] = left_side;
                matrix[element][last] = top;
                matrix[last][last - offset] = right_side;
                matrix[last - offset][first] = bottom;
            }
        }
        return matrix;
    }
}
