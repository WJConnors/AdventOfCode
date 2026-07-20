using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/text.txt"));

string directions = text[0];
text.RemoveRange(0, 2);

List<(string, string, string)> maps = new List<(string, string, string)>();

foreach (string line in text)
{
    maps.Add((line.Substring(0, 3), line.Substring(7, 3), line.Substring(12, 3)));
}

List<string> nextDirections = new List<string>();
foreach ((string, string, string) map in maps)
{
    if (map.Item1[2] == 'A')
    {
        nextDirections.Add(map.Item1);
    }
}

List<double> multiples = [];
foreach (string direction in nextDirections)
{
    string curDirection = direction;
    int i = 0;
    int steps = 0;
    while (curDirection[2] != 'Z')
    {
        int index = maps.FindIndex(x => x.Item1 == curDirection);
        curDirection = directions[i] == 'L' ? maps[index].Item2 : maps[index].Item3;
        steps++;
        i = i == directions.Length - 1 ? 0 : i + 1;
    }
    multiples.Add(steps);
}

double lcm = LCM(multiples[0], multiples[1]);
for (int i = 1; i < multiples.Count; i++)
{
    lcm = LCM(lcm, multiples[i]);
}

Console.WriteLine(lcm);

double LCM (double a, double b)
{
    return (a * b) / GCD(a, b);
}

double GCD(double a, double b)
{
    if (a == 0)
        return b;
    return GCD(b % a, a);
}

/*int i = 0;
int steps = 0;
bool completed = false;

while (!completed)
{
    for (int j = 0; j < nextDirections.Count; j++)
    {
        int index = maps.FindIndex(x => x.Item1 == nextDirections[j]);
        nextDirections[j] = directions[i] == 'L' ? maps[index].Item2 : maps[index].Item3;
    }
    completed = true;
    foreach (string direction in nextDirections)
    {
        if (direction[2] != 'Z') { completed = false; }
    }
    steps++;
    i = i == directions.Length - 1 ? 0 : i + 1;
}
Console.WriteLine(steps);*/