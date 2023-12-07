using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/test.txt");

List<(string, int)> input = new List<(string, int)>();

foreach (string line in text)
{
    string[] split = line.Split(" ");
    input.Add((split[0], int.Parse(split[1])));
}