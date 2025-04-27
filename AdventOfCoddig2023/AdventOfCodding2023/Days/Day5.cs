using AdventOfCodding2023.ReadPuzzles;

namespace AdventOfCodding2023.Days
{
    /// <summary>
    /// This class is used to solve the fifth day of the Advent of Codding 2023 challenge.
    /// </summary>
    public static class Day5
    {
        /// <summary>
        /// This property is used to store the seeds
        /// </summary>
        private static List<long> Seeds;

        /// <summary>
        /// This property is used to store the lines and seeds
        /// </summary>
        private static readonly List<List<(long destination, long source, long range)>> LinesAndSeeds = [];

        /// <summary>
        /// This property is used to store all the seeds
        /// </summary>
        private static List<(long, long)> AllSeeds = [];

        /// <summary>
        /// This method is used to solve the first part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart1(string dayPuzzle)
        {
            ParsePuzzleInput(dayPuzzle, "first");
            return Seeds.Select(seed => FindSeedLocation(seed, LinesAndSeeds)).ToList().Min().ToString();
        }

        /// <summary>
        /// This method is used to solve the second part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart2(string dayPuzzle)
        {
            long answer = 1 << 60;
            LinesAndSeeds.Clear();
            AllSeeds = [.. Enumerable.Range(0, Seeds.Count / 2).Select(i => (Seeds[i * 2], Seeds[i * 2 + 1]))];
            ParsePuzzleInput(dayPuzzle, "second");

            return DetermineOptimalSeedPosition(answer).ToString();
        }

        /// <summary>
        /// This method is used to determine the optimal seed position.
        /// </summary>
        /// <param name="answer">Check for result</param>
        /// <returns>The result of search</returns>
        private static long DetermineOptimalSeedPosition(long answer)
        {
            foreach (var (start, R) in AllSeeds)
            {
                var currentInterval = new List<(long, long)> { (start, start + R - 1) };
                var newInterval = new List<(long, long)>();

                foreach (var line in LinesAndSeeds)
                {
                    foreach (var (lower, high) in currentInterval)
                    {
                        newInterval.AddRange(ChangeLocation(lower, high, line));
                    }

                    (currentInterval, newInterval) = (newInterval, new List<(long, long)>());
                }

                foreach (var (low, high) in currentInterval)
                {
                    answer = Math.Min(answer, low);
                }
            }

            return answer;
        }

        /// <summary>
        /// This method is used to find seed location.
        /// </summary>
        /// <param name="seed">Seed</param>
        /// <param name="linesAndSeeds">Lined & seeds</param>
        /// <returns>Location of seeds</returns>
        private static long FindSeedLocation(long seed, List<List<(long destination, long source, long range)>> linesAndSeeds)
        {
            var currentPosition = seed;

            foreach (var line in linesAndSeeds)
            {
                foreach (var (destintation, source, range) in line)
                {
                    if (source <= currentPosition && currentPosition < source + range)
                    {
                        currentPosition = destintation + (currentPosition - source);
                        break;
                    }
                }
            }

            return currentPosition;
        }

        /// <summary>
        /// This method is used to change the location of the seeds.
        /// </summary>
        /// <param name="lower">Lower</param>
        /// <param name="high">High</param>
        /// <param name="position">Position</param>
        /// <returns>Changed position</returns>
        private static IEnumerable<(long, long)> ChangeLocation(long lower, long high, List<(long, long, long)> position)
        {
            var result = new List<(long, long, long)>();

            foreach (var (destionation, source, rade) in position)
            {
                var end = source + rade - 1;
                var D = destionation - source;

                if (!(end < lower || source > high))
                {
                    result.Add((Math.Max(source, lower), Math.Min(end, high), D));
                }
            }

            foreach (var (line, r, D) in result)
            {
                yield return (line + D, r + D);

                if (result.IndexOf((line, r, D)) < result.Count - 1 && result[result.IndexOf((line, r, D)) + 1].Item1 > r + 1)
                {
                    yield return (r + 1, result[result.IndexOf((line, r, D)) + 1].Item1 - 1);
                }
            }

            if (result.Count == 0)
            {
                yield return (lower, high);
                yield break;
            }

            if (result[0].Item1 != lower)
            {
                yield return (lower, result[0].Item1 - 1);
            }

            if (result[^1].Item2 != high)
            {
                yield return (result[^1].Item2 + 1, high);
            }
        }

        /// <summary>
        /// Used to parse the puzzle input and store in to the list.
        /// </summary>
        /// <param name="dayPuzzle">Puzzle</param>
        /// <param name="part">Part of the puzzle</param>
        private static void ParsePuzzleInput(string dayPuzzle, string part)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);
            switch (part)
            {
                case "first":
                    Seeds = [.. puzzle[0].Split(' ').Skip(1).Select(long.Parse)];
                    for (int i = 2; i < puzzle.Length; i++)
                    {
                        LinesAndSeeds.Add([]);
                        i++;
                        while (i < puzzle.Length && puzzle[i] != string.Empty)
                        {
                            var linesElements = puzzle[i].Split().Select(long.Parse).ToList();
                            var destination = linesElements[0];
                            var source = linesElements[1];
                            var range = linesElements[2];
                            LinesAndSeeds.Last().Add((destination, source, range));
                            i++;
                        }
                    }
                    break;
                case "second":
                    for (int i = 2; i < puzzle.Length; i++)
                    {
                        LinesAndSeeds.Add([]);
                        i++;
                        while (i < puzzle.Length && puzzle[i] != string.Empty)
                        {
                            var linesElements = puzzle[i].Split().Select(long.Parse).ToArray();
                            LinesAndSeeds[^1].Add((linesElements[0], linesElements[1], linesElements[2]));
                            i++;
                        }
                        LinesAndSeeds[^1].Sort((x, y) => x.source.CompareTo(y.source));
                    }
                    break;
            }
        }
    }
}
