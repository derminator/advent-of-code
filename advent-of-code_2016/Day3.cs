using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public class Day3
    {
        private static readonly int[][] Triangles = File.ReadAllLines("../../../.aoc/2016/3").Select(l =>
            l.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();

        private static bool IsTriangle(int a, int b, int c)
        {
            return a + b > c && a + c > b && b + c > a;
        }

        public static void Run()
        {
            Console.WriteLine(Triangles.Count(t => IsTriangle(t[0], t[1], t[2])));
        }
    }
}