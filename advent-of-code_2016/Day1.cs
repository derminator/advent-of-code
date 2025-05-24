using System;
using System.IO;

namespace advent_of_code_2016
{
    internal class Day1
    {
        private static readonly string[] Instructions = File.ReadAllText("../../../.aoc/2016/1").Split(
            new[] { ", " }, StringSplitOptions.None);

        private Direction direction = Direction.Up;
        private int horizontalPosition;

        private int verticalPosition;

        private void FollowInstruction(string instruction)
        {
            var turn = instruction[0];
            var distance = int.Parse(instruction.Substring(1));
            // Adjust direction
            if (turn == 'R')
                direction = (Direction)(((int)direction + 1) % 4);
            else
                direction = (Direction)(((int)direction - 1 + 4) % 4);

            // Move
            if (direction == Direction.Up || direction == Direction.Down)
                verticalPosition += (direction == Direction.Up ? 1 : -1) * distance;
            else
                horizontalPosition += (direction == Direction.Right ? 1 : -1) * distance;
        }

        public static void Run()
        {
            var map = new Day1();
            foreach (var instruction in Instructions) map.FollowInstruction(instruction);

            Console.WriteLine(Math.Abs(map.horizontalPosition) + Math.Abs(map.verticalPosition));
        }

        private enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
    }
}