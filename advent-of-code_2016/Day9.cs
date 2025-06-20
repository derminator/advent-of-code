using System;
using System.IO;
using System.Text;

namespace advent_of_code_2016
{
    public static class Day9
    {
        private static long DecompressFile(string text, bool v2)
        {
            var length = 0L;
            using (var fs = new StringReader(text))
            {
                var inBlock = false;
                var afterX = false;
                var repeatChars = "";
                var numberString = "";
                while (fs.Peek() != -1)
                {
                    var character = (char)fs.Read();
                    if (!inBlock)
                    {
                        if (character != '(')
                        {
                            length += 1;
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

                        var charsToRepeat = repeatCharBuilder.ToString();
                        var numCharsToRepeat = v2 ? DecompressFile(charsToRepeat, true) : charsToRepeat.Length;
                        while (repeatCount-- > 0) length += numCharsToRepeat;
                    }
                }
            }

            return length;
        }

        public static void Run()
        {
            var fileContents = File.ReadAllText("../../../.aoc/2016/9");
            Console.WriteLine(DecompressFile(fileContents, false));
            Console.WriteLine(DecompressFile(fileContents, true));
        }
    }
}