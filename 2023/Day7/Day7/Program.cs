using System.ComponentModel;
using System.Numerics;
using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");

List<(string, int, int)> input = new List<(string, int, int)>();

foreach (string line in text)
{
    string[] split = line.Split(" ");
    input.Add((split[0], int.Parse(split[1]), Strength(split[0])));
}

input.Sort((a, b) => a.Item3.CompareTo(b.Item3));

bool sorted = false;
while (!sorted)
{
    sorted = true;
    for (int i = 0; i < input.Count - 1; i++)
    {
        if (input[i].Item3 == input[i + 1].Item3)
        {
            if (IsGreater(input[i].Item1, input[i + 1].Item1))
            {
                (string, int, int) temp = input[i];
                input[i] = input[i + 1];
                input[i + 1] = temp;
                sorted = false;
            }
        }
    }
}
double total = 0;
for (int i = 0; i < input.Count; i++)
{
    total += input[i].Item2 * (i + 1);
}

Console.WriteLine(total);

int Strength(string hand)
{
    List<(char, int)> cards = new List<(char, int)>();
    foreach (char c in hand)
    {
        Boolean found = false;
        for (int i = 0; i < cards.Count; i++)
        {            
            if (c == cards[i].Item1)
            {
                cards[i] = (cards[i].Item1, cards[i].Item2 + 1);
                found = true;
            }
        }
        if (!found)
        {
            cards.Add((c, 1));
        }
    }
    bool pair = false;
    bool three = false;
    int max = 0;
    int best = 0;
    int jokers = 0;
    foreach ((char, int) card in cards)
    {
        if (card.Item1 == 'J')
        {
            jokers+= card.Item2;
            continue;
        }
        switch (card.Item2)
        {
            case 5:
                return 7;
            case 4:
                best = 6;
                max = 4;
                break;
            case 3:
                if (pair) { best = 5; }
                three = true;
                max = 3;
                break;
            case 2:
                if (three) { best =  5; }
                if (pair) { best = 3; }
                pair = true;
                if (max < 2) { max = 2; }
                break;
            case 1:
                if (max < 1) { max = 1; }
                break;
        }
    }
    if (best == 0)
    {
        if (three) { best = 4; }
        else if (pair) { best = 2; }
        else { best = 1; }
    }

    if (jokers == 0) { return best;  }
    else if (max + jokers == 5) { return 7; }
    else if (max + jokers == 4) { return 6; }
    else if ((best == 4 || best == 3) && jokers == 1) { return 5; }
    else if (max + jokers == 3) { return 4; }
    else if (max + jokers == 2) { return 2; }

    Console.WriteLine("Error: " + hand);
    return 0;
}

bool IsGreater (string a, string b)
{
    for (int i = 0; i < 5; i++)
    {
        if (a[i] == b[i]) { continue; }
        if (cardValue(a[i]) > cardValue(b[i])) { return true; }
        else { return false; }
    }
    return false;
}

int cardValue (char c)
{
    switch (c)
    {
        case 'T':
            return 10;
        case 'J':
            return 1;
        case 'Q':
            return 12;
        case 'K':
            return 13;
        case 'A':
            return 14;
        default:
            return int.Parse(c.ToString());
    }
}