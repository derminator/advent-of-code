using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    internal class Day2
    {
        private static readonly char[][] Part1Keypad =
            { "123".ToCharArray(), "456".ToCharArray(), "789".ToCharArray() };

        private static readonly char[][] Part2Keypad =
        {
            "  1  ".ToCharArray(), " 234 ".ToCharArray(), "56789".ToCharArray(),
            " ABC ".ToCharArray(), "  D  ".ToCharArray()
        };

        private static readonly string[] Instructions = File.ReadAllLines("../../../.aoc/2016/2");

        private readonly char[][] _keypad;

        private Tuple<int, int> _currentPosition;

        private Day2(char[][] keypad)
        {
            _keypad = keypad;
            for (var x = 0; x < keypad.Length; x++)
            for (var y = 0; y < keypad[x].Length; y++)
                if (keypad[x][y] == '5')
                    _currentPosition = new Tuple<int, int>(x, y);

            if (_currentPosition == null) throw new Exception("Could not find 5");
        }

        private void Move(char direction)
        {
            Tuple<int, int> newPosition;
            switch (direction)
            {
                case 'U':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1 - 1, _currentPosition.Item2);
                    break;
                case 'D':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1 + 1, _currentPosition.Item2);
                    break;
                case 'L':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 - 1);
                    break;
                case 'R':
                    newPosition = new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (newPosition.Item1 >= 0 && newPosition.Item1 < _keypad.Length && newPosition.Item2 >= 0 &&
                newPosition.Item2 < _keypad[newPosition.Item1].Length &&
                _keypad[newPosition.Item1][newPosition.Item2] != ' ')
                _currentPosition = newPosition;
        }

        private char GetDigit(char[] instrLine)
        {
            foreach (var instruction in instrLine) Move(instruction);

            return _keypad[_currentPosition.Item1][_currentPosition.Item2];
        }

        public static void Run()
        {
            var part1 = new Day2(Part1Keypad);
            Console.WriteLine(GetCode(part1));
            var part2 = new Day2(Part2Keypad);
            Console.WriteLine(GetCode(part2));
        }

        private static string GetCode(Day2 parser)
        {
            return Instructions.Aggregate("", (current, instruction) =>
                current + parser.GetDigit(instruction.ToCharArray()));
        }
    }
}