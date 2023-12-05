using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/test.txt"));

string seedLine = text[0].Substring(text[0].IndexOf(':') + 2);

List<int> seeds = new List<int>();
foreach (string str in seedLine.Split(' '))
{
    seeds.Add(int.Parse(str));
}
text.RemoveAt(0);

List<Dictionary<int, int>> map = new List<Dictionary<int, int>>();
int curMap = 0;
AddNewMap();
Boolean added = false;
foreach (string line in text)
{
    Console.WriteLine(line);
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
        Console.WriteLine("Added");
        curMap++;
        added = false;
    }
}

foreach (Dictionary<int, int> dict in map)
{
    for (int x = 0; x < dict.Count; x++)
    {
        Console.WriteLine("{0} and {1}", dict.Keys.ElementAt(x), dict[dict.Keys.ElementAt(x)]);
    }
}

void AddNewMap()
{
    map.Add(new Dictionary<int, int>());
    foreach (int seed in seeds)
    {
        map[curMap].Add(seed, seed);
    }
}