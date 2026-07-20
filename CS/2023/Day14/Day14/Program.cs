using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String[] text = File.ReadAllLines(rootDirectory + "/text.txt");

HashSet<string> states = new HashSet<string>();

for (int i = 0; i < 1000000000; i++)
{
    tiltNorth();
    tiltWest();
    tiltSouth();
    tiltEast();

    string currentState = string.Join("|", text); // Serialize 'text' with '|' as delimiter
    if (!states.Add(currentState)) // 'Add' returns false if the item already exists
    {
        Console.WriteLine("Found a duplicate state at iteration " + i);
        int loopLength = i - states.ToList().IndexOf(currentState);
        Console.WriteLine("Loop length: " + loopLength);
        while (i < 1000000000 - loopLength)
        {
            i += loopLength;
        }
    }
}

foreach (string s in text)
{
    Console.WriteLine(s);
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

void tiltNorth ()
{
    for (int i = 0; i < text[0].Length; i++)
    {
        for (int j = 1; j < text.Length; j++)
        {
            if (j < 1) { j = 1; }
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
}

void tiltEast()
{
    for (int i = 0; i < text.Length; i++) // Iterate through each row
    {
        bool moved;
        do
        {
            moved = false;
            for (int j = text[i].Length - 2; j >= 0; j--) // Iterate from right to left within each row
            {
                // Check if current character is 'O' and the next character to the right is '.'
                if (text[i][j] == 'O' && text[i][j + 1] == '.')
                {
                    char[] row = text[i].ToCharArray();

                    // Swap 'O' with '.'
                    row[j] = '.';
                    row[j + 1] = 'O';

                    text[i] = new string(row);

                    // Indicate that a move was made
                    moved = true;

                    // Skip the next position as we just moved 'O' to it
                    j--;
                }
            }
        } while (moved); // Repeat if any 'O' was moved
    }
}

void tiltSouth()
{
    for (int i = 0; i < text[0].Length; i++) // Iterate through each column
    {
        bool moved;
        do
        {
            moved = false;
            for (int j = text.Length - 2; j >= 0; j--) // Iterate upwards within each column
            {
                if (text[j][i] == 'O' && text[j + 1][i] == '.') // Check for 'O' above '.'
                {
                    char[] upperRow = text[j].ToCharArray();
                    char[] lowerRow = text[j + 1].ToCharArray();

                    upperRow[i] = '.'; // Replace 'O' with '.'
                    lowerRow[i] = 'O'; // Move 'O' downward

                    text[j] = new string(upperRow);
                    text[j + 1] = new string(lowerRow);

                    moved = true;
                    j--; // Skip the next position as we just moved 'O' to it
                }
            }
        } while (moved); // Repeat if any 'O' was moved
    }
}

void tiltWest()
{
    for (int i = 0; i < text.Length; i++) // Iterate through each row
    {
        bool moved;
        do
        {
            moved = false;
            for (int j = 1; j < text[i].Length; j++) // Iterate from left to right within each row
            {
                if (text[i][j] == 'O' && text[i][j - 1] == '.') // Check for 'O' to the right of '.'
                {
                    char[] row = text[i].ToCharArray();

                    row[j] = '.'; // Replace 'O' with '.'
                    row[j - 1] = 'O'; // Move 'O' leftward

                    text[i] = new string(row);

                    moved = true;
                    j++; // Skip the next position as we just moved 'O' to it
                }
            }
        } while (moved); // Repeat if any 'O' was moved
    }
}