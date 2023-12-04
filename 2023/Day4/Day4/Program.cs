using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

int[] numCards = new int[text.Length];
int card = 0;
int total = 0;

foreach (string line in text)
{
    string winning = line.Substring(line.IndexOf(':') + 2, 30);
    string chosen = line.Substring(line.IndexOf('|') + 2);
    List<int> winNums = new List<int>();
    List<int> choNums = new List<int>();
    for (int i = 0; i < winning.Length; i++)
    {
        List<char> numbers = new List<char>();
        if (char.IsNumber(winning[i]))
        {
            numbers.Add(winning[i]);
            if (char.IsNumber(winning[++i]))
            {
                numbers.Add(winning[i]);
            }
            winNums.Add(Int32.Parse(new string(numbers.ToArray())));          
        }
    }
    for (int i = 0; i < chosen.Length; i++)
    {
        List<char> numbers = new List<char>();
        if (char.IsNumber(chosen[i]))
        {
            numbers.Add(chosen[i]);
            if (i != chosen.Length - 1)
            {
                if (char.IsNumber(chosen[++i]))
                {
                    numbers.Add(chosen[i]);
                }
            }
            choNums.Add(Int32.Parse(new string(numbers.ToArray())));
        }
    }
    numCards[card]++;
    for (int i = 0; i < numCards[card]; i++)
    {
        int matches = 0;
        foreach (int j in choNums)
        {
            if (winNums.Contains(j))
            {
                matches++;
            }
        }
        for (int j = 1; j <= matches; j++)
        {
            numCards[card + j]++;
        }
    }
    card++;
}
foreach (int i in numCards)
{
    total += i;
}

Console.WriteLine(total);