using System;
using System.Collections.Generic;
using System.Linq;

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
            new Generator(Element.Promethium),
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
        // Goal state: all devices should be on the top floor (MaxFloor)
        var totalDevices = InitialFloors.Sum(f => f.Devices.Count);
        return state.Floors[MaxFloor].Devices.Count == totalDevices;
    }

    private static int FindMinimumMoves()
    {
        var queue = new Queue<(State state, int moves)>();
        var visited = new HashSet<State>();
        var initialState = new State(new Elevator(0), InitialFloors);

        // Validate initial state
        if (!IsStateValid(initialState)) throw new InvalidOperationException("Initial state is invalid");

        queue.Enqueue((initialState, 0));
        visited.Add(initialState);

        while (queue.Count > 0)
        {
            var (currentState, moves) = queue.Dequeue();

            if (IsGoalState(currentState))
                return moves;

            foreach (var nextState in currentState.GetValidNextStates().Where(nextState => visited.Add(nextState)))
            {
                queue.Enqueue((nextState, moves + 1));
            }
        }

        return -1; // No solution found
    }

    private static bool IsStateValid(State state)
    {
        // Check each floor for validity
        return state.Floors.All(floor => IsFloorValid(floor.Devices));
    }

    private static bool IsFloorValid(List<IDevice> devices)
    {
        var microchips = devices.OfType<Microchip>().ToList();
        var generators = devices.OfType<Generator>().ToList();

        // If there are no generators, any microchips are safe
        if (generators.Count == 0)
            return true;

        // If there are generators, each microchip must have its matching generator present
        // A microchip is destroyed if it's on the same floor as any generator of a different element
        // unless its own generator is also present to shield it
        return microchips.All(microchip =>
            generators.Any(g => g.Type == microchip.Type));
    }

    // Helper method to combine hash codes (replacement for HashCode.Combine)
    private static int CombineHashCodes(int h1, int h2)
    {
        return ((h1 << 5) + h1) ^ h2;
    }

    private class Elevator(int currentFloor)
    {
        public int CurrentFloor { get; } = currentFloor;

        public override int GetHashCode()
        {
            return CurrentFloor.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Elevator other && CurrentFloor == other.CurrentFloor;
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
            return CombineHashCodes(nameof(Generator).GetHashCode(), Type.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return obj is Generator other && Type == other.Type;
        }

        public override string ToString()
        {
            return $"{Type}Generator";
        }
    }

    private class Microchip(Element type) : IDevice
    {
        public Element Type { get; } = type;

        public override int GetHashCode()
        {
            return CombineHashCodes(nameof(Microchip).GetHashCode(), Type.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return obj is Microchip other && Type == other.Type;
        }

        public override string ToString()
        {
            return $"{Type}Microchip";
        }
    }

    private class Floor(List<IDevice> devices)
    {
        public List<IDevice> Devices { get; } = devices;

        public override int GetHashCode()
        {
            var orderedDevices = Devices.OrderBy(d => d.GetHashCode());
            var hash = 17;
            foreach (var device in orderedDevices) hash = hash * 31 + device.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Floor other) return false;
            if (Devices.Count != other.Devices.Count) return false;

            var thisDevices = Devices.OrderBy(d => d.GetHashCode()).ToList();
            var otherDevices = other.Devices.OrderBy(d => d.GetHashCode()).ToList();

            return thisDevices.SequenceEqual(otherDevices);
        }
    }

    private class State(Elevator elevator, List<Floor> floors)
    {
        private Elevator Elevator { get; } = elevator;
        public List<Floor> Floors { get; } = floors;

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + Elevator.GetHashCode();
            for (var i = 0; i < Floors.Count; i++)
            {
                hash = hash * 31 + i; // Include floor index to preserve order
                hash = hash * 31 + Floors[i].GetHashCode();
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is not State other) return false;
            if (!Elevator.Equals(other.Elevator)) return false;
            if (Floors.Count != other.Floors.Count) return false;

            return Floors.SequenceEqual(other.Floors);
        }

        public List<State> GetValidNextStates()
        {
            var nextStates = new List<State>();
            var currentFloor = Floors[Elevator.CurrentFloor];
            var availableDevices = currentFloor.Devices.ToList();

            // Generate all possible combinations of devices to carry (1 or 2 devices)
            var deviceCombinations = new List<List<IDevice>>();

            // Single device combinations
            deviceCombinations.AddRange(availableDevices.Select(device => new List<IDevice> { device }));

            // Two device combinations
            for (var i = 0; i < availableDevices.Count; i++)
            for (var j = i + 1; j < availableDevices.Count; j++)
                deviceCombinations.Add([availableDevices[i], availableDevices[j]]);

            // Try each combination with each possible floor move (up/down)
            foreach (var combination in deviceCombinations)
            {
                // Try moving up
                if (Elevator.CurrentFloor < MaxFloor)
                {
                    var upState = CreateStateAfterMove(combination, true);
                    if (upState != null && IsStateValid(upState)) nextStates.Add(upState);
                }

                // Try moving down
                if (Elevator.CurrentFloor > 0)
                {
                    var downState = CreateStateAfterMove(combination, false);
                    if (downState != null && IsStateValid(downState)) nextStates.Add(downState);
                }
            }

            return nextStates;
        }

        private State CreateStateAfterMove(List<IDevice> devicesToMove, bool moveUp)
        {
            var newFloor = Elevator.CurrentFloor + (moveUp ? 1 : -1);
            var newFloors = new List<Floor>();

            // Copy all floors
            for (var i = 0; i < Floors.Count; i++)
            {
                var devices = new List<IDevice>(Floors[i].Devices);

                if (i == Elevator.CurrentFloor)
                    // Remove devices that are moving from the current floor
                    foreach (var device in devicesToMove)
                        devices.Remove(device);
                else if (i == newFloor)
                    // Add devices that are moving to a new floor
                    devices.AddRange(devicesToMove);

                newFloors.Add(new Floor(devices));
            }

            // Create a new elevator state
            var newElevator = new Elevator(newFloor);

            return new State(newElevator, newFloors);
        }
    }
}