string[] input = File.ReadAllLines("test.txt");

List<string> workflows = [];
List<string> ratings = [];
bool empty = false;
foreach (string line in input)
{
    if (line == "")
    {
        empty = true;
        continue;
    }
    if (!empty) { workflows.Add(line); }
    else if (empty) { ratings.Add(line);}
}

List<List<string>> workflowRules = [];
foreach (string line in workflows)
{
    List<string> strLst = line.Split('{').ToList();
    workflowRules.Add([strLst[0]]);
    strLst.RemoveAt(0);
    strLst = [.. strLst[0].Split(",")];
    foreach (string str in strLst)
    {
        workflowRules.Last().Add(str);
    }
    workflowRules.Last()[workflowRules.Last().Count - 1] = workflowRules.Last().Last().Remove(workflowRules.Last().Last().Length - 1, 1);
}

foreach (List<string> str in workflowRules)
{
    foreach (string s in str)
    {
        Console.Write(s + " ");
    }
    Console.WriteLine();
}