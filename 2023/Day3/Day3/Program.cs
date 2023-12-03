using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");

List <Tuple<int,int>> gears = new List<Tuple<int, int>>();
List <List<int>> gearNums = new List<List<int>>();
int total = 0;

for (int i = 0; i < text.Length; i++)
{
    string line = text[i];
    for (int j = 0; j < line.Length; j++)
    {
        if (!Char.IsNumber(line[j]) && line[j] == '*')
        {
            gears.Add(new Tuple<int, int>(i, j));
        }
    }
}

foreach (Tuple<int, int> gear in gears)
{
    gearNums.Add(new List<int>());
}

for (int i = 0; i < text.Length; i++)
{
    List <int> foundGears = new List<int>();
    string line = text[i];
    for (int j = 0; j < line.Length; j++)
    {
        List<char> numbers = new List<char>();
        if (Char.IsNumber(line[j]))
        {
            numbers.Add(line[j]);
            if (IsCounted(i, j) != -1)
            {
                foundGears.Add(IsCounted(i, j));
            }
            while (j + 1 < line.Length)
            {
                if (Char.IsNumber(line[++j]))
                {
                    numbers.Add(line[j]);
                    if (IsCounted(i, j) != -1)
                    {
                        if (!foundGears.Contains(IsCounted(i, j)))
                        {
                            foundGears.Add(IsCounted(i, j));
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        if (foundGears.Count > 0 && numbers.Count > 0)
        {
            foreach (int gear in foundGears)
            {
                gearNums[gear].Add(Int32.Parse(new string(numbers.ToArray())));
            }
        }
        foundGears.Clear();
    }
}

for (int i = 0; i < gearNums.Count; i++)
{
    if (gearNums[i].Count == 2)
    {
        total += gearNums[i][0] * gearNums[i][1];
    }
}

Console.WriteLine(total);


int IsCounted(int i, int j)
{
    Tuple<int, int>[] mods = [
        new Tuple<int, int>(-1, -1),
        new Tuple<int, int>(-1, 0),
        new Tuple<int, int>(-1, 1),
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(1, -1),
        new Tuple<int, int>(1, 0),
        new Tuple<int, int>(1, 1),
    ];

    foreach (Tuple<int, int> mod in mods)
    {
        Tuple<int, int> current = new Tuple<int, int>(i + mod.Item1, j + mod.Item2);
        if (gears.Contains(current))
        {
            return gears.IndexOf(current);
        }
    }

    return -1;
}

/*for (int i = 0; i < text.Length; i++)
{
    bool counted = false;
    string line = text[i];
    for (int j = 0; j < line.Length; j++)
    {
        List<char> numbers = new List<char>();
        if (Char.IsNumber(line[j]))
        {
            numbers.Add(line[j]);
            counted = IsCounted(i, j);
            while (j + 1 < line.Length)
            {
                if (Char.IsNumber(line[++j]))
                {
                    numbers.Add(line[j]);
                    if (IsCounted(i, j))
                    {
                        counted = true;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        if (counted && numbers.Count > 0)
        {
            total += Int32.Parse(new string(numbers.ToArray()));
        }
    }
}

Console.WriteLine(total);*/