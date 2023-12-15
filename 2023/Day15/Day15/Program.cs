using System.Reflection;
using System.Text;

String text = File.ReadAllText("text.txt");

string[] strings = text.Split(",");
List<List<(string, int)>> boxes = [];
List<(string,string)> input = [];
for (int i = 0; i < 256; i++)
{
    boxes.Add([]);
}
foreach (string s in strings)
{
    List<char> chars1 = [];
    List<char> chars2 = [];
    bool found = false;
    for (int i = 0; i < s.Length; i++)
    {
        if (s[i] == '=' || s[i] == '-') {found = true; }
        if (!found)
        {
            chars1.Add(s[i]);
        } else
        {
            chars2.Add(s[i]);
        }
    }
    input.Add((new string(chars1.ToArray()), new string(chars2.ToArray())));
}


foreach ((string, string) i in input)
{
    string label = i.Item1;
    int box = Hash(label);
    char operation = i.Item2[0];
    if (operation == '=')
    {
        int focal = int.Parse(i.Item2[1..]);
        if (boxes[box].Count == 0) { boxes[box].Add((label, focal)); continue; }
        bool found = false;
        for (int j = 0; j < boxes[box].Count; j++)
        {
            if (boxes[box][j].Item1 == label)
            {
                boxes[box][j] = (label, focal);
                found = true;
                break;
            }
            
        }
        if (!found)
        {
            boxes[box].Add((label, focal));
        }
    } else
    {
        for (int j = 0; j < boxes[box].Count; j++)
        {
            if (boxes[box][j].Item1 == label)
            {
                boxes[box].RemoveAt(j);
                break;
            }
        }
    }
}
int total = 0;
for (int i = 0; i < boxes.Count; i++)
{
    for (int j = 0;j < boxes[i].Count; j++)
    {
        total += (i + 1) * (j + 1) * boxes[i][j].Item2;
    }
}

Console.WriteLine(total);

int Hash (string s)
{
    int curVal = 0;
    foreach (char c in s)
    {
        int ascii = (int)c;
        curVal += ascii;
        curVal *= 17;
        curVal %= 256;
    }
    return curVal;
}