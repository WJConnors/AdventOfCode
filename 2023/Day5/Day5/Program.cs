using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/text.txt"));

string seedLineStr = text[0].Substring(text[0].IndexOf(':') + 2);


List<double> seedLine = new List<double>();
foreach (string str in seedLineStr.Split(' '))
{
    seedLine.Add(double.Parse(str));
}
text.RemoveAt(0);

List<(double, double)> seedPairs = new List<(double, double)>();
for (int i = 0; i < seedLine.Count - 1; i = i + 2)
{
    seedPairs.Add((seedLine[i], seedLine[i + 1]));
}

List < List < (double, double, double)>> maps = new List<List<(double, double, double)>>();
int curMap = 0;
maps.Add(new List<(double, double, double)>());
Boolean added = false;
foreach (string line in text)
{
    if (line == "") continue;
    if (Char.IsNumber(line, 0))
    {
        string[] strNums = line.Split(' ');
        double[] nums = Array.ConvertAll(strNums, double.Parse);
        maps[curMap].Add((nums[0], nums[1], nums[2]));
        added = true;
    } else if (added)
    {
        curMap++;
        maps.Add(new List<(double, double, double)>());
    }
}

double maxDecrease = 0;
foreach (List<(double, double, double)> map in maps)
{
    double curDecrease = 0;
    foreach ((double, double, double) values in map)
    {
        if (values.Item1 - values.Item2 < curDecrease)
        {
            curDecrease = values.Item1 - values.Item2;
        }
    }
    maxDecrease += curDecrease;
}
Console.WriteLine(maxDecrease);

double lowest = 0;
foreach ((double, double) pair in seedPairs)
{
    Console.WriteLine(pair);
    for (int i = 0; i < pair.Item2; i++)
    {
        double seed = pair.Item1 + i;
        foreach (List<(double, double, double)> map in maps)
        {
            foreach ((double, double, double) values in map)
            {
                if (seed >= values.Item2 && seed < values.Item2 + values.Item3)
                {
                    seed = seed + (values.Item1 - values.Item2);
                    break;
                }
            }
        }
        if (lowest == 0 || seed < lowest)
        {
            lowest = seed;
        }
    }
}

Console.WriteLine(lowest);

















/*List<double> seeds = new List<double>();
foreach (string str in seedLine.Split(' '))
{
    seeds.Add(double.Parse(str));
}
text.RemoveAt(0);

List<Dictionary<double, double>> map = new List<Dictionary<double, double>>();
int curMap = 0;
AddNewMap();
Boolean added = false;
foreach (string line in text)
{
    Console.WriteLine(line);
    if (line == "") continue;
    if (Char.IsNumber(line, 0)) {
        string[] strNums = line.Split(' ');
        double[] nums = Array.ConvertAll(strNums, double.Parse);
        for (int i = 0; i < nums[2]; i++)
        {
            if (map[curMap].ContainsKey(nums[1] + i))
            {
                map[curMap][nums[1] + i] = nums[0] + i;
            }
        }
        added = true;
    } else if (added)
    {
        Console.WriteLine("added");
        curMap++;
        AddNewMap();
        added = false;
    }
}

double lowest = -1;
foreach (double seed in seeds)
{
    double dictValue = seed;
    foreach (Dictionary<double, double> dict in map)
    {
        dictValue = dict[dictValue];
    }
    if (lowest == -1 || dictValue < lowest)
    {
        lowest = dictValue;
    }
}

Console.WriteLine(lowest);

void AddNewMap()
{
    map.Add(new Dictionary<double, double>());
    foreach (double seed in seeds)
    {
        if (!map[curMap].ContainsKey(seed))
        {
            map[curMap].Add(seed, seed);
        }
    }
    foreach (Dictionary<double, double> dict in map)
    {
        foreach (double value in dict.Values)
        {
            if (!map[curMap].ContainsKey(value))
            {
                map[curMap].Add(value, value);
            }
        }
    }
}*/