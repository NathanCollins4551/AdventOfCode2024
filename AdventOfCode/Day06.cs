using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string input;

    public Day06()
    {
        input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{SolvePartOne()}");

    public override ValueTask<string> Solve_2() => new($"{SolvePartTwo()}");

    public int SolvePartOne()
    {
        var map = input.Split('\n').Where(l => l.Length > 0).Select(l => l.ToList()).ToList();
        int posY = map.FindIndex(line => line.Contains('^'));
        int posX = map[posY].IndexOf('^');

        do
        {
            switch (map[posY][posX])
            {
                case '^':
                    if (posY - 1 < 0)//if out of map
                    {
                        map[posY][posX] = 'X';
                        posY--;
                        break;
                    }
                    else if (map[posY - 1][posX] == '#')//if obstacle ahead, head east
                    {
                        map[posY][posX] = '>';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posY--;
                        map[posY][posX] = '^';
                    }
                    break;
                case '>':
                    if (posX + 1 >= map[0].Count())//if out of map
                    {
                        map[posY][posX] = 'X';
                        posX++;
                        break;
                    }
                    else if (map[posY][posX + 1] == '#')//if obstacle ahead, head south
                    {
                        map[posY][posX] = 'v';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posX++;
                        map[posY][posX] = '>';
                    }
                    break;
                case 'v':
                    if (posY + 1 >= map.Count())//if out of map
                    {
                        map[posY][posX] = 'X';
                        posY++;
                        break;
                    }
                    else if (map[posY + 1][posX] == '#')//if obstacle ahead, head west
                    {
                        map[posY][posX] = '<';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posY++;
                        map[posY][posX] = 'v';
                    }
                    break;
                case '<':
                    if (posX - 1 < 0)//if out of map
                    {
                        map[posY][posX] = 'X';
                        posX--;
                        break;
                    }
                    else if (map[posY][posX - 1] == '#')//if obstacle ahead, head north
                    {
                        map[posY][posX] = '^';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posX--;
                        map[posY][posX] = '<';
                    }
                    break;
                default:
                    Console.WriteLine("error");
                    posY = -1;
                    break;
            }
        }
        while (posY >= 0 && posY < map.Count() && posX >= 0 && posX < map[0].Count());

        return map.Select(l => l.Where(c => c == 'X').Count()).Sum();
    }

    public int SolvePartTwo()//could use some optimazation, it is very slow(2 - 3 seconds with full input)
    {
        var map = input.Split('\n').Where(l => l.Length > 0).Select(l => l.ToList()).ToList();
        int startY = map.FindIndex(line => line.Contains('^'));
        int startX = map[startY].IndexOf('^');

        int possibleLoops = 0;

        for(int y = 0; y < map.Count(); y++)
        {
            for(int x = 0; x < map[0].Count(); x++)
            {
                if (map[y][x] != '#' && !(y == startY && x == startX))
                {
                    possibleLoops += hasInfiniteLoop(y,x);
                }
            }
        }

        return possibleLoops;
    }

    public int SolvePartOneAnimated(int pause)//Only for use with test map, or other small maps, the actual input is too large to properly show in console
    {
        var map = input.Split('\n').Where(l => l.Length > 0).Select(l => l.ToList()).ToList();
        int posY = map.FindIndex(line => line.Contains('^'));
        int posX = map[posY].IndexOf('^');

        printMap(map);
        do
        {
            switch (map[posY][posX])
            {
                case '^':
                    if(posY-1 < 0)//if out of map
                    {
                        map[posY][posX] = 'X';
                        update(posY, posX, map[posY][posX]);
                        posY--;
                        break;
                    }
                    else if (map[posY-1][posX] == '#')//if obstacle ahead, head east
                    {
                        map[posY][posX] = '>';
                        update(posY, posX, map[posY][posX]);
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posY--;
                        map[posY][posX] = '^';
                        update(posY + 1, posX, map[posY + 1][posX]);
                        update(posY, posX, map[posY][posX]);
                    }
                    break;
                case '>':
                    if (posX + 1 >= map[0].Count())//if out of map
                    {
                        map[posY][posX] = 'X';
                        update(posY, posX, map[posY][posX]);
                        posX++;
                        break;
                    }
                    else if (map[posY][posX+1] == '#')//if obstacle ahead, head south
                    {
                        map[posY][posX] = 'v';
                        update(posY, posX, map[posY][posX]);
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posX++;
                        map[posY][posX] = '>';
                        update(posY, posX - 1, map[posY][posX - 1]);
                        update(posY, posX, map[posY][posX]);
                    }
                    break;
                case 'v':
                    if (posY + 1 >= map.Count())//if out of map
                    {
                        map[posY][posX] = 'X';
                        update(posY, posX, map[posY][posX]);
                        posY++;
                        break;
                    }
                    else if (map[posY + 1][posX] == '#')//if obstacle ahead, head west
                    {
                        map[posY][posX] = '<';
                        update(posY, posX, map[posY][posX]);
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posY++;
                        map[posY][posX] = 'v';
                        update(posY - 1, posX, map[posY - 1][posX]);
                        update(posY, posX, map[posY][posX]);
                    }
                    break;
                case '<':
                    if (posX - 1 < 0)//if out of map
                    {
                        map[posY][posX] = 'X';
                        update(posY, posX, map[posY][posX]);
                        posX--;
                        break;
                    }
                    else if (map[posY][posX - 1] == '#')//if obstacle ahead, head north
                    {
                        map[posY][posX] = '^';
                        update(posY, posX, map[posY][posX]);
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posX--;
                        map[posY][posX] = '<';
                        update(posY, posX + 1, map[posY][posX + 1]);
                        update(posY, posX, map[posY][posX]);
                    }
                    break;
                default:
                    Console.WriteLine("error");
                    posY = -1;
                    break;
            }
            System.Threading.Thread.Sleep(pause);
        }
        while (posY >= 0 && posY < map.Count() && posX >= 0 && posX < map[0].Count());

        Console.WriteLine();

        return map.Select(l => l.Where(c => c == 'X').Count()).Sum();
    } 

    public void update(int posY, int posX, char newValue)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.CursorLeft = posX;
        Console.CursorTop = posY;
        Console.Write(newValue);
    }
    public void printMap(List<List<char>> map)
    {
        foreach (List<char> line in map)
        {
            foreach (char c in line)
            {
                if (c == '#')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (c == '^')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
    public int hasInfiniteLoop(int y, int x)
    {
        var map = input.Split('\n').Where(l => l.Length > 0).Select(l => l.ToList()).ToList();
        map[y][x] = '#';
        
        int posY = map.FindIndex(line => line.Contains('^'));
        int posX = map[posY].IndexOf('^');

        List<Tuple<int, int>> northToEastTurns = new List<Tuple<int, int>>(); //List will note the position every time you hit an obstruction going north and have to turn east

        do
        {
            switch (map[posY][posX])
            {
                case '^':
                    if (posY - 1 < 0)//if out of map
                    {
                        map[posY][posX] = 'X';
                        posY--;
                        break;
                    }
                    else if (map[posY - 1][posX] == '#')//if obstacle ahead, head east
                    {
                        if(northToEastTurns.Contains(new Tuple<int, int>(posY, posX)))
                        {
                            return 1;
                        }
                        else
                        {
                            northToEastTurns.Add(new Tuple<int, int>(posY, posX));
                        }
                        map[posY][posX] = '>';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posY--;
                        map[posY][posX] = '^';
                    }
                    break;
                case '>':
                    if (posX + 1 >= map[0].Count())//if out of map
                    {
                        map[posY][posX] = 'X';
                        posX++;
                        break;
                    }
                    else if (map[posY][posX + 1] == '#')//if obstacle ahead, head south
                    {
                        map[posY][posX] = 'v';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posX++;
                        map[posY][posX] = '>';
                    }
                    break;
                case 'v':
                    if (posY + 1 >= map.Count())//if out of map
                    {
                        map[posY][posX] = 'X';
                        posY++;
                        break;
                    }
                    else if (map[posY + 1][posX] == '#')//if obstacle ahead, head west
                    {
                        map[posY][posX] = '<';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posY++;
                        map[posY][posX] = 'v';
                    }
                    break;
                case '<':
                    if (posX - 1 < 0)//if out of map
                    {
                        map[posY][posX] = 'X';
                        posX--;
                        break;
                    }
                    else if (map[posY][posX - 1] == '#')//if obstacle ahead, head north
                    {
                        map[posY][posX] = '^';
                    }
                    else
                    {
                        map[posY][posX] = 'X';
                        posX--;
                        map[posY][posX] = '<';
                    }
                    break;
                default:
                    Console.WriteLine("error");
                    posY = -1;
                    break;
            }
        }
        while (posY >= 0 && posY < map.Count() && posX >= 0 && posX < map[0].Count());

        return 0;
    }
}
