using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

int total = 0;

foreach (string line in text)
{
    string winning = line.Substring(line.IndexOf(':', line.IndexOf('|')));
    string chosen = line.Substring(line.IndexOf('|') + 1);
}