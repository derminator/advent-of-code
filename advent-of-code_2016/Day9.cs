using System;
using System.IO;
using System.Text;

namespace advent_of_code_2016
{
    public class Day9
    {
        private readonly string _file;

        private Day9()
        {
            var sb = new StringBuilder();
            using (var fs = File.OpenText("../../../.aoc/2016/9"))
            {
                var inBlock = false;
                var afterX = false;
                var repeatChars = "";
                var numberString = "";
                while (!fs.EndOfStream)
                {
                    var character = (char)fs.Read();
                    if (!inBlock)
                    {
                        if (character != '(')
                        {
                            sb.Append(character);
                            continue;
                        }

                        inBlock = true;
                        afterX = false;
                        repeatChars = "";
                        numberString = "";
                    }
                    else if (character == 'x')
                    {
                        afterX = true;
                    }
                    else if (character != ')')
                    {
                        if (afterX) repeatChars += character;
                        else numberString += character;
                    }
                    else
                    {
                        inBlock = false;
                        var charCount = int.Parse(numberString);
                        var repeatCount = int.Parse(repeatChars);
                        var repeatCharBuilder = new StringBuilder();
                        for (var i = 0; i < charCount; i++) repeatCharBuilder.Append((char)fs.Read());

                        repeatChars = repeatCharBuilder.ToString();
                        while (repeatCount-- > 0) sb.Append(repeatChars);
                    }
                }
            }

            _file = sb.ToString();
        }

        public static void Run()
        {
            var part1 = new Day9();
            Console.WriteLine(part1._file.Length);
        }
    }
}