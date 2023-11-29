using System;
using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

int nice = 0;
char[] vowelArray = ['a', 'e', 'i', 'o', 'u'];
string[] badArray = ["ab", "cd", "pq", "xy"];

foreach (string line in text)
{
    int vowels = 0;
    Boolean doubleLetter = false;
    Boolean bad = false;
    char previous = '1';

    foreach (char c in line)
    {
        if (vowelArray.Contains(c)) { vowels++; }
        if (c == previous)
        {
            doubleLetter = true;
        }
        char[] chars = [previous, c];
        if (badArray.Contains(new string(chars)))
        {
            bad = true;
            break;
        }
        previous = c;
    }

    if (vowels >= 3 && doubleLetter && !bad) { nice++; }

}

Console.WriteLine(nice);