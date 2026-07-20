using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string[] text = File.ReadAllLines(rootDirectory + "/text.txt");
//text = ["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"];

char game = '1';
int total = 0;

foreach (string line in text)
{
    int minRed = 0;
    int minGreen = 0;
    int minBlue = 0;
    int index = line.IndexOf(game) + 1;
    string[] reveals = line.Split(';');
    foreach (string reveal in reveals)
    {
        int rRed = 0;
        int rGreen = 0;
        int rBlue = 0;
        for (int i = 0; i < reveal.Length; i++)
        {
            char c = reveal[i];
            if (Char.IsNumber(c))
            {
                List<char> numbers = new List<char>() {c};
                while (Char.IsNumber(reveal[++i]))
                {
                    numbers.Add(reveal[i]);                  
                }
                int number = Int32.Parse(new string(numbers.ToArray()));
                char col = reveal[++i];
                if (col == 'r')
                {
                    rRed += number;
                } else if (col == 'g')
                {
                    rGreen += number;
                } else if (col == 'b')
                {
                    rBlue += number;
                }

            }
        }
        minRed = Math.Max(minRed, rRed);
        minGreen = Math.Max(minGreen, rGreen);
        minBlue = Math.Max(minBlue, rBlue);
    }
    total += minRed * minGreen * minBlue;
}

Console.WriteLine(total);




/*int red = 12;
int green = 13;
int blue = 14;
char game = '1';
int total = 0;

foreach (string line in text)
{
    Boolean possible = true;
    int index = line.IndexOf(game) + 1;
    string[] reveals = line.Split(';');
    foreach (string reveal in reveals)
    {
        for (int i = 0; i < reveal.Length; i++)
        {
            char c = reveal[i];
            if (Char.IsNumber(c))
            {
                List<char> numbers = new List<char>() { c };
                while (Char.IsNumber(reveal[++i]))
                {
                    numbers.Add(reveal[i]);
                }
                int number = Int32.Parse(new string(numbers.ToArray()));
                char col = reveal[++i];
                if (col == 'r')
                {
                    if (number > red)
                    {
                        possible = false;
                        break;
                    }
                }
                else if (col == 'g')
                {
                    if (number > green)
                    {
                        possible = false;
                        break;
                    }
                }
                else if (col == 'b')
                {
                    if (number > blue)
                    {
                        possible = false;
                        break;
                    }
                }

            }
            if (!possible)
            {
                break;
            }
        }
        if (!possible)
        {
            break;
        }
    }
    if (possible)
    {
        total += game - '0';
    }
    game++;
}

Console.WriteLine(total);*/