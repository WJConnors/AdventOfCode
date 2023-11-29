using System.Reflection;


string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String text = File.ReadAllText(rootDirectory + "/text.txt");

List <(int, int)> coordinates = new List<(int, int)>();
(int, int) santaCoordinate = (0, 0);
(int, int) roboCoordinate = (0, 0);
coordinates.Add(santaCoordinate);
Boolean isSanta = true;

foreach (char c in text)
{
    (int, int) currentCoordinate = isSanta ? santaCoordinate : roboCoordinate;

    if (c == '^')
    {
        currentCoordinate.Item2++;
    }
    else if (c == 'v')
    {
        currentCoordinate.Item2--;
    }
    else if (c == '>')
    {
        currentCoordinate.Item1++;
    }
    else if (c == '<')
    {
        currentCoordinate.Item1--;
    }

    if (!coordinates.Contains(currentCoordinate))
    {
        coordinates.Add(currentCoordinate);
    }

    if (isSanta)
    {
        santaCoordinate = currentCoordinate;
    }
    else
    {
        roboCoordinate = currentCoordinate;
    }

    isSanta = !isSanta;

}

Console.WriteLine(coordinates.Count);