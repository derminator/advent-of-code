using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_2016;

public static class Day17
{
    private const string Input = "qljzarfv";

    private static char PathChar(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => 'U',
            Direction.Down => 'D',
            Direction.Left => 'L',
            Direction.Right => 'R',
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static void Run()
    {
        var moves = Array.Empty<char>();

        var queue = new Queue<State>();
        queue.Enqueue(new State(moves, 0, 0));

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state is { X: 3, Y: 3 })
            {
                Console.WriteLine(new string(state.PreviousMoves));
                return;
            }

            foreach (var move in state.GetValidNeighbors()) queue.Enqueue(move);
        }

        Console.WriteLine("No valid solution");
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private class Hash(char[] previousMoves)
    {
        private readonly string _hash = string.Concat(MD5.HashData(Encoding.UTF8
                                                .GetBytes(Input + new string(previousMoves)))
                                            .Select(b => b.ToString("x2")))
                                        ?? throw new InvalidOperationException();

        public bool IsDoorOpen(Direction direction)
        {
            var checkChar = _hash[(int)direction];
            return checkChar is >= 'b' and <= 'f';
        }
    }

    private record State(char[] PreviousMoves, int X, int Y)
    {
        public List<State> GetValidNeighbors()
        {
            var hash = new Hash(PreviousMoves);
            return Enum.GetValues<Direction>().Where(dir => hash.IsDoorOpen(dir)).Select(dir =>
            {
                var pathChar = dir.PathChar();
                var newX = X;
                var newY = Y;
                switch (dir)
                {
                    case Direction.Up:
                        newY -= 1;
                        break;
                    case Direction.Down:
                        newY += 1;
                        break;
                    case Direction.Left:
                        newX -= 1;
                        break;
                    case Direction.Right:
                        newX += 1;
                        break;
                }

                return new State(PreviousMoves.Append(pathChar).ToArray(), newX, newY);
            }).Where(state => state.X is >= 0 and <= 3 && state.Y is >= 0 and <= 3).ToList();
        }
    }
}