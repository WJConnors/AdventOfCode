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

string nextDirection = maps[0].Item1;
int i = 0;
int steps = 0;

while (nextDirection != "ZZZ")
{
    if (i == directions.Length)
    {
        i = 0;
    }
    int index = maps.FindIndex(x => x.Item1 == nextDirection);
}