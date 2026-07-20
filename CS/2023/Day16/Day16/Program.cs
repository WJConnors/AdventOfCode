string[] input = File.ReadAllLines("text.txt");
int maxEnergized = 0;

for (int i = 0; i < input.Length; i++)
{
    int energized = explore(('R', i, 0));
    if (energized > maxEnergized)
    {
        maxEnergized = energized;
    }
}
for (int i = 0; i < input.Length; i++)
{
    int energized = explore(('L', i, input[0].Length - 1));
    if (energized > maxEnergized)
    {
        maxEnergized = energized;
    }
}
for (int i = 0; i < input[0].Length; i++)
{
    int energized = explore(('D', 0, i));
    if (energized > maxEnergized)
    {
        maxEnergized = energized;
    }
}
for (int i = 0; i < input[0].Length; i++)
{
    int energized = explore(('U', input.Length - 1, i));
    if (energized > maxEnergized)
    {
        maxEnergized = energized;
    }
}

Console.WriteLine(maxEnergized);

int explore((char, int, int) start)
{
    List<(int, int)> energized = [];
    List<(char, int, int)> toExplore = [start];
    List<(char, int, int)> explored = [];

    while (toExplore.Count > 0)
    {
        (char, int, int) curNode = toExplore[0];
        explored.Add(curNode);
        if (!energized.Contains((curNode.Item2, curNode.Item3)))
        {
            energized.Add((curNode.Item2, curNode.Item3));
        }
        char curChar = input[curNode.Item2][curNode.Item3];
        toExplore.RemoveAt(0);

        (int, int) nextCoords;
        List<(char, int, int)> found = [];

        switch (curChar)
        {
            case '.':
                switch (curNode.Item1)
                {
                    case 'R':
                        nextCoords = (curNode.Item2, curNode.Item3 + 1);
                        found.Add(('R', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'L':
                        nextCoords = (curNode.Item2, curNode.Item3 - 1);
                        found.Add(('L', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'U':
                        nextCoords = (curNode.Item2 - 1, curNode.Item3);
                        found.Add(('U', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'D':
                        nextCoords = (curNode.Item2 + 1, curNode.Item3);
                        found.Add(('D', nextCoords.Item1, nextCoords.Item2));
                        break;
                }
                break;
            case '/':
                switch (curNode.Item1)
                {
                    case 'R':
                        nextCoords = (curNode.Item2 - 1, curNode.Item3);
                        found.Add(('U', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'L':
                        nextCoords = (curNode.Item2 + 1, curNode.Item3);
                        found.Add(('D', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'U':
                        nextCoords = (curNode.Item2, curNode.Item3 + 1);
                        found.Add(('R', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'D':
                        nextCoords = (curNode.Item2, curNode.Item3 - 1);
                        found.Add(('L', nextCoords.Item1, nextCoords.Item2));
                        break;
                }
                break;
            case '\\':
                switch (curNode.Item1)
                {
                    case 'R':
                        nextCoords = (curNode.Item2 + 1, curNode.Item3);
                        found.Add(('D', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'L':
                        nextCoords = (curNode.Item2 - 1, curNode.Item3);
                        found.Add(('U', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'U':
                        nextCoords = (curNode.Item2, curNode.Item3 - 1);
                        found.Add(('L', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'D':
                        nextCoords = (curNode.Item2, curNode.Item3 + 1);
                        found.Add(('R', nextCoords.Item1, nextCoords.Item2));
                        break;
                }
                break;
            case '|':
                switch (curNode.Item1)
                {
                    case 'R':
                        nextCoords = (curNode.Item2 + 1, curNode.Item3);
                        found.Add(('D', nextCoords.Item1, nextCoords.Item2));
                        nextCoords = (curNode.Item2 - 1, curNode.Item3);
                        found.Add(('U', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'L':
                        nextCoords = (curNode.Item2 + 1, curNode.Item3);
                        found.Add(('D', nextCoords.Item1, nextCoords.Item2));
                        nextCoords = (curNode.Item2 - 1, curNode.Item3);
                        found.Add(('U', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'U':
                        nextCoords = (curNode.Item2 - 1, curNode.Item3);
                        found.Add(('U', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'D':
                        nextCoords = (curNode.Item2 + 1, curNode.Item3);
                        found.Add(('D', nextCoords.Item1, nextCoords.Item2));
                        break;
                }
                break;
            case '-':
                switch (curNode.Item1)
                {
                    case 'R':
                        nextCoords = (curNode.Item2, curNode.Item3 + 1);
                        found.Add(('R', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'L':
                        nextCoords = (curNode.Item2, curNode.Item3 - 1);
                        found.Add(('L', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'U':
                        nextCoords = (curNode.Item2, curNode.Item3 - 1);
                        found.Add(('L', nextCoords.Item1, nextCoords.Item2));
                        nextCoords = (curNode.Item2, curNode.Item3 + 1);
                        found.Add(('R', nextCoords.Item1, nextCoords.Item2));
                        break;
                    case 'D':
                        nextCoords = (curNode.Item2, curNode.Item3 - 1);
                        found.Add(('L', nextCoords.Item1, nextCoords.Item2));
                        nextCoords = (curNode.Item2, curNode.Item3 + 1);
                        found.Add(('R', nextCoords.Item1, nextCoords.Item2));
                        break;
                }
                break;

        }

        foreach ((char, int, int) node in found)
        {
            if (!(node.Item2 > input.Length - 1 || node.Item3 > input[0].Length - 1 || node.Item2 < 0 || node.Item3 < 0))
            {
                if (!explored.Contains(node))
                {
                    toExplore.Add(node);
                }
            }
        }
    }
    return energized.Count;
}