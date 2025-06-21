using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace advent_of_code_2016
{
    public static class Day10
    {
        private static readonly int[] Target = { 61, 17 };
        private static readonly string[] Instructions = File.ReadAllLines("../../../.aoc/2016/10");
        private static readonly Dictionary<int, Bot> Bots = new Dictionary<int, Bot>();
        private static readonly Dictionary<int, OutputBin> Outputs = new Dictionary<int, OutputBin>();

        private static IDeliverable GetTarget(string type, string id)
        {
            var targetId = int.Parse(id);
            if (type == "bot")
            {
                if (!Bots.ContainsKey(targetId))
                    Bots[targetId] = new Bot(targetId);
                return Bots[targetId];
            }
            else
            {
                if (!Outputs.ContainsKey(targetId))
                    Outputs[targetId] = new OutputBin(targetId);
                return Outputs[targetId];
            }
        }

        public static void Run()
        {
            foreach (var instruction in Instructions)
            {
                var parts = instruction.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch (parts[0])
                {
                    case "value":
                        var botId = int.Parse(parts[5]);
                        var bot = Bots.TryGetValue(botId, out var botInstance) ? botInstance : new Bot(botId);
                        bot.Deliver(int.Parse(parts[1]));
                        Bots[botId] = bot;
                        break;
                    case "bot":
                        var instructingBotId = int.Parse(parts[1]);
                        var lowTarget = GetTarget(parts[5], parts[6]);
                        var highTarget = GetTarget(parts[10], parts[11]);
                        var instructingBot = Bots.TryGetValue(instructingBotId, out var botInstance2)
                            ? botInstance2
                            : new Bot(instructingBotId);
                        instructingBot.LowTarget = lowTarget;
                        instructingBot.HighTarget = highTarget;
                        Bots[instructingBotId] = instructingBot;
                        break;
                }
            }
        }

        private interface IDeliverable
        {
            int Id { get; }
            void Deliver(int value);
        }

        private class OutputBin : IDeliverable
        {
            public OutputBin(int id)
            {
                Id = id;
            }

            public int Id { get; }

            public void Deliver(int value)
            {
            }
        }

        private class Bot : IDeliverable
        {
            private IDeliverable _highTarget;
            private IDeliverable _lowTarget;

            public Bot(int id)
            {
                Id = id;
            }

            private int? Hand1 { get; set; }
            private int? Hand2 { get; set; }

            [CanBeNull]
            public IDeliverable LowTarget
            {
                get => _lowTarget;
                set
                {
                    _lowTarget = value;
                    CheckForDelivery();
                }
            }

            [CanBeNull]
            public IDeliverable HighTarget
            {
                get => _highTarget;
                set
                {
                    _highTarget = value;
                    CheckForDelivery();
                }
            }

            public int Id { get; }

            public void Deliver(int value)
            {
                if (Hand1 == null) Hand1 = value;
                else if (Hand2 == null) Hand2 = value;
                else throw new Exception("Bot already has two hands");

                if ((Hand1 == Target[0] && Hand2 == Target[1]) || (Hand1 == Target[1] && Hand2 == Target[0]))
                {
                    Console.WriteLine(Id);
                    Environment.Exit(0);
                }

                CheckForDelivery();
            }

            private void CheckForDelivery()
            {
                if (Hand1 == null || Hand2 == null || LowTarget == null || HighTarget == null) return;
                LowTarget.Deliver(Math.Min(Hand1.Value, Hand2.Value));
                HighTarget.Deliver(Math.Max(Hand1.Value, Hand2.Value));
                Hand1 = null;
                Hand2 = null;
            }
        }
    }
}