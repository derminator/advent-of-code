using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace advent_of_code_2016
{
    internal class Day1
    {
        private static readonly string[] Instructions = File.ReadAllText("../../../.aoc/2016/1").Split(
            new[] { ", " }, StringSplitOptions.None);

        private static readonly List<Tuple<int, int>> _visited = new List<Tuple<int, int>>();

        private Direction _direction = Direction.Up;
        private int _horizontalPosition;

        private int _verticalPosition;

        private void FollowInstruction(string instruction)
        {
            var turn = instruction[0];
            var distance = int.Parse(instruction.Substring(1));
            // Adjust direction
            if (turn == 'R')
                _direction = (Direction)(((int)_direction + 1) % 4);
            else
                _direction = (Direction)(((int)_direction - 1 + 4) % 4);

            // Move
            if (_direction == Direction.Up || _direction == Direction.Down)
                _verticalPosition += (_direction == Direction.Up ? 1 : -1) * distance;
            else
                _horizontalPosition += (_direction == Direction.Right ? 1 : -1) * distance;
        }

        private int FindDistance()
        {
            return Math.Abs(_horizontalPosition) + Math.Abs(_verticalPosition);
        }

        public static void Run()
        {
            var map = new Day1();
            var part2 = 0;
            foreach (var instruction in Instructions)
            {
                map.FollowInstruction(instruction);
                var location = new Tuple<int, int>(map._horizontalPosition, map._verticalPosition);
                if (part2 == 0 && _visited.Contains(location))
                    part2 = map.FindDistance();
                else
                    _visited.Add(location);
            }

            var part1 = map.FindDistance();
            Console.WriteLine(part1);
            Console.WriteLine(part2);
        }

        private enum Direction
        {
            Up,
            Right,
            Down,
            [UsedImplicitly] Left
        }
    }
}