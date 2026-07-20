using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");

List<List<List<int>>> sequences = [];

foreach (string line in text)
{
    List<List<int>> sequence = [];
    sequence.Add([]);
    foreach (string number in line.Split(' '))
    {
        sequence[0].Add(int.Parse(number));
    }
    sequences.Add(sequence);
}

int total = 0;
foreach (List<List<int>> sequence in sequences)
{
    sequence[0].Reverse();
    bool zeroed = false;
    while (!zeroed)
    {
        List<int> curList = sequence.Last();
        List<int> newList = [];
        for (int i = 0; i < curList.Count - 1; i++)
        {
            newList.Add(curList[i + 1] - curList[i]);
        }
        sequence.Add(newList);
        zeroed = true;
        foreach (int number in newList)
        {
            if (number != 0)
            {
                zeroed = false;
            }
        }
    }

    for(int i = 1; i < sequence.Count; i++)
    {
        sequence[i].Add(sequence[i].Last() + sequence[i - 1].Last());
    }
    total += sequence.Last().Last();
}

Console.WriteLine(total);


/*int total = 0;
foreach (List<List<int>> sequence in sequences)
{
    bool zeroed = false;
    while (!zeroed)
    {
        List<int> curList = sequence.Last();
        List<int> newList = [];
        for (int i = 0; i < curList.Count - 1; i++)
        {
            newList.Add(curList[i + 1] - curList[i]);
        }
        sequence.Add(newList);
        zeroed = true;
        foreach (int number in newList)
        {
            if (number != 0)
            {
                zeroed = false;
            }
        }
    }

    sequence.Reverse();
    for (int i = 1; i < sequence.Count; i++)
    {
        sequence[i].Add(sequence[i].Last() + sequence[i - 1].Last());
    }
    total += sequence.Last().Last();
}

Console.WriteLine(total);*/