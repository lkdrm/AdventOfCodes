using AdventOfCodding2023.ReadPuzzles;
using System.Text.RegularExpressions;

namespace AdventOfCodding2023.Days
{
    /// <summary>
    /// This class is used to solve the second day of the Advent of Codding 2023 challenge.
    /// </summary>
    public static class Day2
    {
        #region Const color-cubes numbers
        const int RedCubes = 12;
        const int GreenCubes = 13;
        const int BlueCubes = 14;
        #endregion

        // Here i`ll save my all games which was read from txt 
        private static readonly Dictionary<string, string> _games = [];
        private static readonly Dictionary<string, string> _secondGames = [];

        // Here i`ll save my found correct values
        private static readonly List<int> _winGames = [];
        private static readonly List<int> _secretNumbers = [];

        /// <summary>
        /// This method is used to solve the first part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart1(string dayPuzzle)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);
            SplitCards(puzzle);

            // Running whole games is remove all games where color more than const
            foreach (var game in _games)
            {
                List<int> redNumbers = [];
                List<int> greenNumbers = [];
                List<int> blueNumbers = [];

                var foundRedCubes = Regex.Matches(game.Value, @"\d+ red").Select(e => e.Value).ToList().Select(e => e.Replace("red", ""));
                foreach (var red in foundRedCubes)
                {
                    if (int.Parse(red) > RedCubes)
                    {
                        _games.Remove(game.Key);
                        break;
                    }
                }

                var foundGreenCubes = Regex.Matches(game.Value, @"\d+ green").Select(e => e.Value).ToList().Select(e => e.Replace("green", ""));
                foreach (var green in foundGreenCubes)
                {
                    if (int.Parse(green) > GreenCubes)
                    {
                        _games.Remove(game.Key);
                        break;
                    }
                }

                var foundBlueCubes = Regex.Matches(game.Value, @"\d+ blue").Select(e => e.Value).ToList().Select(e => e.Replace("blue", ""));
                foreach (var blue in foundBlueCubes)
                {
                    if (int.Parse(blue) > BlueCubes)
                    {
                        _games.Remove(game.Key);
                        break;
                    }
                }
            }

            foreach (var game in _games.Keys)
            {
                _winGames.Add(int.Parse(game.Replace("Game", "")));
            }

            return _winGames.Sum().ToString();
        }

        /// <summary>
        /// This method is used to solve the second part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart2()
        {
            foreach (var game in _secondGames)
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

                _secretNumbers.Add(redNumber * greenNumber * blueNumber);
            }
            return _secretNumbers.Sum().ToString();
        }

        /// <summary>
        /// This method is used to split the cards from the puzzle into two dictionaries.
        /// </summary>
        /// <param name="puzzle">Puzzle</param>
        private static void SplitCards(string[] puzzle)
        {
            foreach (var line in puzzle)
            {
                var splitLine = line.Split(":");
                _games.Add(splitLine[0], splitLine[1]);
                _secondGames.Add(splitLine[0], splitLine[1]);
            }
        }
    }
}
