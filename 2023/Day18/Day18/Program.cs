string[] input = File.ReadAllLines("test.txt");

(int, int) curLocation = (0, 0);

foreach (string line in input)
{
    char dir = line[0];
    string dist = line.Substring(2, 1);
    if (char.IsDigit(line[3]))
    {
        dist += line[3];
    }
}