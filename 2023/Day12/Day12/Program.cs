using System.Diagnostics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AdventOfCode2023;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{
    public class Day12
    {
        public void Part(int part)
        {
            var timer = new Stopwatch();
            timer.Start();

            //var outFile = "C:\\src\\Day12.txt";
            //File.WriteAllLines(outFile, new[] { "Day 12 Part 2"});

            string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var input = File.ReadLines(rootDirectory + "/text.txt").ToList();

            var tasks = input.Select(line => Task.Factory.StartNew(() =>
            {
                var pattern = line.Split(" ")[0];
                var known = line.Split(" ")[1].Split(",").Select(int.Parse).ToList();

                if (part == 2)
                {
                    var repeats = 5;
                    pattern = string.Join("?", Enumerable.Repeat(pattern, repeats));
                    known = Enumerable.Repeat(known, repeats).SelectMany(i => i).ToList();
                }

                var timer = new Stopwatch();
                timer.Start();
                var arrangements = Arrangements(pattern, known);

                return (arrangements, timer.ElapsedMilliseconds);
            })).ToList();

            long result = 0;
            while (tasks.Count > 0)
            {
                var waitAny = Task.WaitAny(tasks.ToArray<Task>(), TimeSpan.FromSeconds(10));
                var printRemaining = true;
                if (waitAny >= 0)
                {
                    var (arrangements, elapsedMilliseconds) = tasks[waitAny].Result;
                    result += arrangements;
                    tasks.RemoveAt(waitAny);
                    input.RemoveAt(waitAny);

                    //File.AppendAllLines(outFile, new[] { $"Found {arrangements} arrangements in {elapsedMilliseconds}ms - interim result is {result} with {input.Count} tasks remaining" });
                    printRemaining = true;
                }
                else if (printRemaining)
                {
                    //File.AppendAllLines(outFile, new[] { "Remaining tasks:" }.Concat(input));
                    printRemaining = false;
                }
            }

            Console.WriteLine($"{result} in {timer.ElapsedMilliseconds}ms");
        }

        private long Arrangements(string pattern, IReadOnlyList<int> sections) =>
            Arrangements(pattern, sections, 0, pattern.Length);

        private long Arrangements(string pattern, IReadOnlyList<int> sections, int patternOffset, int patternLength) =>
            sections.Count switch
            {
                0 => NoSections(pattern, patternOffset, patternLength),
                1 => OneSection(pattern, sections[0], patternOffset, patternLength),
                _ => MultipleSections(pattern, sections, patternOffset, patternLength)
            };

        private long MultipleSections(string pattern, IReadOnlyList<int> sections, int patternOffset, int patternLength)
        {
            // If there's more than one section, split the sections into three smaller sub-problems
            var leftSections = sections.Take(sections.Count / 2).ToList();
            var pivotSection = sections[sections.Count / 2];
            var rightSections = sections.Skip(sections.Count / 2 + 1).ToList();

            // For each possible position of the pivot section...
            var beforePivotMin = leftSections.Sum() + leftSections.Count - 1;
            var beforePivot = beforePivotMin;
            var slack = patternLength - sections.Sum() - sections.Count + 2;
            long result = 0;
            for (var i = 0; i < slack; i++, beforePivot++)
            {
                // Check points before and after the pivot section are empty - if not then we're done
                var afterPivot = beforePivot + 1 + pivotSection;
                if (pattern[patternOffset + beforePivot] == '#' || (afterPivot < patternLength && pattern[patternOffset + afterPivot] == '#'))
                {
                    continue;
                }

                // Check the pivot section is legally placed - if not then we're done
                var middle = OneSection(pattern, pivotSection, patternOffset + beforePivot + 1, pivotSection);
                if (middle == 0)
                {
                    continue;
                }

                // Check how many ways the left section(s) can be placed in the space to the left of the pivot - if none then we're done
                var left = Arrangements(pattern, leftSections, patternOffset, beforePivot);
                if (left == 0)
                {
                    continue;
                }

                // Check how many ways the right section(s) can be placed in the space to the right of the pivot
                var right = Arrangements(pattern, rightSections, patternOffset + afterPivot + 1, patternLength - afterPivot - 1);

                // Add to the result the combined permutations of left and right
                result += left * right;
            }

            return result;
        }

        private long OneSection(string pattern, int section, int patternOffset, int patternLength)
        {
            // If there's only one section, then try it in each of its possible positions
            var slack = patternLength - section - 1 + 2;
            long result = 0;
            for (var i = 0; i < slack; i++)
            {
                var x = 0;
                var isPossible = true;
                for (; isPossible && x < i; x++)
                {
                    if (pattern[patternOffset + x] == '#')
                    {
                        isPossible = false;
                    }
                }

                for (; isPossible && x < i + section; x++)
                {
                    if (pattern[patternOffset + x] == '.')
                    {
                        isPossible = false;
                    }
                }

                for (; isPossible && x < patternLength; x++)
                {
                    if (pattern[patternOffset + x] == '#')
                    {
                        isPossible = false;
                    }
                }

                if (isPossible)
                {
                    // If the position is legal then it counts as one possible arrangement
                    result++;
                }
            }

            return result;
        }

        private static long NoSections(string pattern, int patternOffset, int patternLength)
        {
            // If there are no sections, check the space can be entirely empty
            for (var x = 0; x < patternLength; x++)
            {
                if (pattern[patternOffset + x] == '#')
                {
                    return 0;
                }
            }

            return 1;
        }
    }
}

class Program
{
    static void Main()
    {
        var day12 = new Day12();
        day12.Part(1);
        day12.Part(2);
    }
}

/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

class Program
{
    static void Main()
    {
        string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/test.txt"));

        List<double> totals = new List<double>();

        foreach (string line in text)
        {
            string[] splitLine = line.Split(" ");
            string gears = splitLine[0];
            List<int> groups = splitLine[1].Split(",").Select(int.Parse).ToList();

            List<List<int>> permutations = Combinations(gears, groups);

            double total = 0;
            List<string> found = new List<string>();

            foreach (List<int> group in permutations)
            {
                StringBuilder record = new StringBuilder(gears);
                foreach (int j in group)
                {
                    record[j] = '#';
                }
                List<int> list = Arrangement(record.ToString());
                bool same = true;
                if (list.Count == groups.Count)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j] != groups[j])
                        {
                            same = false;
                            break;
                        }
                    }
                }
                else { same = false; }
                if (same && !found.Contains(record.ToString()))
                {
                    total++;
                    found.Add(record.ToString());
                }
            }

            totals.Add(total);
        }

        Console.WriteLine("Task 1: " + totals.Sum());

        // Task 2
        double task2Total = 0;
        foreach (string line in text)
        {
            string[] splitLine = line.Split(" ");
            string gears = splitLine[0];
            List<int> groups = splitLine[1].Split(",").Select(int.Parse).ToList();

            // Unfold five times
            for (int i = 0; i < 5; i++)
            {
                gears = UnfoldRow(gears, groups);
                List<List<int>> permutations = Combinations(gears, groups);
                task2Total += permutations.Count;
            }
        }

        Console.WriteLine("Task 2: " + task2Total);
    }

    static string UnfoldRow(string gears, List<int> groups)
    {
        StringBuilder unfoldedRow = new StringBuilder();

        // Duplicate gears five times with '?'
        for (int i = 0; i < 5; i++)
        {
            unfoldedRow.Append(gears);
            unfoldedRow.Append("?.");
        }

        // Duplicate groups
        for (int i = 0; i < 5; i++)
        {
            unfoldedRow.Append(string.Join(",", groups));
            unfoldedRow.Append(',');
        }

        return unfoldedRow.ToString().TrimEnd(',');
    }

    static List<int> Arrangement(string gear)
    {
        List<int> ints = new List<int>();

        bool broken = false;
        foreach (char c in gear)
        {
            if (c == '#')
            {
                if (!broken)
                {
                    broken = true;
                    ints.Add(1);
                    continue;
                }
                ints[ints.Count - 1] = ints.Last() + 1;
            }
            else
            {
                broken = false;
            }
        }

        return ints;
    }

    static List<List<int>> Combinations(string gear, List<int> group)
    {
        List<List<int>> listCombos = new List<List<int>>();

        int totalBroken = group.Sum();
        int foundBroken = gear.Count(x => x == '#');
        List<int> questions = new List<int>();

        for (int i = 0; i < gear.Length; i++)
        {
            if (gear[i] == '?')
            {
                questions.Add(i);
            }
        }

        int toAdd = totalBroken - foundBroken;

        if (questions.Count > 0 && toAdd >= 0 && toAdd <= questions.Count)
        {
            GenerateCombinations(questions, toAdd, 0, new List<int>(), listCombos);
        }

        return listCombos;
    }

    static void GenerateCombinations(List<int> questions, int remaining, int start, List<int> currentCombo, List<List<int>> result)
    {
        if (remaining == 0)
        {
            result.Add(new List<int>(currentCombo));
            return;
        }

        for (int i = start; i < questions.Count; i++)
        {
            currentCombo.Add(questions[i]);
            GenerateCombinations(questions, remaining - 1, i + 1, currentCombo, result);
            currentCombo.RemoveAt(currentCombo.Count - 1);
        }
    }
}



*/










/*using System.Collections.Generic;
using System.Reflection;
using System.Text;

string? rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
List<string> text = new List<string>(File.ReadAllLines(rootDirectory + "/test.txt"));

List<string> gears = [];
List<string> strGroups = [];
List<List<int>> groups = [];
List<(List<int>, int, IEnumerable<IEnumerable<int>>)> memo = new List<(List<int>, int, IEnumerable<IEnumerable<int>>)>();

foreach (string line in text)
{
    string[] splitLine = line.Split(" ");
    gears.Add(splitLine[0]);
    strGroups.Add(splitLine[1]);
}

for (int i = 0; i < strGroups.Count; i++)
{
    string[] splitgroups = strGroups[i].Split(",");
    groups.Add([]);
    foreach (string group in splitgroups)
    {
        groups[i].Add(int.Parse(group));
    }
}


List <List<List<int>>> permutations = [];
for (int i = 0; i < gears.Count; i++)
{
    permutations.Add(Combinations(gears[i], groups[i]));
}


double total = 0;
for (int i = 0; i < permutations.Count; i++)
{
    List<string> found = [];
    foreach (List<int> group in permutations[i])
    {
        StringBuilder record = new StringBuilder(gears[i]);
        foreach (int j in group)
        {
            record[j] = '#';
        }
        List<int> list = Arrangement(record.ToString());
        bool same = true;
        if (list.Count == groups[i].Count)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j] != groups[i][j])
                {
                    same = false;
                    break;
                }
            }
        }
        else { same = false; }
        if (same && !found.Contains(record.ToString()))
        {
            total++;
            found.Add(record.ToString());
        }
    }
}

Console.WriteLine("Task 1: " + total);

List<int> Arrangement(string gear)
{
    List<int> ints = [];

    bool broken = false;
    foreach (char c in gear)
    {
        if (c == '#')
        {
            if (!broken)
            {
                broken = true;
                ints.Add(1);
                continue;
            }
            ints[ints.Count - 1] = ints.Last() + 1;
        }
        else
        {
            broken = false;
        }
    }

    return ints;
}

static List<List<int>> Combinations(string gear, List<int> group)
{
    Console.WriteLine(gear);
    List<List<int>> listCombos = new List<List<int>>();

    int totalBroken = group.Sum();
    int foundBroken = gear.Count(x => x == '#');
    List<int> questions = new List<int>();

    for (int i = 0; i < gear.Length; i++)
    {
        if (gear[i] == '?')
        {
            questions.Add(i);
        }
    }

    int toAdd = totalBroken - foundBroken;

    if (questions.Count > 0 && toAdd >= 0 && toAdd <= questions.Count)
    {
        HashSet<int> memo = new HashSet<int>();
        GenerateCombinations(questions, toAdd, 0, new List<int>(), listCombos, memo);
    }

    return listCombos;
}

static void GenerateCombinations(List<int> questions, int remaining, int start, List<int> currentCombo, List<List<int>> result, HashSet<int> memo)
{
    if (remaining == 0)
    {
        int hash = GetCombinationHashCode(currentCombo);
        if (memo.Add(hash))
        {
            result.Add(new List<int>(currentCombo));
        }
        return;
    }

    for (int i = start; i < questions.Count; i++)
    {
        currentCombo.Add(questions[i]);
        GenerateCombinations(questions, remaining - 1, i + 1, currentCombo, result, memo);
        currentCombo.RemoveAt(currentCombo.Count - 1);
    }
}

static int GetCombinationHashCode(List<int> combination)
{
    // Simple hash code calculation for the combination
    return combination.Aggregate(17, (current, item) => current * 23 + item.GetHashCode());
}*/

/*List<List<int>> Combinations(string gear, List<int> group)
{
    IEnumerable<IEnumerable<int>> combinations = [];
    int totalBroken = group.Sum();
    int foundBroken = gear.Count(x => x == '#');
    List<int> questions = [];
    for (int i = 0; i < gear.Length; i++)
    {
        if (gear[i] == '?')
        {
            questions.Add(i);
        }
    }
    int toAdd = totalBroken - foundBroken;
    if (questions.Count() > 0)
    {
        combinations = intCombos.Combos(questions, toAdd);
    }
    List<List<int>> listCombos = [];
    foreach (IEnumerable<int> combos in combinations)
    {
        listCombos.Add(combos.ToList());
    }
    return listCombos;
}

static class intCombos
{
    private static Dictionary<(int, int), IEnumerable<IEnumerable<int>>> memo = new Dictionary<(int, int), IEnumerable<IEnumerable<int>>>();

    public static IEnumerable<IEnumerable<T>> Combos<T>(this IEnumerable<T> elements, int k)
    {
        return Combos(elements.ToList(), k);
    }

    private static IEnumerable<IEnumerable<int>> Combos(List<int> elements, int k)
    {
        var key = (elements.Count, k);

        // Check if the result is already computed and stored in the memo
        if (memo.TryGetValue(key, out var result))
        {
            return result;
        }

        IEnumerable<IEnumerable<int>> combinations = k == 0
            ? new[] { new int[0] }
            : elements.SelectMany((e, i) => elements.Skip(i + 1).Combos(k - 1).Select(c => (new[] { e }).Concat(c)));

        // Store the result in the memo
        memo[key] = combinations;

        return combinations;
    }
}*/


/*static class intCombos
{
    public static IEnumerable<IEnumerable<T>> Combos<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
          elements.SelectMany((e, i) =>
            elements.Skip(i + 1).Combos(k - 1).Select(c => (new[] { e }).Concat(c)));
    }
}*/


/*gears = [];
strGroups = [];
groups = [];
foreach (string line in text)
{
    string[] splitLine = line.Split(" ");
    gears.Add(splitLine[0]);
    strGroups.Add(splitLine[1]);
}

for (int i = 0; i < strGroups.Count; i++)
{
    string curGroup = strGroups[i];
    for (int j = 0; j < 4; j++)
    {
        strGroups[i] += curGroup;
    }
}
for (int i = 0; i < strGroups.Count; i++)
{
    string[] splitgroups = strGroups[i].Split(",");
    groups.Add(new List<int>());
    foreach (string group in splitgroups)
    {
        groups[i].Add(int.Parse(group));
    }
}
for (int i = 0; i < gears.Count; i++)
{
    string curGear = gears[i];
    for (int j = 0; j < 4; j++)
    {
        gears[i] += '?';
        gears[i] += curGear;
    }
}*/