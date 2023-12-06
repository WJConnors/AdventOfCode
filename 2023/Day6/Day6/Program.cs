using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

string time = text[0];
string distance = text[1];
double raceTime;
double raceDistance;
List<char> tempTime = new List<char>();
List<char> tempDistance = new List<char>();
for (int i = 0; i < time.Length; i++)
{
    if (Char.IsNumber(time[i]))
    {
        tempTime.Add(time[i]);
    }
}
raceTime = int.Parse(new string(tempTime.ToArray()));
for (int i = 0; i < distance.Length; i++)
{
    if (Char.IsNumber(distance[i]))
    {
        tempDistance.Add(distance[i]);
    }
}
raceDistance = double.Parse(new string(tempDistance.ToArray()));


int count = 0;
for (int j = 0; j < raceTime; j++)
{
    if ((j * (raceTime - j)) > raceDistance)
    {
        count++;
    }
}

Console.WriteLine(count);


/*List<int> times = new List<int>();
List<int> distances = new List<int>();

for (int i = 0; i < time.Length; i++)
{
    List<char> tempTime = new List<char>();
    while (char.IsNumber(time[i]))
    {
        tempTime.Add(time[i]);
        if (++i == time.Length) { break; }
    }
    if (tempTime.Count > 0)
    {
        times.Add(int.Parse(new string(tempTime.ToArray())));
    }

}
for (int i = 0; i < time.Length; i++)
{
    List<char> tempDistance = new List<char>();
    while (char.IsNumber(distance[i]))
    {
        tempDistance.Add(distance[i]);
        if (++i == distance.Length) { break; }
    }
    if (tempDistance.Count > 0)
    {
        distances.Add(int.Parse(new string(tempDistance.ToArray())));
    }
}

int total = 1;
for (int i = 0; i < times.Count; i++)
{
    int count = 0;
    for (int j = 0; j < times[i]; j++)
    {
        if ((j * (times[i] - j)) > distances[i])
        {
            count++;
        }
    }
    total *= count;
}

Console.WriteLine(total);*/