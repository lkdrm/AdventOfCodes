using System.Reflection;

#region Find and read my text input 
// Used to find my folder 
var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

// Used to set current txt file
var filePath = Path.Combine(currentDirectory, "puzzleInput.txt");

// It`s for reading my txt file
var puzzleText = File.ReadAllLines(filePath);
#endregion

#region Use to set my width of line & length
var lengthOText = puzzleText.Length;
var width = puzzleText[0].Length;
#endregion

// Here I`ll save the answer
var secretPassword = 0;

#region Puzzle Part 1

//Use to find symbol left or right side
bool findSymbol(int first, int second)
{
    bool symbol = false;
    if (first >= 0 && first < lengthOText && second >= 0 && second < width)
    {
        if (puzzleText[first][second] != '.' && !char.IsDigit(puzzleText[first][second]))
        {
            symbol = true;
        }
    }
    return symbol;
}

for (int line = 0; line < lengthOText; line++)
{
    for (int elementInLine = 0; elementInLine < width; elementInLine++)
    {
        var start = elementInLine;
        var numbers = string.Empty;

        while (elementInLine < width && char.IsDigit(puzzleText[line][elementInLine]))
        {
            numbers += puzzleText[line][elementInLine++];
        }

        if (!string.IsNullOrEmpty(numbers))
        {
            var number = int.Parse(numbers);

            if (findSymbol(line, start - 1) || findSymbol(line, elementInLine))
            {
                secretPassword += number;
            }
            else
            {
                for (int element = start - 1; element <= elementInLine; element++)
                {
                    //Checking if two are symbol
                    if (findSymbol(line - 1, element) || findSymbol(line + 1, element))
                    {
                        secretPassword += number;
                        break;
                    }
                }
            }
        }
    }
}

Console.WriteLine($"The puzzle answer of Day 3 Part 1 is : {secretPassword}");
secretPassword = 0;
#endregion

#region Puzzle Part 2
// Here I`ll save all numbers that fulfilled my conditions
var correctNumbers = new List<List<List<int>>>();

for (int value = 0; value < lengthOText; value++)
{
    correctNumbers.Add([]);
    for (int anotherValue = 0; anotherValue < width; anotherValue++)
    {
        correctNumbers[value].Add([]);
    }
}

//Use to find symbol and numbers around
bool findSymbols(int first, int second, int number)
{
    if (!(first >= 0 && first < lengthOText && second >= 0 && second < width))
    {
        return false;
    }
    if (puzzleText[first][second] == '*')
    {
        correctNumbers[first][second].Add(number);
    }

    return puzzleText[first][second] != '.' && !char.IsDigit(puzzleText[first][second]);
}

for (int line = 0; line < lengthOText; line++)
{
    var start = 0;
    var element = 0;

    while (element < width)
    {
        start = element;
        var numbers = string.Empty;

        while (element < width && char.IsDigit(puzzleText[line][element]))
        {
            numbers += puzzleText[line][element];
            element++;
        }

        if (string.IsNullOrEmpty(numbers))
        {
            element++;
            continue;
        }

        var number = int.Parse(numbers);

        findSymbols(line, start - 1, number);
        findSymbols(line, element, number);

        for (var value = start - 1; value <= element; value++)
        {
            findSymbols(line - 1, value, number);
            findSymbols(line + 1, value, number);
        }
    }
}

// Final step 
for (int firstValue = 0; firstValue < lengthOText; firstValue++)
{
    for (int secondValue = 0; secondValue < width; secondValue++)
    {
        List<int> finalizationNumbers = correctNumbers[firstValue][secondValue];
        if (puzzleText[firstValue][secondValue] == '*' && finalizationNumbers.Count == 2)
        {
            secretPassword += finalizationNumbers[0] * finalizationNumbers[1];
        }
    }
}

Console.WriteLine($"The puzzle answer of Day 3 Part 2 is : {secretPassword}");
#endregion