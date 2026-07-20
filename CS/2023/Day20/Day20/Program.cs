string[] input = [.. File.ReadAllLines("text.txt")];
List<List<string>> inputs = [];
foreach (string line in input)
{
    inputs.Add([.. line.Split(" ")]);
}
int bIndex = inputs.FindIndex(x => x[0] == "broadcaster");
List<string> bOut = inputs[bIndex].GetRange(2, inputs[bIndex].Count - 2);
inputs.RemoveAt(bIndex);
for (int i = 0; i < bOut.Count - 1; i++)
{
    bOut[i] = bOut[i].Remove(bOut[i].Length - 1);
}

List<(string, bool, List<string>)> flipflops = [];
List<(string, List<(string, bool)>, List<string>)>  conjunctions = [];

foreach (List<string> lst in inputs)
{
    if (lst[0][0] == '%')
    {
        flipflops.Add((lst[0].Substring(1, 2), false, lst.GetRange(2, lst.Count - 2)));
    } else if (lst[0][0] == '&')
    {
        conjunctions.Add((lst[0].Substring(1, 2), [], lst.GetRange(2, lst.Count - 2)));
    } else
    {
        throw new Exception("bad line start: " + lst[0][0]);
    }
}
foreach (List<string> lst in inputs)
{
    string curModule = lst[0].Substring(1, 2);
    List<string> outs = lst.GetRange(2, lst.Count - 2);
    foreach (string s in outs)
    {
        if (conjunctions.FindIndex(x => x.Item1 == s) != -1)
        {
            List<(string, bool)> conLst = conjunctions[conjunctions.FindIndex(x => x.Item1 == s)].Item2;
            if (conLst.FindIndex(x => x.Item1 == curModule) == -1)
            {
                conLst.Add((curModule, false));
            }
        }
    }
}
foreach ((string, bool, List<string>) f in flipflops)
{
    Console.Write(f.Item1 + " " + f.Item2 + " ");
    foreach (string s in f.Item3)
    {
        Console.Write(s + " ");
    }
    Console.WriteLine();
}
foreach ((string, List<(string, bool)>, List<string>) c in conjunctions)
{
    Console.Write(c.Item1 + " ");
    foreach ((string, bool) s in c.Item2)
    {
        Console.Write(s.Item1 + " " + s.Item2 + " ");
    }
    foreach (string s in c.Item3)
    {
        Console.Write(s + " ");
    }
    Console.WriteLine();
}