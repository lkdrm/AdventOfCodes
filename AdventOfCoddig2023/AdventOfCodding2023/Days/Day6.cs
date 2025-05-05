using AdventOfCodding2023.ReadPuzzles;

namespace AdventOfCodding2023.Days
{
    /// <summary>
    /// This class is used to solve the sixth day of the Advent of Codding 2023 challenge.
    /// </summary>
    public static class Day6
    {
        /// <summary>
        /// This property is used to store the time average
        /// </summary>
        private static List<int> TimeAveragePart1;

        /// <summary>
        /// This property is used to store the time average
        /// </summary>
        private static int TimeAveragePart2;

        /// <summary>
        /// This property is used to store the distance
        /// </summary>
        private static List<int> DistancePart1;

        /// <summary>
        /// This property is used to store the distance
        /// </summary>
        private static long DistancePart2;

        /// <summary>
        /// This property is used to store the time iteration
        /// </summary>
        private static int DistanceElemet = 0;

        /// <summary>
        /// This property is used to store the save all possible ways
        /// </summary>
        private static int DifferentWays = 0;

        private static int SecretPasswordPart1 = 1;
        private static int SecretPasswordPart2 = 0;

        /// <summary>
        /// This method is used to solve the first part of the puzzle.
        /// </summary>
        /// <param name="dayPuzzle">Task to resolve</param>
        /// <returns>Resolve task result</returns>
        public static string ResultPart1(string dayPuzzle)
        {
            var puzzle = ReadTask.ReadText(dayPuzzle);
            TimeAveragePart1 = [.. puzzle[0].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse)];
            DistancePart1 = [.. puzzle[1].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse)];

            foreach (var t in TimeAveragePart1)
            {
                // It`s i like make simulation to holding button in ms.
                var rangedElements = Enumerable.Range(1, t);
                // Simulating running seconds
                foreach (var ms in rangedElements)
                {
                    // What it`s mean example:
                    // 1 * (7 - 1) > 9 = 6 > 9 => which is False
                    if (ms * (t - ms) > DistancePart1[DistanceElemet])
                    {
                        DifferentWays++;
                    }
                }

                DistanceElemet++;
                SecretPasswordPart1 *= DifferentWays;
                DifferentWays = 0;
            }
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
            TimeAveragePart2 = puzzle[0].Replace(" ", "").Split(":").Skip(1).Select(int.Parse).First();
            DistancePart2 = puzzle[1].Replace(" ", "").Split(":").Skip(1).Select(long.Parse).First();

            // It`s i like make simulation to holding button in ms.
            var rangedElements = Enumerable.Range(1, TimeAveragePart2);
            // Simulating running seconds
            foreach (long ms in rangedElements)
            {
                if (ms * (TimeAveragePart2 - ms) > DistancePart2)
                {
                    SecretPasswordPart2++;
                }
            }
            return SecretPasswordPart2.ToString();
        }
    }
}
