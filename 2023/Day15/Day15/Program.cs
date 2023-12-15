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