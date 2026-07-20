using System.Collections.Generic;

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
    List<string> strLst = [.. line.Split('{')];
    workflowRules.Add([strLst[0]]);
    strLst.RemoveAt(0);
    strLst = [.. strLst[0].Split(",")];
    foreach (string str in strLst)
    {
        workflowRules.Last().Add(str);
    }
    workflowRules.Last()[^1] = workflowRules.Last().Last().Remove(workflowRules.Last().Last().Length - 1, 1);
}

foreach(List<string> rule in workflowRules)
{
    foreach (string str in rule)
    {
        Console.Write(str + " ");
    }
    Console.WriteLine();
}

List<List<(int, int)>> firstRanges = [[(1, 4000), (1, 4000), (1, 4000), (1, 4000),]];
int index = workflowRules.FindIndex(x => x[0] == "in");

List<List<(int, int)>> ranges = GetRanges(firstRanges[0], index);
Console.WriteLine(ranges.Count);
foreach (List<(int, int)> range in ranges)
{
    foreach ((int, int) val in range)
    {
        Console.Write(val + " ");
    }
    Console.WriteLine();
}



List<List<(int,int)>> GetRanges (List<(int, int)> ranges, int index)
{
    Console.WriteLine(index);
    foreach ((int, int) val in ranges)
    {
        Console.Write(val + " ");
    }
    Console.WriteLine();
    List<List<(int, int)>> output = [];
    List<(int, int)> range = ranges;
    List<string>  curRules = workflowRules[index];
    for (int i = 1; i < curRules.Count; i++)
    {
        Console.WriteLine(curRules[i]);
        string newFlow = "";
        bool found = false;
        if (i == curRules.Count - 1)
        {
            found = true;
            newFlow = curRules[i];
            if (newFlow == "A")
            {
                Console.WriteLine("found");
                foreach ((int, int) v in range)
                {
                    Console.Write(v + " ");
                }
                Console.WriteLine();
                output.Add(range);
                continue;
            } else if (newFlow == "R")
            {
                continue;
            }
            List<List<(int, int)>> curOutput = GetRanges(range, workflowRules.FindIndex(x => x[0] == newFlow));
            foreach (List<(int, int)> r in curOutput)
            {
                output.Add(r);
            }
            continue;
        }

        int cat = CharToCat(curRules[i][0]);
        char op = curRules[i][1];
        string strVal = "";
        for (int j = 2; j < curRules[i].Length; j++)
        {
            if (char.IsDigit(curRules[i][j]))
            {
                strVal += curRules[i][j];
            }
        }
        int val = int.Parse(strVal);
        (int, int) catVals = ranges[cat];
        if (val >= catVals.Item1 && val <= catVals.Item2)
        {
            (int, int) lower;
            (int, int) upper;

            switch (op)
            {
                case '<':
                    newFlow = curRules[i].Split(':')[1];
                    lower = (catVals.Item1, int.Min(catVals.Item2, val -1));
                    upper = (val, catVals.Item2);
                    List<(int, int)> newRange1 = new(range);
                    range[cat] = upper;
                    newRange1[cat] = lower;
                    if (newFlow == "A")
                    {
                        Console.WriteLine("found");
                        foreach ((int, int) v in range)
                        {
                            Console.Write(v + " ");
                        }
                        Console.WriteLine();
                        output.Add(newRange1);
                    }
                    else if (newFlow == "R")
                    {
                        continue;
                    }
                    else
                    {
                        List<List<(int, int)>> curOutput = GetRanges(range, workflowRules.FindIndex(x => x[0] == newFlow));
                        foreach (List<(int, int)> r in curOutput)
                        {
                            output.Add(r);
                        }
                    }
                    break;
                case '>':
                    newFlow = curRules[i].Split(':')[1];
                    lower = (catVals.Item1, val);
                    upper = (val + 1, catVals.Item2);
                    List<(int, int)> newRange2 = new(range);
                    range[cat] = lower;
                    newRange2[cat] = upper;
                    if (newFlow == "A")
                    {
                        Console.WriteLine("found");
                        foreach ((int, int) v in range)
                        {
                            Console.Write(v + " ");
                        }
                        Console.WriteLine();
                        output.Add(newRange2);
                    }
                    else if (newFlow == "R")
                    {
                        continue;
                    }
                    else
                    {
                        List<List<(int, int)>> curOutput = GetRanges(range, workflowRules.FindIndex(x => x[0] == newFlow));
                        foreach (List<(int, int)> r in curOutput)
                        {
                            output.Add(r);
                        }
                    }
                    break;
                default:
                    throw new Exception("incorrect op: " + op);
            }
            continue;
        }
        else
        {
            found = op switch
            {
                '<' => catVals.Item1 < val,
                '>' => catVals.Item1 > val,
                _ => throw new Exception("incorrect op: " + op),
            };
            if (found)
            {
                newFlow = curRules[i].Split(':')[1];
                if (newFlow == "A")
                {
                    Console.WriteLine("found");
                    foreach ((int, int) v in range)
                    {
                        Console.Write(v + " ");
                    }
                    Console.WriteLine();
                    output.Add(range);
                }
                else if (newFlow == "R")
                {
                    continue;
                }
                else
                {
                    List<List<(int, int)>> curOutput = GetRanges(range, workflowRules.FindIndex(x => x[0] == newFlow));
                    foreach (List<(int, int)> r in curOutput)
                    {
                        output.Add(r);
                    }
                }
            }
            else
            {
                continue;
            }
        }
    }

    return output;
}

static int CharToCat(char c)
{
    var cat = c switch
    {
        'x' => 0,
        'm' => 1,
        'a' => 2,
        's' => 3,
        _ => throw new Exception("incorrect chartocat: " + c),
    };
    return cat;
}

/*List<List<int>> rateVals = [];
foreach (string line in ratings)
{
    List<string> strList = [.. line.Split(",")];
    List<int> intList = [];
    foreach (string str in strList)
    {
        string curVal = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsDigit(str[i]))
            {
                curVal += str[i];
            }
        }
        intList.Add(int.Parse(curVal));
    }
    rateVals.Add(intList);
}

double total = 0;
foreach (List<int> part in rateVals)
{
    int index = workflowRules.FindIndex(x => x[0] == "in");
    string newFlow = "in";
    List<string> curRules = workflowRules[index];
    while (true)
    {
        for (int i = 1; i < curRules.Count; i++)
        {
            bool found = false;
            if (i == curRules.Count - 1)
            {
                found = true;
                newFlow = curRules[i];
                break;
            }

            int cat = CharToCat(curRules[i][0]);
            char op = curRules[i][1];
            string strVal = "";
            for (int j = 2; j < curRules[i].Length; j++)
            {
                if (char.IsDigit(curRules[i][j]))
                {
                    strVal += curRules[i][j];
                }
            }
            int val = int.Parse(strVal);
            found = op switch
            {
                '<' => part[cat] < val,
                '>' => part[cat] > val,
                _ => throw new Exception("incorrect op: " + op),
            };
            if (found)
            {
                newFlow = curRules[i].Split(':')[1];
                break;
            }
        }
        if (newFlow == "A")
        {
            total += part.Sum();
            break;
        }
        else if (newFlow == "R")
        {
            break;
        }
        else
        {
            index = workflowRules.FindIndex(x => x[0] == newFlow);
            curRules = workflowRules[index];
        }
    }
}

Console.WriteLine(total);*/