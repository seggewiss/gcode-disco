using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GcodeRenumber
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2) {
                Console.WriteLine("Please provide 3 arguments: NumerationStart, Step and Input file.");
                return;
            }

            int start, step;
            string path;

            start = Convert.ToInt32(args[0]);
            step = Convert.ToInt32(args[1]);
            path = args[2];

            string[] lines = System.IO.File.ReadAllLines(path);
            lines = Renumber(start, step, lines);
            
            System.IO.File.WriteAllLines(path, lines);
        }

        private static string[] Renumber(int start, int step, string[] lines)
        {
            int lineNumber = start;
            int count = 0;
            string[] newLines = new string[lines.Length];

            foreach (var line in lines) {
                newLines[count] = RenumberLine(line, lineNumber);
                count++;
                lineNumber += step;
            }

            return newLines;
        }

        private static string RenumberLine(string line, int lineNumber)
        {
            var match = Regex.Match(line, "[nN][\\d]*");

            if (match.Success) {
                line = line.Replace(match.Value, "N" + lineNumber);

                return line;
            }

            return "N" + lineNumber + " " + line;
        }
    }
}