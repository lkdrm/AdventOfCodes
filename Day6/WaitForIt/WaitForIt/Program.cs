using System.Reflection;

#region Find and read my text input
// Used to find my folder 
var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "puzzleInput.txt");

// It`s for reading my txt file
var puzzleText = File.ReadAllLines(filePath);
#endregion

// Here I`m removing whole spaces
// Skipping first index because its an word
// And make converting to int + List
List<int> timeAvarage = puzzleText[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
List<int> distance = puzzleText[1].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();

// It`s i`m using to switch distance
// from every finished time iteration
int distanceElemet = 0;
// Save all possible ways
int differentWays = 0;
// And here i`ll save my result of all ways
int secretAnswer = 1;
#region Part One 
foreach (var t in timeAvarage)
{
    // It`s i like make simulation to holding button in ms.
    var rangedElements = Enumerable.Range(1, t);
    // Simulating running seconds
    foreach (var ms in rangedElements)
    {
        // What it`s mean example:
        // 1 * (7 - 1) > 9 = 6 > 9 => which is False
        if (ms * (t - ms) > distance[distanceElemet])
        {
            differentWays++;
        }
    }

    distanceElemet++;
    secretAnswer *= differentWays;
    differentWays = 0;
}

Console.WriteLine($"The puzzle answer of Day 6 Part 1 is : {secretAnswer}");
#endregion

#region Part 2
int timeAvarage2 = puzzleText[0].Replace(" ", "").Split(":").Skip(1).Select(int.Parse).First();
long distance2 = puzzleText[1].Replace(" ", "").Split(":").Skip(1).Select(long.Parse).First();
secretAnswer = 0;

// It`s i like make simulation to holding button in ms.
var rangedElements2 = Enumerable.Range(1, timeAvarage2);
// Simulating running seconds
foreach (long ms in rangedElements2)
{
    if (ms * (timeAvarage2 - ms) > distance2)
    {
        secretAnswer++;
    }
}

Console.WriteLine($"The puzzle answer of Day 6 Part 2 is : {secretAnswer}");
#endregion