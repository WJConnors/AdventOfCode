using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

string time = text[0];
string distance = text[1];
List<int> times = new List<int>();
List<int> distances = new List<int>();

for (int i = 0; i < time.Length; i++)
{
    List<char> tempTime = new List<char>();
    if (char.IsNumber(time[i]))
    {
        tempTime.Add(time[i]);
        if (char.IsNumber(time[++i]))
        {
            tempTime.Add(time[i]);
        }
        times.Add(int.Parse(new string(tempTime.ToArray())));
    }

    List<char> tempDistance = new List<char>();
    if (char.IsNumber(distance[i]))
    {
        tempDistance.Add(distance[i]);
        if (char.IsNumber(distance[++i]))
        {
            tempTime.Add(distance[i]);
        }
        distances.Add(int.Parse(new string(tempDistance.ToArray())));
    }
}