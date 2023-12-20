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

List<List<(string, bool, List<string>)>> flipflops = [];
List<List<(string, List<(string, bool)>, List<string>)>>  conjunctions = [];

foreach (List<string> lst in inputs)
{
    if (lst[0][0] == '%')
    {

    } else if (lst[0][0] == '&')
    {

    } else
    {
        throw new Exception("bad line start: " + lst[0][0]);
    }
}