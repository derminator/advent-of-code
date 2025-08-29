namespace advent_of_code_2016;

public static class Day15
{
    private static readonly List<List<Disc>> TimeMap =
    [
        [
            new Disc(17, 15),
            new Disc(3, 2),
            new Disc(19, 4),
            new Disc(13, 2),
            new Disc(7, 2),
            new Disc(5, 0)
        ]
    ];

    public static void Run()
    {
        var startTime = 0;
        while (!CheckForSolution(startTime))
        {
            startTime++;
        }

        Console.WriteLine(startTime);

        // Part 2
        for (var i = 0; i < TimeMap.Count; i++) TimeMap[i].Add(new Disc(11, (0 + i) % 11));

        startTime = 0;
        while (!CheckForSolution(startTime)) startTime++;

        Console.WriteLine(startTime);
    }

    private static int GetPositionAtTime(int time, int discId)
    {
        while (TimeMap.Count <= time)
            TimeMap.Add(TimeMap.Last().Select(disc =>
                disc with { StartPosition = (disc.StartPosition + 1) % disc.Positions }).ToList());

        return TimeMap[time][discId - 1].StartPosition;
    }

    private static bool CheckForSolution(int startTime)
    {
        for (var i = 1; i <= TimeMap[0].Count; i++)
            if (GetPositionAtTime(startTime + i, i) != 0)
                return false;

        return true;
    }

    private record Disc(int Positions, int StartPosition);
}