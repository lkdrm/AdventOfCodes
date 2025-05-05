using AdventOfCodding2023;
using AdventOfCodding2023.Days;

#region Start
Console.Title = "Advent of codding 2023";
var legendDefault = ConsoleColor.Green;
var resultPrintColorText = ConsoleColor.White;
Console.ForegroundColor = legendDefault;
BeautyPrint.Print(BeautyPrint.Welcome);
#endregion

Console.ForegroundColor = resultPrintColorText;
Console.WriteLine($"The secret code for Day 1 is: \"{Day1.Result("Day1.txt")}\".");
Console.WriteLine();
Console.WriteLine($"The secret code for Day 2 part 1 is: \"{Day2.ResultPart1("Day2.txt")}\".");
Console.WriteLine($"The secret code for Day 2 part 2 is: \"{Day2.ResultPart2()}\".");
Console.WriteLine();
Console.WriteLine($"The secret code for Day 3 part 1 is: \"{Day3.ResultPart1("Day3.txt")}\".");
Console.WriteLine($"The secret code for Day 3 part 2 is: \"{Day3.ResultPart2("Day3.txt")}\".");
Console.WriteLine();
Console.WriteLine($"The secret code for Day 4 part 1 is: \"{Day4.ResultPart1("Day4.txt")}\".");
Console.WriteLine($"The secret code for Day 4 part 2 is: \"{Day4.ResultPart2("Day4.txt")}\".");
Console.WriteLine();
Console.WriteLine($"The secret code for Day 5 part 1 is: \"{Day5.ResultPart1("Day5.txt")}\".");
Console.WriteLine($"The secret code for Day 5 part 2 is: \"{Day5.ResultPart2("Day5.txt")}\".");
Console.WriteLine();
Console.WriteLine($"The secret code for Day 6 part 1 is: \"{Day6.ResultPart1("Day6.txt")}\".");
Console.WriteLine($"The secret code for Day 6 part 2 is: \"{Day6.ResultPart2("Day6.txt")}\".");

Console.ReadLine();