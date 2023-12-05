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

List<Dictionary<int, int>> map = new List<Dictionary<int, int>>();
int curMap = 0;
AddNewMap();
Boolean added = false;
foreach (string line in text)
{
    if (line == "") continue;
    if (Char.IsNumber(line, 0)) {
        string[] strNums = line.Split(' ');
        int[] nums = Array.ConvertAll(strNums, int.Parse);
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
        curMap++;
        AddNewMap();
        added = false;
    }
}

int lowest = -1;
foreach (int seed in seeds)
{
    int dictValue = seed;
    foreach (Dictionary<int, int> dict in map)
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
    double max = 0;
    foreach (Dictionary<int, int> dict in map)
    {
        if (dict.Values.Max() > max)
        {
            max = dict.Values.Max();
        }
    }
    max = Math.Max(max, seeds.Max());
    map.Add(new Dictionary<int, int>());
    for (int i = 0; i <= max; i++)
    {
        map[curMap].Add(i, i);
    }
}