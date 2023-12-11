using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Xml.Linq;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/test.txt"));

List<int> emptyRows = [];
List<int> emptyCols = [];
for (int i = 0; i < text.Count; i++)
{
    if (!text[i].Any(x => x != '.')) { emptyRows.Add(i); }
}
for (int i = 0; i < text[0].Length; i++)
{
    bool galFound = false;
    foreach (string line in text) { if (line[i] == '#') { galFound = true; } }
    if (!galFound) { emptyCols.Add(i); }
}
int inserted = 0;
foreach (int i in emptyRows)
{
    for (int j = 0; j < 10; j++)
    {
        text.Insert(i + inserted, text[i + inserted]);
        inserted++;
    }
}
inserted = 0;
foreach (int i in emptyCols)
{
    for (int j = 0; j < text.Count; j++)
    {
        for (int k = 0; k < 10; k++)
        {
            text[j] = text[j].Insert(i + inserted + k, ".");
        }
    }
    inserted++;
}

List<(int, int)> galaxies = [];
for (int i = 0; i < text.Count; i++)
{
    for (int j = 0; j < text[i].Length; j++)
    {
        if (text[i][j] == '#')
        {
            galaxies.Add((i, j));
        }
    }
}

foreach (string line in text)
{
    Console.WriteLine(line);
}

int total = 0;
foreach ((int, int) i in galaxies)
{
    foreach ((int, int) j in galaxies)
    {
        if (galaxies.IndexOf(i) <= galaxies.IndexOf(j)) { continue; }
        total += (Math.Abs(i.Item1 - j.Item1) + Math.Abs(i.Item2 - j.Item2));
    }
}

Console.WriteLine(total);