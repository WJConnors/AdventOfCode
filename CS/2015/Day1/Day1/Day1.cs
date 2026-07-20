using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String text = File.ReadAllText(rootDirectory + "/text.txt");
int i = 0;
int j = 0;
foreach (char c in text)
{
    /*if (c.Equals('('))
    {
        i++;
    }
    else if (c.Equals(')'))
    {
        i--;
    }*/
    if (c.Equals('('))
    {
        i++;
    }
    else if (c.Equals(')'))
    {
        i--;
    }
    j++;
    if (i == -1)
    {
        break;
    }
}
Console.WriteLine(j);