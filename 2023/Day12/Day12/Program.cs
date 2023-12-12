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

List<List<List<int>>> permutations = [];
for (int i = 0; i < gears.Count; i++)
{
    permutations.Add(new List<List<int>>());
    permutations[i].Add(Combinations(gears[i], groups[i]));    
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

List<List<int>> Combinations(string gear, List<int> group)
{
    IEnumerable<IEnumerable<int>> combinations = [];

    int totalBroken = group.Sum();
    int foundBroken = gear.Count(x => x == '#');
    List<int> questions = [];
    for (int i = 0; i < gear.Length; i++)
    {
        if (gear[i] == '?')
        {
            questions.Add(i);
        }
    }
    int toAdd = totalBroken - foundBroken;
    if (questions.Count() > 0)
    {
        Console.WriteLine(questions + " " + foundBroken);
        combinations = intCombos.GetPermutations(questions, foundBroken);
    }
    List<List<int>> listCombos = [];
    foreach (IEnumerable<int> combos in combinations)
    {
        listCombos.Add(combos.ToList());
    }
    return listCombos;
}

static class intCombos
{
    public static IEnumerable<IEnumerable<T>>
    GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}