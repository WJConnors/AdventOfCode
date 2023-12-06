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
    while (char.IsNumber(time[i]))
    {
        tempTime.Add(time[i]);
        if (++i == time.Length) { break; }
    }
    if (tempTime.Count > 0) { 
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

