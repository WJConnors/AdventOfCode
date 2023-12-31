﻿string[] input = File.ReadAllLines("test.txt");

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
    List<char> thisDirection = [curNode.Direction[0]];
    if (curNode.Direction[1] == thisDirection[0])
    {
        thisDirection.Add(curNode.Direction[1]);
        if (curNode.Direction[2] == thisDirection[0])
        {
            thisDirection.Add(curNode.Direction[2]);
        }
    }
    if (!curShortest[curNode.X, curNode.Y].History.TryAdd((thisDirection), curNode.Path))
    {
        continue;
    }
    //Console.WriteLine(curNode.X + " " + curNode.Y + " " + curNode.Path + " " + curNode.Direction[0]  + " " + curNode.Path);

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
        List<char> curDirection = [newDirection[0]];
        if (newDirection[1] == curDirection[0] )
        {
            curDirection.Add(newDirection[1]);
            if (newDirection[2] == curDirection[0])
            {
                curDirection.Add(newDirection[2]);
            }
        }
        if (curShortest[newNode.Item1, newNode.Item2].History.ContainsKey(curDirection))
        {
            continue;
        }
        foundNodes.Add(new Node(newNode.Item1, newNode.Item2, newPath, newDirection, newHistory));        
    }
    foreach (Node n in foundNodes)
    {
        toVisit.Enqueue(n, n.Path);
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
    public Dictionary<List<char>, int> History = [];
}

/*from heapq import heappop, heappush

with open('text.txt') as f:
    puzzle_input = f.read()


def part1(puzzle_input):
    grid = [[int(d) for d in line] for line in puzzle_input.split('\n')]
    m, n = len(grid), len(grid[0])
    directions = [(-1, 0), (0, -1), (0, 1), (1, 0)]

    # tuple: (heat-loss, x-coord, y-coord, length-of-current-run, x-direction, y-direction)
    q = [(0, 0, 0, 0, 0, 0)]
    visited = set()
    while q:
        loss, x, y, k, dx, dy = heappop(q)

        if x == m-1 and y == n-1:
            break

        if any((x, y, k_, dx, dy) in visited for k_ in range(1, k + 1)):
            continue

        visited.add((x, y, k, dx, dy))
        for new_dx, new_dy in directions:
            straight = (new_dx == dx and new_dy == dy)
    new_x, new_y = x + new_dx, y + new_dy

            if any((new_dx == -dx and new_dy == -dy,
                    k == 3 and straight,
                    new_x < 0, new_y < 0,
                    new_x == m, new_y == n)):
                continue

            new_k = k + 1 if straight else 1
            heappush(q, (loss + grid[new_x][new_y], new_x, new_y, new_k, new_dx, new_dy))

    return loss


def part2(puzzle_input):
    grid = [[int(d) for d in line] for line in puzzle_input.split('\n')]
    m, n = len(grid), len(grid[0])
    directions = [(-1, 0), (0, -1), (0, 1), (1, 0)]

    # tuple: (heat-loss, x-coord, y-coord, length-of-current-run, x-direction, y-direction)
    q = [(0, 0, 0, 0, 0, 1), (0, 0, 0, 0, 1, 0)]
    visited = set()
    while q:
        loss, x, y, k, dx, dy = heappop(q)

        if x == m-1 and y == n-1:
            if k < 4:
                continue
            break

        if (x, y, k, dx, dy) in visited:
            continue

        visited.add((x, y, k, dx, dy))

        for new_dx, new_dy in directions:
            straight = (new_dx == dx and new_dy == dy)
    new_x, new_y = x + new_dx, y + new_dy

            if any((new_dx == -dx and new_dy == -dy,
                    k == 10 and straight,
                    k < 4 and not straight,
                    new_x < 0, new_y < 0,
                    new_x == m, new_y == n)):
                continue

            new_k = k + 1 if straight else 1
            heappush(q, (loss + grid[new_x][new_y], new_x, new_y, new_k, new_dx, new_dy))

    return loss


print('Part 1:', part1(puzzle_input))
print('Part 2:', part2(puzzle_input))*/