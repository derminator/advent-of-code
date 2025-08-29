namespace advent_of_code_2016;

public static class Day15
{
    public static void Run()
    {
        var discs = new List<Disc>
        {
            new(1, 17, 15),
            new(2, 3, 2),
            new(3, 19, 4),
            new(4, 13, 2),
            new(5, 7, 2),
            new(6, 5, 0)
        };
    }

    private record Disc(int Id, int Positions, int StartPosition);
}