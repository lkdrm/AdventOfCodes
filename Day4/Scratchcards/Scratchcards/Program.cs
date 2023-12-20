#region Find and read my text input 
// Used to find my folder 
using System.Reflection;
using System.Text.RegularExpressions;

var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "puzzleInput.txt");

// It`s for reading my txt file
StreamReader streamReader = new(filePath);
string? line = string.Empty;
#endregion

// Here i`ll save my all cards which was read from txt 
Dictionary<string, string> allCards = [];

// Here i`ll save win
int puzzleSecretPassword = 0;

// Make a dictionary 
// Where Key is Card and Value is all numbers
while (line != null)
{
    line = streamReader.ReadLine();
    if (line != null)
    {
        var splitedStringInput = line.Split(':');
        allCards.Add(splitedStringInput[0], splitedStringInput[1]);
    }
}

streamReader.Close();

#region Puzzle Part 1
foreach (var card in allCards)
{
    int combinations = 0;
    List<string> allCardNumbers = [.. card.Value.Split('|')];
    List<string> winningCardNumbers = [.. allCardNumbers[0].Split().Where(e => e != "")];
    List<string> havingCardNumbers = [.. allCardNumbers[1].Split().Where(e => e != "")];

    foreach (var firstNumber in winningCardNumbers)
    {
        if (havingCardNumbers.Contains(firstNumber))
        {
            combinations++;
        }
    }

    if (combinations > 0)
    {
        puzzleSecretPassword += (int)Math.Pow(2, combinations - 1);
    }
}

Console.WriteLine($"The secret password of Day 4 Part 1 is : {puzzleSecretPassword}");
puzzleSecretPassword = 0;
#endregion

#region Puzzle Part 2 
//All copies of cards.
Dictionary<string, int> CardPairs = [];

foreach (var key in allCards.Keys)
{
    //Ugly replace
    CardPairs.Add(key.Replace(" ", ""), 1);
}

foreach (var card in allCards)
{
    var key = int.Parse(Regex.Matches(card.Key, @"\d+").Select(e => e.Value).First());

    List<string> allCardNumbers = [.. card.Value.Split('|')];
    List<string> winningCardNumbers = [.. allCardNumbers[0].Split().Where(e => e != "")];
    List<string> havingCardNumbers = [.. allCardNumbers[1].Split().Where(e => e != "")];

    foreach (var firstNumber in winningCardNumbers)
    {
        if (havingCardNumbers.Contains(firstNumber))
        {
            key++;
            var currentKey = Regex.Replace(card.Key.Replace(" ", ""), @"\d+", key.ToString());
            CardPairs[currentKey] += CardPairs[card.Key.Replace(" ", "")];
        }
    }
}

Console.WriteLine($"The secret password of Day 4 Part 2 is : {CardPairs.Values.Sum()}");
#endregion