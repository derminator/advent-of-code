using System.IO;

namespace advent_of_code_2016
{
    public static class Day10
    {
        private static readonly int[] Target = { 61, 17 };
        private static readonly string[] Instructions = File.ReadAllLines("../../../.aoc/2016/10");

        public static void Run()
        {
        }

        private class Bot
        {
            public int? Hand1 { get; set; } = null;
            public int? Hand2 { get; set; } = null;
        }
    }
}