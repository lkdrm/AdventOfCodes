using System.Reflection;

#region Find and read my text input

// Used to find my folder 
var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "puzzleInput.txt");

// It`s for reading my txt file
var puzzleText = File.ReadAllLines(filePath);
#endregion

#region Part 1
List<long> seeds = puzzleText[0].Split(' ').Skip(1).Select(long.Parse).ToList();
List<List<(long destination, long source, long range)>> linesAndSeeds = [];

for (int i = 2; i < puzzleText.Length; i++)
{
    linesAndSeeds.Add([]);

    i++;

    while (i < puzzleText.Length && puzzleText[i] != string.Empty)
    {
        var linesElements = puzzleText[i].Split().Select(long.Parse).ToList();
        var destination = linesElements[0];
        var source = linesElements[1];
        var range = linesElements[2];
        linesAndSeeds.Last().Add((destination, source, range));

        i++;
    }
}

var foundSeeds = seeds.Select(seed => FindSeedLocation(seed, linesAndSeeds)).ToList();

static long FindSeedLocation(long seed, List<List<(long destination, long source, long range)>> linesAndSeeds)
{
    var currentPosition = seed;

    foreach (var line in linesAndSeeds)
    {
        foreach (var (destintation, source, range) in line)
        {
            if (source <= currentPosition && currentPosition < source + range)
            {
                currentPosition = destintation + (currentPosition - source);
                break;
            }
        }
    }

    return currentPosition;
}

Console.WriteLine($"The puzzle answer of Day 5 Part 1 is : {foundSeeds.Min()}");

#endregion

#region Clear list before second puzzle
linesAndSeeds.Clear();
#endregion

#region Part 2
var allSeeds = Enumerable.Range(0, seeds.Count / 2).Select(i => (seeds[i * 2], seeds[i * 2 + 1])).ToList();

for (int i = 2; i < puzzleText.Length; i++)
{
    linesAndSeeds.Add([]);

    i++;

    while (i < puzzleText.Length && puzzleText[i] != string.Empty)
    {
        var linesElements = puzzleText[i].Split().Select(long.Parse).ToArray();
        linesAndSeeds[^1].Add((linesElements[0], linesElements[1], linesElements[2]));
        i++;
    }

    linesAndSeeds[^1].Sort((x, y) => x.source.CompareTo(y.source));
}

var locations = new List<(long, long)>();
long answer = 1 << 60;

foreach (var (start, R) in allSeeds)
{
    var currentInterval = new List<(long, long)> { (start, start + R - 1) };
    var newInterval = new List<(long, long)>();

    foreach (var line in linesAndSeeds)
    {
        foreach (var (lower, high) in currentInterval)
        {
            newInterval.AddRange(ChangeLocation(lower, high, line));
        }

        (currentInterval, newInterval) = (newInterval, new List<(long, long)>());
    }

    foreach (var (low, high) in currentInterval)
    {
        answer = Math.Min(answer, low);
    }
}

static IEnumerable<(long, long)> ChangeLocation(long lower, long high, List<(long, long, long)> position)
{
    var result = new List<(long, long, long)>();

    foreach (var (destionation, source, rade) in position)
    {
        var end = source + rade - 1;
        var D = destionation - source;

        if (!(end < lower || source > high))
        {
            result.Add((Math.Max(source, lower), Math.Min(end, high), D));
        }
    }

    foreach (var (line, r, D) in result)
    {
        yield return (line + D, r + D);

        if (result.IndexOf((line, r, D)) < result.Count - 1 && result[result.IndexOf((line, r, D)) + 1].Item1 > r + 1)
        {
            yield return (r + 1, result[result.IndexOf((line, r, D)) + 1].Item1 - 1);
        }
    }

    if (result.Count == 0)
    {
        yield return (lower, high);
        yield break;
    }

    if (result[0].Item1 != lower)
    {
        yield return (lower, result[0].Item1 - 1);
    }

    if (result[^1].Item2 != high)
    {
        yield return (result[^1].Item2 + 1, high);
    }
}

Console.WriteLine($"The puzzle answer of Day 5 Part 2 is : {answer}");
#endregion