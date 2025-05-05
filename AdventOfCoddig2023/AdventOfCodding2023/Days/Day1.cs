using AdventOfCodding2023.ReadPuzzles;

namespace AdventOfCodding2023.Days
{
    /// <summary>
    /// This class is used to solve the first day of the Advent of Codding 2023 challenge.
    /// </summary>
    public static class Day1
    {
        /// <summary>
        /// This method reads the input file and returns the sum of the numbers found in the text.
        /// </summary>
        private static readonly Dictionary<string, int> _numbersToText = new()
        {{ "one",   1 },
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

        /// <summary>
        /// This method reads the input file and returns the sum of the numbers found in the text.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string Result(string dayPuzzle)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);
            List<int> numbersFromText = [];

            foreach (var line in puzzle)
            {
                Dictionary<int, int> foundCombinations = [];
                foreach (var number in _numbersToText.Keys)
                {
                    var element = line.IndexOf(number);
                    if (element >= 0)
                    {
                        foundCombinations.Add(element, _numbersToText[number]);
                    }
                    var element2 = line.LastIndexOf(number);
                    if (element2 >= 0 && !element2.Equals(element))
                    {
                        foundCombinations.Add(element2, _numbersToText[number]);
                    }
                }
                numbersFromText.Add(int.Parse(foundCombinations[foundCombinations.Keys.Min()].ToString() + foundCombinations[foundCombinations.Keys.Max()].ToString()));
            }
            return numbersFromText.Sum().ToString();
        }
    }
}
