using System.Reflection;

// Used to find my folder 
var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "puzzleInput.txt");

// It`s for reading my txt file
StreamReader streamReader = new(filePath);

string? line = string.Empty;

// Here I`ll save my found numbers
List<int> numbersFromText = [];

// All possible combinations 
Dictionary<string, int> numbersToText = new()
{
    { "one",   1 },
    { "two",   2 },
    { "three", 3 },
    { "four",  4 },
    { "five",  5 },
    { "six",   6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine",  9 },
    { "1",  1 },
    { "2",  2 },
    { "3",  3 },
    { "4",  4 },
    { "5",  5 },
    { "6",  6 },
    { "7",  7 },
    { "8",  8 },
    { "9",  9 },
};

while (line != null)
{
    line = streamReader.ReadLine();

    string? resultNumber = null;

    // My found combinations
    Dictionary<int, int> foundCombinations = [];

    // How without null controlling
    if (!string.IsNullOrEmpty(line))
    {
        foreach (var number in numbersToText.Keys)
        {
            var element = line.IndexOf(number);
            if (element >= 0)
            {
                foundCombinations.Add(element, numbersToText[number]);
            }
            var element2 = line.LastIndexOf(number);
            if (element2 >= 0 && !element2.Equals(element))
            {
                foundCombinations.Add(element2, numbersToText[number]);
            }
        }
        numbersFromText.Add(int.Parse(foundCombinations[foundCombinations.Keys.Min()].ToString() + foundCombinations[foundCombinations.Keys.Max()].ToString()));
    }
}

// Don`t forget to close of course
streamReader.Close();

Console.WriteLine($"The secret code of first day is : {numbersFromText.Sum()}");