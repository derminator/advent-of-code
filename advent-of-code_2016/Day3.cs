using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public static class Day3
    {
        private static readonly int[][] Part1Triangles = File.ReadAllLines("../../../.aoc/2016/3").Select(l =>
            l.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();

        private static bool IsTriangle(int a, int b, int c)
        {
            return a + b > c && a + c > b && b + c > a;
        }

        public static void Run()
        {
            Console.WriteLine(Part1Triangles.Count(t => IsTriangle(t[0], t[1], t[2])));

            var validPart2 = 0;

            // process blocks of 3 rows at a time
            for (var row = 0; row < Part1Triangles.Length; row += 3)
                // for each of the three columns in that block
            for (var col = 0; col < 3; col++)
            {
                var a = Part1Triangles[row][col];
                var b = Part1Triangles[row + 1][col];
                var c = Part1Triangles[row + 2][col];

                if (IsTriangle(a, b, c))
                    validPart2++;
            }

            Console.WriteLine(validPart2);
        }
    }
}