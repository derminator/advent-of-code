using System.Collections.Generic;

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