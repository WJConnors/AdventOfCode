using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

int totalPaper = 0;
int totalRibbon = 0;
int[] dimensions = new int[3];

foreach (string line in text)
{
    int cur = 0;
    string curStr = "";
    for (int i = 0; i < line.Length; i++)
    {
        char c = line[i];
        if ( c != 'x')
        {
            curStr += c;
        }
        if (c == 'x' || i == line.Length - 1)
        {
            dimensions[cur] = int.Parse(curStr);
            cur++;
            curStr = "";
        }
    }

    int[] areas = [dimensions[0] * dimensions[1], dimensions[1] * dimensions[2], dimensions[2] * dimensions[0]];
    int smallest = areas.Min();
    totalPaper += (2 * areas[0]) + (2 * areas[1]) + (2 * areas[2]) + smallest;

    Array.Sort(dimensions);
    int perimeter = (2 * dimensions[0]) + (2 * dimensions[1]);
    int volume = dimensions[0] * dimensions[1] * dimensions[2];
    totalRibbon += perimeter + volume;
}

Console.WriteLine(totalPaper);
Console.WriteLine(totalRibbon);