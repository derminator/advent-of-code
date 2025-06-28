using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent_of_code_2016;

public static class Day11
{
    private static List<Floor> _floors =
    [
        new([
            new Generator(Element.Polonium),
            new Generator(Element.Thulium),
            new Microchip(Element.Thulium),
            new Microchip(Element.Promethium),
            new Generator(Element.Ruthenium),
            new Microchip(Element.Ruthenium),
            new Generator(Element.Cobalt),
            new Microchip(Element.Cobalt)
        ]),
        new([
            new Microchip(Element.Polonium),
            new Microchip(Element.Promethium)
        ]),
        new([]),
        new([])
    ];


    public static void Run()
    {
        var elevator = new Elevator();
    }

    private class Elevator
    {
        public int CurrentFloor { get; } = 0;
        [CanBeNull] public IDevice Device1 { get; set; }
        [CanBeNull] public IDevice Device2 { get; set; }

        public bool IsMoveValid(bool direction)
        {
            if ((!direction && CurrentFloor == 0) || (direction && CurrentFloor == 3)) return false;
            if (Device1 == null || Device2 == null) return false;
            var targetFloor = CurrentFloor + (direction ? 1 : -1);
            var devices = new List<IDevice> { Device1, Device2 };
            devices.AddRange(_floors[targetFloor].Devices);
            return devices.Any(d => d is Microchip &&
                                    !devices.Any(d2 => d2 is Generator && d2.Type == d.Type) &&
                                    devices.Any(d2 => d2 is Generator && d2.Type == d.Type));
        }
    }

    private enum Element
    {
        Polonium,
        Thulium,
        Promethium,
        Ruthenium,
        Cobalt
    }

    private interface IDevice
    {
        Element Type { get; }
    }

    private class Generator(Element type) : IDevice
    {
        public Element Type { get; } = type;
    }

    private class Microchip(Element type) : IDevice
    {
        public Element Type { get; } = type;
    }

    private class Floor(List<IDevice> devices)
    {
        public List<IDevice> Devices { get; } = devices;
    }
}

public class State
{
    public int ElevatorFloor { get; set; }
    public Dictionary<Element, int> GeneratorFloors { get; set; }
    public Dictionary<Element, int> MicrochipFloors { get; set; }

    // Implement GetHashCode and Equals for proper state comparison
    // Implement canonical representation for optimization
}

public int FindMinimumMoves()
{
    var queue = new Queue<(State state, int moves)>();
    var visited = new HashSet<State>();

    // Add initial state
    queue.Enqueue((initialState, 0));

    while (queue.Count > 0)
    {
        var (currentState, moves) = queue.Dequeue();

        if (IsGoalState(currentState))
            return moves;

        foreach (var nextState in GetValidNextStates(currentState))
            if (!visited.Contains(nextState))
            {
                visited.Add(nextState);
                queue.Enqueue((nextState, moves + 1));
            }
    }

    return -1; // No solution found
}