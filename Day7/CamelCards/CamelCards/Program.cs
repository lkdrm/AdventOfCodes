using System.Reflection;

#region Find and read my text input
// Used to find my folder 
var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "example.txt");

// It`s for reading my txt file
var puzzleText = File.ReadAllLines(filePath);
#endregion

List<string> CamelCardsValues =
    ["A",
        "K",
        "Q",
        "J",
        "T",
        "9",
        "8",
        "7",
        "6",
        "5",
        "4",
        "3",
        "2"];

List<string> myCards = [];
Dictionary<string, int> allCamelCards = [];
Dictionary<string, int> sortedCards = [];
Dictionary<string, int> fourSameCards = [];
Dictionary<string, int> fullHouseCards = [];
Dictionary<string, int> threeSameCards = [];
Dictionary<string, int> twoPairsCards = [];
Dictionary<string, int> topCard = [];
Dictionary<string, int> finishedSortedCards = [];
int cardRank = 1;

foreach (var readLine in puzzleText)
{
    var cutLine = readLine.Split(" ");
    allCamelCards.Add(cutLine.First(), int.Parse(cutLine[1]));
}
Console.WriteLine();

//Five of a kind
//Four of a kind
//#Full house
//Three of a kind
//Two pair
//One pair
//High card

foreach (var pairCard in allCamelCards.Keys)
{
    int sameCard = 0;
    var splitedCard = pairCard.ToCharArray();
    var j = 0;
    for (int i = 0; i < splitedCard.Length - 1; i++)
    {
        j = i + 1;
        var firstCard = splitedCard[i];
        while (j < splitedCard.Length)
        {
            var secondCard = splitedCard[j];
            if (firstCard == secondCard)
            {
                sameCard++;
            }
            j++;
        }
    }

    sortedCards.Add(pairCard, sameCard);
}

foreach (var pairCard in sortedCards)
{
    switch (pairCard.Value)
    {
        case 4:
            finishedSortedCards.Add(pairCard.Key, cardRank);
            break;
        case 3:
            break;
        case 2: 
            break;
        case 1: 
            break;
        case 0: 
            break;
    }
}

Console.WriteLine();