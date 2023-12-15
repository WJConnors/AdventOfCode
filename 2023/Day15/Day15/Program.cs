using System.Reflection;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
String text = File.ReadAllText("test.txt");

string[] strings = text.Split(",");