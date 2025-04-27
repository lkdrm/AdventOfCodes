using AdventOfCodding2023.ReadPuzzles;

namespace AdventOfCodding2023.Days
{
    /// <summary>
    /// This class is used to solve the third day of the Advent of Codding 2023 challenge.
    /// </summary>
    public static class Day3
    {
        private static int LenghtOfText { get; set; }
        private static int WidthOfText { get; set; }
        private static int SecretPasswordPart1 { get; set; } = 0;
        private static int SecretPasswordPart2 { get; set; } = 0;
        private static readonly List<List<List<int>>> _correctNumbers = [];

        /// <summary>
        /// This method is used to solve the first part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart1(string dayPuzzle)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);
            GetWidthAndLength(puzzle);
            ProcessPuzzle(puzzle);

            return SecretPasswordPart1.ToString();
        }

        /// <summary>
        /// This method is used to solve the second part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart2(string dayPuzzle)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);
            CreateSpace();
            ProcessPuzzleNumbers(puzzle);
            ProcessFinalNumbers(puzzle);
            return SecretPasswordPart2.ToString();
        }

        /// <summary>
        /// This method is used to process the puzzle and find the secret password.
        /// </summary>
        /// <param name="puzzle">Puzzle</param>
        private static void ProcessPuzzle(string[] puzzle)
        {
            for (int line = 0; line < LenghtOfText; line++)
            {
                for (int elementInLine = 0; elementInLine < WidthOfText; elementInLine++)
                {
                    var start = elementInLine;
                    var numbers = string.Empty;

                    while (elementInLine < WidthOfText && char.IsDigit(puzzle[line][elementInLine]))
                    {
                        numbers += puzzle[line][elementInLine++];
                    }

                    if (!string.IsNullOrEmpty(numbers))
                    {
                        var number = int.Parse(numbers);

                        if (FindSymbol(line, start - 1, puzzle) || FindSymbol(line, elementInLine, puzzle))
                        {
                            SecretPasswordPart1 += number;
                        }
                        else
                        {
                            for (int element = start - 1; element <= elementInLine; element++)
                            {
                                //Checking if two are symbol
                                if (FindSymbol(line - 1, element, puzzle) || FindSymbol(line + 1, element, puzzle))
                                {
                                    SecretPasswordPart1 += number;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to split the cards into two parts.
        /// </summary>
        /// <param name="puzzle">Puzzle</param>
        private static void ProcessPuzzleNumbers(string[] puzzle)
        {
            for (int line = 0; line < LenghtOfText; line++)
            {
                var start = 0;
                var element = 0;

                while (element < WidthOfText)
                {
                    start = element;
                    var numbers = string.Empty;

                    while (element < WidthOfText && char.IsDigit(puzzle[line][element]))
                    {
                        numbers += puzzle[line][element];
                        element++;
                    }

                    if (string.IsNullOrEmpty(numbers))
                    {
                        element++;
                        continue;
                    }

                    var number = int.Parse(numbers);

                    FindSymbol(line, start - 1, number, puzzle);
                    FindSymbol(line, element, number, puzzle);

                    for (var value = start - 1; value <= element; value++)
                    {
                        FindSymbol(line - 1, value, number, puzzle);
                        FindSymbol(line + 1, value, number, puzzle);
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to process the final numbers.
        /// </summary>
        /// <param name="puzzle">Puzzle</param>
        private static void ProcessFinalNumbers(string[] puzzle)
        {
            for (int firstValue = 0; firstValue < LenghtOfText; firstValue++)
            {
                for (int secondValue = 0; secondValue < WidthOfText; secondValue++)
                {
                    List<int> finalizationNumbers = _correctNumbers[firstValue][secondValue];
                    if (puzzle[firstValue][secondValue] == '*' && finalizationNumbers.Count == 2)
                    {
                        SecretPasswordPart2 += finalizationNumbers[0] * finalizationNumbers[1];
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get the width and length of the puzzle.
        /// </summary>
        /// <param name="puzzle">Puzzle</param>
        private static void GetWidthAndLength(string[] puzzle)
        {
            LenghtOfText = puzzle.Length;
            WidthOfText = puzzle[0].Length;
        }

        /// <summary>
        /// This method is used to find the symbol in the puzzle.
        /// </summary>
        /// <param name="first">First symbol</param>
        /// <param name="second">Second symbol</param>
        /// <param name="puzzle">Puzzle</param>
        /// <returns>The symbol</returns>
        private static bool FindSymbol(int first, int second, string[] puzzle)
        {
            bool symbol = false;
            if (first >= 0 && first < LenghtOfText && second >= 0 && second < WidthOfText)
            {
                if (puzzle[first][second] != '.' && !char.IsDigit(puzzle[first][second]))
                {
                    symbol = true;
                }
            }
            return symbol;
        }

        /// <summary>
        /// This method is used to find the symbol in the puzzle.
        /// </summary>
        /// <param name="first">First symbol</param>
        /// <param name="second">Second symbol</param>
        /// <param name="puzzle">Puzzle</param>
        /// <param name="number">Number</param>
        /// <returns>The symbol</returns>
        private static bool FindSymbol(int first, int second, int number, string[] puzzle)
        {
            if (!(first >= 0 && first < LenghtOfText && second >= 0 && second < WidthOfText))
            {
                return false;
            }
            if (puzzle[first][second] == '*')
            {
                _correctNumbers[first][second].Add(number);
            }

            return puzzle[first][second] != '.' && !char.IsDigit(puzzle[first][second]);
        }

        /// <summary>
        /// This method is used to create a space for the correct numbers.
        /// </summary>
        private static void CreateSpace()
        {
            for (int value = 0; value < LenghtOfText; value++)
            {
                _correctNumbers.Add([]);
                for (int anotherValue = 0; anotherValue < WidthOfText; anotherValue++)
                {
                    _correctNumbers[value].Add([]);
                }
            }
        }
    }
}
