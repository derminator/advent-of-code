using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace advent_of_code_2016
{
    public class Day6
    {
        private static readonly string[] Input = File.ReadAllLines("../../../.aoc/2016/6");

        public static void Run()
        {
            var p1Message = new StringBuilder();
            var p2Message = new StringBuilder();
            var wordLen = Input[0].Length;
            for (var i = 0; i < wordLen; i++)
            {
                var stats = new Dictionary<char, int>();
                foreach (var attempt in Input)
                {
                    var c = attempt[i];
                    stats[c] = stats.TryGetValue(c, out var stat) ? stat + 1 : 1;
                }

                p1Message.Append(stats.OrderByDescending(kvp => kvp.Value).First().Key);
                p2Message.Append(stats.OrderBy(kvp => kvp.Value).First().Key);
            }

            Console.WriteLine(p1Message);
            Console.WriteLine(p2Message);
        }
    }
}