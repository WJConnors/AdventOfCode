string[] input = File.ReadAllLines("test.txt");

PriorityQueue<Node, int> toVisit = new();
toVisit.Enqueue(new Node(0, 0, 0, [' ', ' ', ' '], [(0, 0)]), 0);
int shortestPath = int.MaxValue;
List<(int, int)> surroundings = [(0, 1), (0, -1), (1, 0), (-1, 0)];
List<(int, int, char)> visited = [];

CellInfo[,] curShortest = new CellInfo[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[0].Length; j++)
    {
        curShortest[i, j] = new CellInfo();
    }
}
List<(int, int)> history = [];
while (toVisit.Count > 0)
{
    Node curNode = toVisit.Dequeue();
    if (visited.Contains((curNode.X, curNode.Y, curNode.Direction[0])))
    {
        continue;
    } else
    {
        visited.Add((curNode.X, curNode.Y, curNode.Direction[0]));
    }

    Console.WriteLine(curNode.X + " " + curNode.Y + " " + curNode.Path + " " + curNode.Direction[0]  + " " + curNode.Path);

    if (curNode.X == input.Length - 1 && curNode.Y == input[0].Length - 1)
    {
        Console.WriteLine("Found");
        if (curNode.Path < shortestPath)
        {
            shortestPath = curNode.Path;
            Console.WriteLine(shortestPath);
            history = curNode.History;
            continue;
        }
        else if (curNode.Path > shortestPath)
        {
            continue;
        }

    }
    (int, int) limit = (0, 0);
    if (curNode.Direction[0] == 'U' && curNode.Direction[1] == 'U' && curNode.Direction[2] == 'U')
    {
        limit = (curNode.X - 1, curNode.Y);
    }
    else if (curNode.Direction[0] == 'D' && curNode.Direction[1] == 'D' && curNode.Direction[2] == 'D')
    {
        limit = (curNode.X + 1, curNode.Y);
    }
    else if (curNode.Direction[0] == 'L' && curNode.Direction[1] == 'L' && curNode.Direction[2] == 'L')
    {
        limit = (curNode.X, curNode.Y - 1);
    }
    else if (curNode.Direction[0] == 'R' && curNode.Direction[1] == 'R' && curNode.Direction[2] == 'R')
    {
        limit = (curNode.X, curNode.Y + 1);
    }
    List<Node> foundNodes = [];
    foreach ((int, int) s in surroundings)
    {
        char direction = ' ';
        if (s.Item1 == 1)
        {
            direction = 'D';
        }
        else if (s.Item1 == -1)
        {
            direction = 'U';
        }
        else if (s.Item2 == 1)
        {
            direction = 'R';
        }
        else if (s.Item2 == -1)
        {
            direction = 'L';
        }
        (int, int) newNode = (curNode.X + s.Item1, curNode.Y + s.Item2);
        if (newNode == limit)
        {
            continue;
        }
        if (curNode.Direction.Count > 0)
        {

            if (curNode.Direction[0] == 'U' && direction == 'D')
            {
                continue;
            }
            else if (curNode.Direction[0] == 'D' && direction == 'U')
            {
                continue;
            }
            else if (curNode.Direction[0] == 'L' && direction == 'R')
            {
                continue;
            }
            else if (curNode.Direction[0] == 'R' && direction == 'L')
            {
                continue;
            }
        }
        if (newNode.Item1 < 0 || newNode.Item1 >= input.Length || newNode.Item2 < 0 || newNode.Item2 >= input[0].Length)
        {
            continue;
        }
        if (curNode.History.Contains(newNode))
        {
            continue;
        }
        int newPath = curNode.Path + int.Parse(input[newNode.Item1][newNode.Item2].ToString());
        if (newPath >= shortestPath)
        {
            continue;
        }
        List<char> newDirection = new(curNode.Direction);
        newDirection.Insert(0, direction);
        List<(int, int)> newHistory = new(curNode.History) { (newNode.Item1, newNode.Item2) };
        if (!curShortest[newNode.Item1, newNode.Item2].History.TryAdd(direction, newPath))
        {
            continue;
        }
        foundNodes.Add(new Node(newNode.Item1, newNode.Item2, newPath, newDirection, newHistory));
        foreach (Node n in foundNodes)
        {
            toVisit.Enqueue(n, n.Path);
        }
    }
}

int total = 0;
foreach ((int, int) h in history)
{
    if (h.Item1 == 0 && h.Item2 == 0) continue;
    total += int.Parse(input[h.Item1][h.Item2].ToString());
    Console.WriteLine(h + " " + input[h.Item1][h.Item2] + " " + total);
}
Console.WriteLine(shortestPath);


struct Node(int x, int y, int path, List<char> direction, List<(int, int)> history)
{
    public int X = x;
    public int Y = y;
    public int Path = path;
    public List<char> Direction = direction;
    public List<(int, int)> History = history;
}

struct CellInfo()
{
    public Dictionary<char, int> History = [];
}