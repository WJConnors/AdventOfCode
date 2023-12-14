using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

for  (int i = 0; i < text[0].Length; i++)
{
    for (int j = 1; j < text.Length; j++)
    {
        if (j < 1) { j = 1;}
        if (text[j][i] == 'O' && text[j - 1][i] == '.')
        {
            char[] arr1 = text[j].ToCharArray();
            char[] arr2 = text[j - 1].ToCharArray();

            arr1[i] = '.';
            arr2[i] = 'O';

            text[j] = new string(arr1);
            text[j - 1] = new string(arr2);
            j -= 2;
        }
    }
}

int total = 0;
for (int i = 0; i < text.Length; i++)
{
    foreach (char c in text[i])
    {
        if (c == 'O')
        {
            total += text.Length - i;
        }
    }
}

Console.WriteLine(total);