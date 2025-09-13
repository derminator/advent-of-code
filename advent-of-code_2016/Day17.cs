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

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}