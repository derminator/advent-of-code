using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public static class Day4
    {
        private static readonly Tuple<string, int, string>[] Input = File.ReadAllLines("../../../.aoc/2016/4")
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

        private static string DecipherName(string name, int sectorId)
        {
            return new string(name.Select(c =>
            {
                if (c == '-')
                    return ' '; // Replace dash with space

                if (c < 'a' || c > 'z') return c; // Return unchanged if not a lowercase letter
                // Shift the character by sectorId positions
                var shifted = (c - 'a' + sectorId) % 26 + 'a';
                return (char)shifted;
            }).ToArray());
        }

        public static void Run()
        {
            var realRooms = Input.Where(i => CalculateChecksum(i.Item1) == i.Item3).ToArray();
            Console.WriteLine(realRooms.Sum(i => i.Item2));
            foreach (var room in realRooms)
                Console.WriteLine("Name: " + DecipherName(room.Item1, room.Item2) + "; ID: " + room.Item2);
        }
    }
}