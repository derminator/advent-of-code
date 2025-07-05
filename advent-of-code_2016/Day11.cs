using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent_of_code_2016;

public static class Day11
{
    private const int MaxFloor = 3;

    private static readonly List<Floor> InitialFloors =
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
        Console.WriteLine(FindMinimumMoves());
    }

    private static bool IsGoalState(State state)
    {
        return state.Elevator.Device1 == null && state.Elevator.Device2 == null && state.Floors
            .Except([state.Floors[MaxFloor]]).All(f => f.Devices.Count == 0);
        ;
    }

    private static int FindMinimumMoves()
    {
        var queue = new Queue<(State state, int moves)>();
        var visited = new HashSet<State>();
        var initialState = new State(new Elevator(0, null, null), InitialFloors);

        // Add initial state
        queue.Enqueue((initialState, 0));

        while (queue.Count > 0)
        {
            var (currentState, moves) = queue.Dequeue();

            if (IsGoalState(currentState))
                return moves;

            foreach (var nextState in currentState.GetValidNextStates()
                         .Where(nextState => !visited.Contains(nextState)))
            {
                visited.Add(nextState);
                queue.Enqueue((nextState, moves + 1));
            }
        }

        return -1; // No solution found
    }

    private class Elevator(int currentFloor, [CanBeNull] IDevice device1, [CanBeNull] IDevice device2)
    {
        public int CurrentFloor { get; } = currentFloor;
        [CanBeNull] public IDevice Device1 { get; } = device1;
        [CanBeNull] public IDevice Device2 { get; } = device2;

        public bool IsMoveValid(bool direction)
        {
            if ((!direction && CurrentFloor == 0) || (direction && CurrentFloor == 3)) return false;
            if (Device1 == null || Device2 == null) return false;
            var targetFloor = CurrentFloor + (direction ? 1 : -1);
            var devices = new List<IDevice> { Device1, Device2 };
            devices.AddRange(InitialFloors[targetFloor].Devices);
            return devices.Any(d => d is Microchip &&
                                    !devices.Any(d2 => d2 is Generator && d2.Type == d.Type) &&
                                    devices.Any(d2 => d2 is Generator && d2.Type == d.Type));
        }

        public override int GetHashCode()
        {
            var devicesStr = string.Join(",",
                new[] { Device1, Device2 }.OrderBy(d => d.GetHashCode()).Select(d => d.GetHashCode()));
            var input = $"{CurrentFloor}|{devicesStr}";
            return input.GetHashCode();
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

        public override int GetHashCode()
        {
            return $"{GetType().Name}{Type}".GetHashCode();
        }
    }

    private class Microchip(Element type) : IDevice
    {
        public Element Type { get; } = type;
    }

    private class Floor(List<IDevice> devices)
    {
        public List<IDevice> Devices { get; } = devices;

        public override int GetHashCode()
        {
            var devicesStr = string.Join(",", Devices.OrderBy(d => d.GetHashCode()).Select(d => d.GetHashCode()));
            return $"{GetType().Name}{devicesStr}".GetHashCode();
        }
    }

    private class State(
        Elevator elevator,
        List<Floor> floors)
    {
        public Elevator Elevator { get; } = elevator;
        public List<Floor> Floors { get; } = floors;

        public override int GetHashCode()
        {
            var floorsStr = string.Join(",", Floors.OrderBy(f => f.GetHashCode()).Select(f => f.GetHashCode()));
            var input = $"{Elevator.GetHashCode()}|{floorsStr}";
            return input.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is not State) return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public List<State> GetValidNextStates()
        {
            return []; // TODO
        }
    }
}