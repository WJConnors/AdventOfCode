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

List<List<(int, int)>> map = new List<List<(int, int)>>();
int curMap = 0;
foreach (string line in text)
{
    if (line == "")
    {
        if (map[curMap].Any())
        {
            curMap++;
        }
        continue;
    }
    else if (!Char.IsNumber(line, 0))
    {
        map[curMap] = new List<(int, int)>();
    } else
    {
        string[] strNums = line.Split(' ');
        int[] nums = Array.ConvertAll(strNums, int.Parse);
        for (int i  = 0; i < nums[2]; i++)
        {
            map[curMap].Add((nums[0] + i, nums[1]  + i));
        }
    }
}