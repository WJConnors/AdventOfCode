using System.ComponentModel;
using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/test.txt");

List<(string, int)> input = new List<(string, int)>();

foreach (string line in text)
{
    string[] split = line.Split(" ");
    input.Add((split[0], int.Parse(split[1])));
}

char Strength(string hand)
{
    List<(char, int)> card = new List<(char, int)>();
    foreach (char c in hand)
    {
        Boolean found = false;
        for (int i = 0; i < card.Count; i++)
        {            
            if (c == card[i].Item1)
            {
                card[i] = (card[i].Item1, card[i].Item2 + 1);
                found = true;
            }
        }
        if (!found)
        {
            card.Add((c, 1));
        }
    }

    return '0';
}