using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/test.txt"));

List<string> gears = [];
List<string> strGroups = [];
List<List<int>> groups = [];
foreach (string line in text)
{
    string[] splitLine = line.Split(" ");
    gears.Add(splitLine[0]);
    strGroups.Add(splitLine[1]);
}
for (int i = 0; i < strGroups.Count; i++)
{
    string[] splitgroups = strGroups[i].Split(",");
    groups.Add(new List<int>());
    foreach (string group in splitgroups)
    {
        groups[i].Add(int.Parse(group));
    }
}

for (int i = 0; i < gears.Count; i ++)
{
    Console.WriteLine(gears[i] + " " + strGroups[i]);
}