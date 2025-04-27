using AdventOfCodding2023.ReadPuzzles;
using System.Text.RegularExpressions;

namespace AdventOfCodding2023.Days
{
    /// <summary>
    /// This class is used to solve the fourth day of the Advent of Codding 2023 challenge.
    /// </summary>
    public static class Day4
    {
        /// <summary>
        /// This property is used to store the Card and Value is all numbers
        /// </summary>
        private static readonly Dictionary<string, string> AllCards = [];

        /// <summary>
        /// This property is used to store the Card pairs
        /// </summary>
        private static readonly Dictionary<string, int> CardPairs = [];

        private static int SecretPassword { get; set; } = 0;

        /// <summary>
        /// This method is used to solve the first part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart1(string dayPuzzle)
        {
            ParsePuzzleInput(dayPuzzle, "first");

            foreach (var card in AllCards)
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
                    SecretPassword += (int)Math.Pow(2, combinations - 1);
                }
            }

            return SecretPassword.ToString();
        }

        /// <summary>
        /// This method is used to solve the second part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart2(string dayPuzzle)
        {
            ParsePuzzleInput(dayPuzzle, "second");

            foreach (var card in AllCards)
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

            return CardPairs.Values.Sum().ToString();
        }

        /// <summary>
        /// Used to parse the puzzle input and store the card and value in a dictionary.
        /// </summary>
        /// <param name="dayPuzzle">Puzzle</param>
        /// <param name="part">Part of the puzzle</param>
        private static void ParsePuzzleInput(string dayPuzzle, string part)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);

            switch(part)
            {
                case "first":
                    foreach (var line in puzzle)
                    {
                        AllCards.Add(line.Split(':')[0], line.Split(':')[1]);
                    }
                    break;
                case "second":
                    foreach (var key in AllCards.Keys)
                    {
                        CardPairs.Add(key.Replace(" ", ""), 1);
                    }
                    break;
            }
        }
    }
}
