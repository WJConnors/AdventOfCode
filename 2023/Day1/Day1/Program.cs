using System.ComponentModel;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");
//text = ["two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234", "7pqrstsixteen"];
text = ["twofivefourb5four"];

int total = 0;
string[] digits = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

int first;
int firstpos;
int last;
int lastpos;

foreach (string line in text)
{

    first = -1;
    firstpos = -1;
    last = -1;
    lastpos = -1;

    int cur = 0;

    foreach (char c in line)
    {
        if (Char.IsNumber(c))
        {
            if (first == -1)
            {
                first = c - '0';
                firstpos = cur;
            }
            last = c - '0';
            lastpos = cur;
        }
        cur++;
    }

    int curDigit = 1;
    foreach (string digit in digits)
    {
        StringDigit(curDigit, line, digit, 0);
        curDigit++;
    }

    string num = first.ToString() + last.ToString();
    Console.WriteLine(line);
    Console.WriteLine(num);
    total += Int32.Parse(num);
}

Console.WriteLine(total);

void StringDigit(int curDigit, string line, string digit, int firstIndex)
{
    int index = line.IndexOf(digit);
    if (index != -1)
    {
        if (index + firstIndex < firstpos || firstpos == -1)
        {
            first = curDigit;
            firstpos = index + firstIndex;
        }
        Console.WriteLine(index + firstIndex);
        Console.WriteLine(lastpos);
        if (index + firstIndex > lastpos || lastpos == -1)
        {
            last = curDigit;
            lastpos = index + firstIndex;
        }
        string sub = line.Substring(index + digit.Length);
        Console.WriteLine(sub);
        StringDigit(curDigit, sub, digit, firstIndex + index);

    }
}