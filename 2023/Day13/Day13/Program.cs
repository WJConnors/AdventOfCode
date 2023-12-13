using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/test.txt");

List<List<string>> input = [];
input.Add([]);
foreach (string line in text)
{
    if (line != "")
    {
        input.Last().Add(line);
    } else
    {
        input.Add([]);
    }
}

foreach (List<string> section in input)
{
    Console.WriteLine(HoriReflection(section));
    Console.WriteLine(VertReflection(section));
}

int HoriReflection (List<string> s)
{
    int found = -1;

    for (int i = 0; i < s.Count - 1; i++)
    {
        if (s[i] == s[i + 1])
        {
            found = i;
            int j = i;
            int k = i + 1;
            while (j >= 0 && k < s.Count)
            {
                Console.WriteLine(s[j]);
                Console.WriteLine(s[k]);
                if ((s[j] != s[k]))
                {
                    found = -1;
                    break;
                }
                j -= 1;
                k += 1;
            }
            if (found != -1) { break; }
        }
    }

    return found;
}

int VertReflection (List<string> s)
{
    int found = -1;
    List<string> n = [];

    for (int i = 0; i < s[0].Length; i++)
    {
        n.Add("");
        foreach (string line in s)
        {
            n.Last().Append(line[i]);
        }
    }

    foreach (string line in n)
    {
        Console.WriteLine(line);
    }

    found = HoriReflection(n);

    return found;
}