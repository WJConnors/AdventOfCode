using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");


List<(char, (int, int), (int, int))> pipes = [];
pipes.Add(('|', (-1, 0), (1, 0)));
pipes.Add(('-', (0, -1), (0, 1)));
pipes.Add(('L', (-1, 0), (0, 1)));
pipes.Add(('J', (-1, 0), (0, -1)));
pipes.Add(('7', (0, -1), (1, 0)));
pipes.Add(('F', (0, 1), (1, 0)));
pipes.Add(('.', (0, 0), (0, 0)));
pipes.Add(('S', (0, 0), (0, 0)));

(int, int) s = FindS();
List<List<(int, int)>> paths = [];

for (int i = -1; i <= 1; i++)
{
    for (int j = -1; j <= 1; j++)
    {
        if (i == 0 && j == 0) { continue; }
        (int, int) newCoord = (s.Item1 + i, s.Item2 + j);
        if (Enumerable.Range(0, text.Length).Contains(newCoord.Item1) && Enumerable.Range(0, text[0].Length).Contains(newCoord.Item2))
        {
            char newPipe = text[newCoord.Item1][newCoord.Item2];
            if (newPipe != '.')
            {
                paths.Add(new List<(int, int)> { newCoord });
            }
        }        
    }
}

List<(int, int)> loop = [];
for (int i = 0; i < paths.Count; i++)
{
    bool found = false;
    (int, int) coord = paths[i][0];
    (int, int) oldCoord = s;
    (int, int) newCoord;
    while (true)
    {
        char pipe = text[coord.Item1][coord.Item2];
        (int, int) tempCoord = Check(pipe, coord, oldCoord);
        if (tempCoord == (0,0))
        {
            break;
        }
        newCoord = (coord.Item1 + tempCoord.Item1, coord.Item2 + tempCoord.Item2);
        oldCoord = coord;
        coord = newCoord;

        if (newCoord == s) 
        { 
            found = true;
            break;
        } else
        {
            paths[i].Add(newCoord);
            newCoord = paths[i].Last();
        }
    }
    if (found)
    {
        loop = paths[i];
        break;
    }
}

int distance = ((loop.Count + 1) / 2);
Console.WriteLine(distance);

List<List<char>> map = new List<List<char>>();
for (int i = 0; i < text.Length; i++)
{
    map.Add(new List<char>());
    for (int j = 0; j < text[i].Length; j++)
    {
        if (!loop.Contains((i, j)) && s != (i, j))
        {
            Console.Write(" ");
            map[i].Add(' ');
        } else
        {
            Console.Write(text[i][j]);
            map[i].Add(text[i][j]);
        }
    }
    Console.WriteLine();
}

int quarter = 35;
int count = 0;
for (int i = 35; i < 105; i ++)
{
    for (int j = 35; j < 105; j++)
    {
        if (map[i][j] == ' ')
        {
            count++;
        }
    }
}
Console.WriteLine(count);


/*for (int i = 0; i < map.Count; i++)
{
    for (int j = 0; j < map[i].Count; j++)
    {
        if (!loop.Contains((i, j))) { continue; }
        int hits = 0;
        for (int k = j; k < 140; k++)
        {
            if (loop.Contains((i, k)))
            {
                hits++;
                break;
            }
        }
        for (int k = 0; k < j; k++)
        {
            if (loop.Contains((i, k)))
            {
                hits++;
                break;
            }
        }
        for (int k = i; k < 140; k++)
        {
            if (loop.Contains((k, j)))
            {
                hits++;
                break;
            }
        }
        for (int k = 0; k < i; k++)
        {
            if (loop.Contains((k, j)))
            {
                hits++;
                break;
            }
        }
        if (hits < 4)
        {
            map[i][j] = '0';
        }
    }
}

bool changed = true;
while (changed)
{
    changed = false;
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Count; j++)
        {
            if (map[i][j] == ' ')
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        if ((k == 0 && l == 0) || i + k < 0 || j + l < 0 || i + k > 139 || j + l > 139) { continue; }
                        if (map[i + k][j + l] == '0')
                        {
                            map[i][j] = '0';
                            changed = true;
                        }
                    }
                }
            }
        }
    }
}


int count = 0;
for (int i = 0; i < text.Length; i++)
{
    for (int j = 0; j < text[i].Length; j++)
    {
        Console.Write(map[i][j]);
        if (map[i][j] == ' ')
        {
            count++;
        }
    }
    Console.WriteLine();
}
Console.WriteLine(count);*/


(int, int) Check (char pipe, (int, int) curCoord, (int, int) oldCoord)
{
    int index = pipes.FindIndex(x => x.Item1 == pipe);
    (int, int) oldDif = (oldCoord.Item1 - curCoord.Item1, oldCoord.Item2 - curCoord.Item2);
    (int, int) newCoord;
    (int, int) newCoordDif;
    if (oldDif == pipes[index].Item2)
    {
        newCoordDif = pipes[index].Item3;
        newCoord = (curCoord.Item1 + newCoordDif.Item1, curCoord.Item2 + newCoordDif.Item2);
    }
    else if (oldDif == pipes[index].Item3)
    {
        newCoordDif = pipes[index].Item2;
        newCoord = (curCoord.Item1 + newCoordDif.Item1, curCoord.Item2 + newCoordDif.Item2);
    }
    else { return (0, 0); }

    if (!Enumerable.Range(0, text.Length).Contains(newCoord.Item1) || !Enumerable.Range(0, text[0].Length).Contains(newCoord.Item2)) { return (0, 0); }

    char newPipe = text[newCoord.Item1][newCoord.Item2];
    int index2 = pipes.FindIndex(x => x.Item1 == newPipe);
    if (newPipe == 'S')
    {
        return (s.Item1 - curCoord.Item1, s.Item2 - curCoord.Item2);
    } else
    if (pipes[index2].Item2 == (-newCoordDif.Item1, -newCoordDif.Item2) || pipes[index2].Item3 == (-newCoordDif.Item1, -newCoordDif.Item2))
    {
        if (pipes[index].Item2 == oldDif)
        {
            return pipes[index].Item3;
        }
        else if (pipes[index].Item3 == oldDif)
        {
            return pipes[index].Item2;
        }
    }

    return (0, 0);
}

(int, int) FindS ()
{
    for (int i = 0; i < text.Length; i++)
    {
        for (int j = 0; j < text[i].Length; j++)
        {
            if (text[i][j] == 'S') { return (i, j);  }
        }
    }

    Console.WriteLine("Error");
    return (-1, -1);
}