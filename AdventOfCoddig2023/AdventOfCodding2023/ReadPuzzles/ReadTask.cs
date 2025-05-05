using System.Reflection;

namespace AdventOfCodding2023.ReadPuzzles
{
    /// <summary>
    /// This class is used to read the input file.
    /// </summary>
    public static class ReadTask
    {
        /// <summary>
        /// This method gets the current directory of the assembly.
        /// </summary>
        private static readonly string CurrentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        /// <summary>
        /// This method reads the input file and returns the lines as an array of strings.
        /// </summary>
        /// <param name="currentDayTask">Current day secret</param>
        /// <returns>Array of strings</returns>
        public static string[] ReadText(string currentDayTask) => File.ReadAllLines(Path.Combine(CurrentDirectory, currentDayTask));
    }
}
