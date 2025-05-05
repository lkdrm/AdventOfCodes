namespace AdventOfCodding2023
{
    /// <summary>
    /// Static class for better cmd view
    /// </summary>
    public static class BeautyPrint
    {
        public const string Welcome = "Here u can see the resolved advent tasks!";
        private const int StarWidth = 60;

        /// <summary>
        /// Make better human print
        /// </summary>
        public static void Print(string txt)
        {
            Console.WriteLine();
            Console.WriteLine(new string('*', StarWidth));
            int padding = (StarWidth - txt.Length - 2) / 2;
            Console.WriteLine("*" + new string(' ', padding) + $" {txt} " + new string(' ', padding - 1) + "*");
            Console.WriteLine(new string('*', StarWidth));
            Console.WriteLine();
        }
    }
}
