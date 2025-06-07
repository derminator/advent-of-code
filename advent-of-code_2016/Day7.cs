using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public class Day7
    {
        private static readonly string[] Addresses = File.ReadAllLines("../../../.aoc/2016/7");

        private static bool SupportsTls(string address)
        {
            var inHypernet = false;
            var hasAbba = false;
            var history = new char?[] { null, null, null };
            foreach (var digit in address)
            {
                if (digit == '[' || digit == ']') history = new char?[] { null, null, null };
                switch (digit)
                {
                    case '[':
                        inHypernet = true;
                        break;
                    case ']':
                        inHypernet = false;
                        break;
                    default:
                    {
                        if (history.All(c => c != null))
                            if (history[0] != history[1] && history[2] == history[1] && digit == history[0])
                            {
                                if (inHypernet) return false;
                                hasAbba = true;
                            }

                        history[0] = history[1];
                        history[1] = history[2];
                        history[2] = digit;
                        break;
                    }
                }
            }

            return hasAbba;
        }

        public static void Run()
        {
            Console.WriteLine(Addresses.Count(SupportsTls));
        }
    }
}