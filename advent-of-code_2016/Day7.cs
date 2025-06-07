using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2016
{
    public static class Day7
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

        private static bool SupportsSsl(string address)
        {
            var inHypernet = false;
            var hypernetAbas = new List<string>();
            var supernetAbas = new List<string>();
            var history = new char?[] { null, null };
            foreach (var digit in address)
            {
                if (digit == '[' || digit == ']') history = new char?[] { null, null };
                switch (digit)
                {
                    case '[':
                        inHypernet = true;
                        break;
                    case ']':
                        inHypernet = false;
                        break;
                    default:
                        if (history.All(c => c != null) && history[0] == digit && history[1] != digit)
                        {
                            var list = inHypernet ? hypernetAbas : supernetAbas;
                            var compareList = inHypernet ? supernetAbas : hypernetAbas;
                            var aba = $"{history[0]}{history[1]}{digit}";
                            var abaReverse = $"{history[1]}{digit}{history[1]}";
                            if (compareList.Any(i => i == abaReverse)) return true;
                            list.Add(aba);
                        }

                        history[0] = history[1];
                        history[1] = digit;
                        break;
                }
            }

            return false;
        }

        public static void Run()
        {
            Console.WriteLine(Addresses.Count(SupportsTls));
            Console.WriteLine(Addresses.Count(SupportsSsl));
        }
    }
}