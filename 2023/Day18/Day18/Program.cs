using System.Drawing;
using System.Linq;
using System.Reflection;

string[] input = File.ReadAllLines("text.txt");

(long, long) curLocation = (0, 0);
List<(long, long)> locations = [curLocation];
List< (long, long)> shape = [];
shape.Add((0, 0));
long perimeter = 0;
foreach (string line in input)
{
    char dir = line[^2];
    switch (dir) {
        case '0':
            dir = 'R';
            break;
        case '1':
            dir = 'D';
            break;
        case '2':
            dir = 'L';
            break;
        case '3':
            dir = 'U';
            break;
    }
    Console.WriteLine(dir);
    int index = line.IndexOf('#');
    string dist = line.Substring(index + 1, 5);
    int distance = Convert.ToInt32("0x" + dist, 16);
    Console.WriteLine(distance);
    for (int i = 0; i < distance; i++)
    {
        if (dir == 'U')
        {
            curLocation.Item2++;
        }
        else if (dir == 'D')
        {
            curLocation.Item2--;
        }
        else if (dir == 'R')
        {
            curLocation.Item1++;
        }
        else if (dir == 'L')
        {
            curLocation.Item1--;
        }
        locations.Add(curLocation);
        perimeter++;
    }
    shape.Add((curLocation.Item1, curLocation.Item2));
    Console.WriteLine(curLocation);
}

long area = 0;
int j = shape.Count - 1;
for (int i = 0; i < shape.Count; i++)
{
    area += (shape[j].Item1 * shape[i].Item2) - (shape[i].Item1 * shape[j].Item2);
    j = i;
}
area = Math.Abs(area) / 2;

long interior = area - (perimeter / 2) + 1;
Console.WriteLine(interior + perimeter);

/*double area = 0;
int j = shape.Count - 1;
for (int i = 0; i < shape.Count; i++)
{
    area += (shape[j].Item1 + shape[i].Item1) * (shape[j].Item2 - shape[i].Item2);
    j = i;
}
area = Math.Abs(area / 2);
//area += locations.Count;
Console.WriteLine(area);*/
/*
/*double total = 0;
for (int i = minY; i < maxY + 1; i++)
{
    for (int j = minX; j < maxX + 1; j++)
    {
        if (locations.Contains((i, j)))
        {
            total++;
            continue;
        }
        if (IsInPolygon(points, new Point(j,i)))
        {
            int count = 0;
            while (!locations.Contains((i, j)))
            {
                count++;
                j++;
            }
            total += count;
        }
    }
}
Console.WriteLine(total);*/

/*bool IsInPolygon(Point[] poly, Point p)
{
    Point p1, p2;
    bool inside = false;

    if (poly.Length < 3)
    {
        return inside;
    }

    var oldPoint = new Point(
        poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

    for (int i = 0; i < poly.Length; i++)
    {
        var newPoint = new Point(poly[i].X, poly[i].Y);

        if (newPoint.X > oldPoint.X)
        {
            p1 = oldPoint;
            p2 = newPoint;
        }
        else
        {
            p1 = newPoint;
            p2 = oldPoint;
        }

        if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
            && (p.Y - (long)p1.Y) * (p2.X - p1.X)
            < (p2.Y - (long)p1.Y) * (p.X - p1.X))
        {
            inside = !inside;
        }

        oldPoint = newPoint;
    }

    return inside;
}*/



/*foreach (string line in input)
{
    char dir = line[0];
    string dist = line.Substring(2, 1);
    if (char.IsDigit(line[3]))
    {
        dist += line[3];
    }
    int distance = int.Parse(dist);
    for (int i = 0; i < distance; i++)
    {
        if (dir == 'U')
        {
            curLocation.Item1--;
        }
        else if (dir == 'D')
        {
            curLocation.Item1++;
        }
        else if (dir == 'R')
        {
            curLocation.Item2++;
        }
        else if (dir == 'L')
        {
            curLocation.Item2--;
        }
        shape.Add(curLocation);
        Console.WriteLine(curLocation);
    }
}
int widthMin = shape.OrderByDescending(x => x.Item1).Last().Item1;
int widthMax = shape.OrderByDescending(x => x.Item1).First().Item1;
int lengthMin = shape.OrderByDescending(x => x.Item2).Last().Item2;
int lengthMax = shape.OrderByDescending(x => x.Item2).First().Item2;
for (int i = widthMin; i < widthMax + 1; i++)
{
    for (int j = lengthMin; j < lengthMax + 1; j++)
    {
        if (shape.Contains((i, j)))
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}*/