using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public class Day4
    {
        private static readonly Tuple<string, int, string>[] _input = File.ReadAllLines("../../../.aoc/2016/4")
            .Select(l =>
            {
                var lastDashIndex = l.LastIndexOf('-');
                var idChecksum = l.Substring(lastDashIndex + 1)
                    .Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                return new Tuple<string, int, string>(
                    l.Substring(0, lastDashIndex),
                    Convert.ToInt32(idChecksum[0]),
                    idChecksum[1]
                );
            }).ToArray();

        private static string CalculateChecksum(string name)
        {
            var charCounts = new Dictionary<char, int>();
            foreach (var character in name.Where(character => character != '-'))
                if (charCounts.TryGetValue(character, out var count))
                    charCounts[character] = count + 1;
                else
                    charCounts.Add(character, 1);

            // Sort by value (count) in descending order, then by key (character) in ascending order
            var sortedChars = charCounts.OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Select(pair => pair.Key)
                .Take(5); // Take the first 5 characters for checksum

            return new string(sortedChars.ToArray());
        }

        public static void Run()
        {
            Console.WriteLine(_input.Sum(i => CalculateChecksum(i.Item1) == i.Item3 ? i.Item2 : 0));
        }
    }
}