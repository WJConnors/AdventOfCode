using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");

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


int total1 = 0;
List<(char, int)> reflections = [];
foreach (List<string> section in input)
{
    int horiVal = HoriReflection(section, -1);
    if (horiVal != -1)
    {
        reflections.Add(('h', horiVal));
        total1 += (horiVal + 1) * 100;
    } else if (VertReflection(section, -1) != -1)
    {
        reflections.Add(('v', VertReflection(section, -1)));
        total1 += VertReflection(section, -1) + 1;
    }
}

Console.WriteLine(total1);

int total2 = 0;
for (int i = 0; i < input.Count; i++)
{
    bool found = false;
    List<string> temp = new List<string>(input[i]);
    for (int j = 0; j < temp.Count; j++)
    {
        for (int k = 0; k < temp[j].Length; k++)
        {
            char tempChar = temp[j][k];
            if (tempChar == '#')
            {
                char[] tempArrayRocks = temp[j].ToCharArray();
                tempArrayRocks[k] = '.';
                temp[j] = new string(tempArrayRocks);
            } else
            {
                char[] tempArrayAsh = temp[j].ToCharArray();
                tempArrayAsh[k] = '#';
                temp[j] = new string(tempArrayAsh);
            }
            int original = -1;
            if (reflections[i].Item1 == 'h')
            {
                original = reflections[i].Item2;
            } 
            int hr = HoriReflection(temp, original);
            if (reflections[i].Item1 == 'v')
            {
                original = reflections[i].Item2;
            }
            int vr = VertReflection(temp, original);
            if (hr != -1)
            {
                if (!(reflections[i].Item1 == 'h' && reflections[i].Item2 == hr))
                {
                    total2 += (hr + 1) * 100;
                    found = true;
                    break;
                }
            }
            if (vr != -1)
            {
                if (!(reflections[i].Item1 == 'v' && reflections[i].Item2 == vr))
                {
                    total2 += vr + 1;
                    found = true;
                    break;
                }
            }
            char[] tempArray = temp[j].ToCharArray();
            tempArray[k] = tempChar;
            temp[j] = new string(tempArray);
        }
        if (found) { break; }        
    }
    if (!found)
    {
        Console.WriteLine(i);
    }
}
Console.WriteLine(total2);





int HoriReflection (List<string> s, int original)
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
                if ((s[j] != s[k]))
                {
                    found = -1;
                    break;
                }
                j -= 1;
                k += 1;
            }
            if (found != -1 && found != original) { break; }
        }
    }

    return found;
}

int VertReflection (List<string> s, int original)
{
    int found = -1;
    List<string> n = [];

    for (int i = 0; i < s[0].Length; i++)
    {
        StringBuilder sb = new StringBuilder("", s.Count);
        foreach (string line in s)
        {
            sb.Append(line[i]);
        }
        n.Add(sb.ToString());
    }

    found = HoriReflection(n, original);

    return found;
}