using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    internal class Day2
    {
        private static readonly char[][] Keypad = { "123".ToCharArray(), "456".ToCharArray(), "789".ToCharArray() };

        private static readonly string[] Instructions = File.ReadAllLines("../../../.aoc/2016/2");

        private Tuple<int, int> _currentPosition = new Tuple<int, int>(1, 1);

        private void Move(char direction)
        {
            Tuple<int, int> newPosition;
            switch (direction)
            {
                case 'U':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 - 1);
                    break;
                case 'D':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 + 1);
                    break;
                case 'L':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1 - 1, _currentPosition.Item2);
                    break;
                case 'R':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1 + 1, _currentPosition.Item2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (newPosition.Item1 >= 0 && newPosition.Item1 < Keypad.Length && newPosition.Item2 >= 0 &&
                newPosition.Item2 < Keypad[0].Length)
                _currentPosition = newPosition;
        }

        private char GetDigit(char[] instrLine)
        {
            foreach (var instruction in instrLine) Move(instruction);

            return Keypad[_currentPosition.Item1][_currentPosition.Item2];
        }

        public static void Run()
        {
            var part1 = new Day2();
            var answer = Instructions.Aggregate("", (current, instruction) =>
                current + part1.GetDigit(instruction.ToCharArray()));
            Console.WriteLine(answer);
        }
    }
}