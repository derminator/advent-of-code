using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public class Day8
    {
        private const int Rows = 6;
        private const int Cols = 50;

        private static readonly string[] Instructions = File.ReadAllLines("../../../.aoc/2016/8");

        private readonly bool[,] _screen = new bool[Rows, Cols];

        private void _rect(int width, int height)
        {
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                _screen[y, x] = true;
        }

        private void _rotateRow(int row, int distance)
        {
            var original = (bool[,])_screen.Clone();
            for (var i = 0; i < Cols; i++)
            {
                var newCol = (i + distance) % Rows;
                _screen[row, newCol] = original[row, i];
            }
        }

        private void _rotateColumn(int col, int distance)
        {
            var original = (bool[,])_screen.Clone();
            for (var i = 0; i < Rows; i++)
            {
                var newRow = (i + distance) % Cols;
                _screen[newRow, col] = original[i, col];
            }
        }

        private void DoInstruction(string instruction)
        {
            var words = instruction.Split(' ');
            if (words[0] == "rect")
            {
                var dimensions = words[1].Split('x');
                _rect(int.Parse(dimensions[0]), int.Parse(dimensions[1]));
            }
            else
            {
                var target = int.Parse(words[2].Split('=')[1]);
                var distance = int.Parse(words[4]);
                if (words[1] == "row")
                    _rotateRow(target, distance);
                else
                    _rotateColumn(target, distance);
            }
        }

        public static void Run()
        {
            var part1 = new Day8();
            foreach (var instruction in Instructions) part1.DoInstruction(instruction);

            Console.WriteLine(part1._screen.Cast<bool>().Count(b => b));
        }
    }
}