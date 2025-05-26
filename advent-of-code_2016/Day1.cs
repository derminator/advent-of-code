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

        private readonly List<Tuple<int, int>> _visited = new List<Tuple<int, int>>();

        private Direction _direction = Direction.Up;

        private Tuple<int, int> _firstDoubledLocation;
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
            for (var i = 0; i < distance; i++)
            {
                if (_direction == Direction.Up || _direction == Direction.Down)
                    _verticalPosition += _direction == Direction.Up ? 1 : -1;
                else
                    _horizontalPosition += _direction == Direction.Right ? 1 : -1;

                if (_firstDoubledLocation != null) continue;
                var location = new Tuple<int, int>(_horizontalPosition, _verticalPosition);
                if (_visited.Contains(location))
                    _firstDoubledLocation = location;
                else
                    _visited.Add(location);
            }
        }

        private int FindDistance(int horizontalPosition, int verticalPosition)
        {
            return Math.Abs(horizontalPosition) + Math.Abs(verticalPosition);
        }

        public static void Run()
        {
            var map = new Day1();
            foreach (var instruction in Instructions)
            {
                map.FollowInstruction(instruction);
            }

            var part1 = map.FindDistance(map._horizontalPosition, map._verticalPosition);
            Console.WriteLine(part1);
            var part2 = map.FindDistance(map._firstDoubledLocation.Item1, map._firstDoubledLocation.Item2);
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