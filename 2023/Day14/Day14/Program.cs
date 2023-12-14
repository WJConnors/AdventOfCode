using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

for  (int i = 0; i < text[0].Length; i++)
{
    for (int j = 1; j < text.Length; j++)
    {
        char temp1 = text[j][i];
        char temp2 = text[i][j - 1];

        if (temp1 == 'O' && temp2 == '.')
        {

        }
    }
}