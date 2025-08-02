using System;
using System.Collections.Generic;

namespace advent_of_code_2016;

public static class Day13
{
    private const int FavoriteNumber = 1358;
    private static readonly State InitialState = new(1, 1);
    private static readonly State FinalState = new(31, 39);

    private static bool IsOpenSpace(int x, int y)
    {
        var calc = x * x + 3 * x + 2 * x * y + y + y * y + FavoriteNumber;
        // Count the number of set bits (1's) in the binary representation
        var bitCount = CountSetBits(calc);
        // Return false if odd number of bits (wall), true if even (open space)
        return bitCount % 2 == 0;
    }

    private static int CountSetBits(int number)
    {
        var count = 0;
        while (number != 0)
        {
            number &= number - 1; // Brian Kernighan's algorithm
            count++;
        }

        return count;
    }

    private static IEnumerable<State> GetNextStates(State state)
    {
        var x = state.X;
        var y = state.Y;

        if (x + 1 >= 0 && y >= 0 && IsOpenSpace(x + 1, y)) yield return new State(x + 1, y);
        if (x >= 0 && y + 1 >= 0 && IsOpenSpace(x, y + 1)) yield return new State(x, y + 1);
        if (x - 1 >= 0 && y >= 0 && IsOpenSpace(x - 1, y)) yield return new State(x - 1, y);
        if (x >= 0 && y - 1 >= 0 && IsOpenSpace(x, y - 1)) yield return new State(x, y - 1);
    }

    public static void Run()
    {
        var queue = new Queue<(State state, int moves)>();
        queue.Enqueue((InitialState, 0));
        var visited = new HashSet<State> { InitialState };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.state.Equals(FinalState))
            {
                Console.WriteLine(current.moves);
                return;
            }

            foreach (var nextState in GetNextStates(current.state))
                if (visited.Add(nextState))
                    queue.Enqueue((nextState, current.moves + 1));
        }

        Console.WriteLine("No solution found");
    }

    private class State(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public override bool Equals(object obj)
        {
            return obj is State state && Equals(state);
        }

        private bool Equals(State other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}