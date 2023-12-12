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
    Console.WriteLine(gears[i]);
    List<int> arrangement = Arrangement(gears[i]);
    foreach (int j in arrangement)
    {
        Console.WriteLine(j);
    }
    Console.WriteLine();
}

for (int i = 0; i < gears.Count; i++)
{
    List<List<char>> output = Combinations(gears[i], groups[i]);
    Console.WriteLine(gears[i]);
    foreach(List<char> lc in output)
    {
        foreach (char c in lc)
        {
            Console.Write(c);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

List<int> Arrangement(string gear)
{
    List<int> ints = [];

    bool broken = false;
    foreach( char c in gear)
    {
        if (c == '#')
        {
            if (!broken)
            {
                broken = true;
                ints.Add(1);
                continue;
            }
            ints[ints.Count - 1] = ints.Last() + 1;
        } else
        {
            broken = false;
        }
    }

    return ints;
}

List<List<char>> Combinations(string gear, List<int> group)
{
    IEnumerable<IEnumerable<char>> combinations = [];

    int totalBroken = group.Sum();
    int foundBroken = gear.Count(x => x == '#');
    string questions = "";
    for (int i = 0; i < gear.Length; i++)
    {
        if (gear[i] == '?')
        {
            questions += i.ToString();
        }
    }
    int toAdd = totalBroken - foundBroken;
    if (questions.Count() > 0)
    {
        combinations = intCombos.Combinations(questions, foundBroken);
    }
    List<List<char>> listCombos = [];
    foreach (IEnumerable<char> combos in combinations)
    {
        listCombos.Add(combos.ToList());
    }
    return listCombos;
}

static class intCombos
{
    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
          elements.SelectMany((e, i) =>
            elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }
}