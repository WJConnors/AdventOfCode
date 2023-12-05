using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/text.txt"));

string seedLine = text[0].Substring(text[0].IndexOf(':') + 2);

List<double> seeds = new List<double>();
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
}