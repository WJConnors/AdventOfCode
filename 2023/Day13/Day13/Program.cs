using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;

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

int total = 0;
for (int i = 0; i < input.Count; i++)
{
    bool found = false;
    List<string> temp = new List<string>(input[i]);
    for (int j = 0; j < temp.Count; j++)
    {
        for (int k = j + 1; k < temp[j].Length; k++)
        {
            char tempChar = temp[j][k];
            if (tempChar == '#')
            {
                char[] tempArray = temp[j].ToCharArray();
                tempArray[k] = '.';
                temp[j] = new string(tempArray);
            } else
            {
                char[] tempArray = temp[j].ToCharArray();
                tempArray[k] = '#';
                temp[j] = new string(tempArray);
            }
            int hr = HoriReflection(temp);
            int vr = VertReflection(temp);
            if (hr != -1)
            {
                total += (hr + 1) * 100;
                found = true;
                break;
            }
            if (vr != -1)
            {
                total += vr + 1;
                found = true;
                break;
            }
            char[] tempArray = temp[j].ToCharArray();
            tempArray[k] = tempChar;
            temp[j] = new string(tempArray);
        }
        if (found) { break; }
    }
}

/*int total = 0;
foreach (List<string> section in input)
{
    int horiVal = HoriReflection(section);
    if (horiVal != -1)
    {
        total += (horiVal + 1) * 100;
    } else if (VertReflection(section) != -1)
    {
        total += VertReflection(section) + 1;
    } else
    {
        Console.WriteLine("Error");
    }
}*/

Console.WriteLine(total);

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
        StringBuilder sb = new StringBuilder("", s.Count);
        foreach (string line in s)
        {
            sb.Append(line[i]);
        }
        n.Add(sb.ToString());
    }

    found = HoriReflection(n);

    return found;
}