using System.Reflection;
using System.Text.RegularExpressions;

#region Const color-cubes numbers
const int RedCubes = 12;
const int GreenCubes = 13;
const int BlueCubes = 14;
#endregion

// Used to find my folder 
var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "puzzleInput.txt");

// It`s for reading my txt file
StreamReader streamReader = new(filePath);
string? line = string.Empty;

// Here i`ll save my all games which was read from txt 
Dictionary<string, string> games = [];
Dictionary<string, string> secondGames = [];

// Here i`ll save my found correct values
List<int> winGames = [];
List<int> secretNumbers = [];

// Make a dictionary 
// Where Key is Game and Value is colors with numbers
while (line != null)
{
    line = streamReader.ReadLine();
    if (line != null)
    {
        var splitedStringInput = line.Split(':');
        games.Add(splitedStringInput[0], splitedStringInput[1]);
        secondGames.Add(splitedStringInput[0], splitedStringInput[1]);
    }
}

streamReader.Close();

// Running whole games is remove all games where color more than const
foreach (var game in games)
{
    List<int> redNumbers = [];
    List<int> greenNumbers = [];
    List<int> blueNumbers = [];

    var foundRedCubes = Regex.Matches(game.Value, @"\d+ red").Select(e => e.Value).ToList().Select(e => e.Replace("red", ""));
    foreach (var red in foundRedCubes)
    {
        if (int.Parse(red) > RedCubes)
        {
            games.Remove(game.Key);
            break;
        }
    }

    var foundGreenCubes = Regex.Matches(game.Value, @"\d+ green").Select(e => e.Value).ToList().Select(e => e.Replace("green", ""));
    foreach (var green in foundGreenCubes)
    {
        if (int.Parse(green) > GreenCubes)
        {
            games.Remove(game.Key);
            break;
        }
    }

    var foundBlueCubes = Regex.Matches(game.Value, @"\d+ blue").Select(e => e.Value).ToList().Select(e => e.Replace("blue", ""));
    foreach (var blue in foundBlueCubes)
    {
        if (int.Parse(blue) > BlueCubes)
        {
            games.Remove(game.Key);
            break;
        }
    }
}

// Only for remove name "Game" is not necessary
foreach (var game in games.Keys)
{
    winGames.Add(int.Parse(game.Replace("Game", "")));
}

Console.WriteLine($"The secret code of second day, Part 1: {winGames.Sum()} ");

// Second part here only need to find out the max value from colors in every game.
foreach (var game in secondGames)
{
    int redNumber;
    int greenNumber;
    int blueNumber;

    var foundRedCubes = Regex.Matches(game.Value, @"\d+ red").Select(e => e.Value).Select(e => e.Replace("red", "")).Select(int.Parse).ToList();
    redNumber = foundRedCubes.Max();

    var foundGreenCubes = Regex.Matches(game.Value, @"\d+ green").Select(e => e.Value).Select(e => e.Replace("green", "")).Select(int.Parse).ToList();
    greenNumber = foundGreenCubes.Max();

    var foundBlueCubes = Regex.Matches(game.Value, @"\d+ blue").Select(e => e.Value).Select(e => e.Replace("blue", "")).Select(int.Parse).ToList();
    blueNumber = foundBlueCubes.Max();

    secretNumbers.Add(redNumber * greenNumber * blueNumber);
}

Console.WriteLine($"The secret code of second day, Part 2: {secretNumbers.Sum()} ");