using System;
using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_2016
{
    public class Day5
    {
        private const string Input = "ffykfhsq";
        private static readonly MD5 Md5 = MD5.Create();
        private int _index;

        private string _findNextHash()
        {
            var hashString = "";
            while (!hashString.StartsWith("00000"))
            {
                var hashInput = Input + _index;
                var hash = Md5.ComputeHash(Encoding.ASCII.GetBytes(hashInput));
                hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                _index++;
            }

            return hashString;
        }

        public static void Run()
        {
            var part1Password = "";
            var part1Processor = new Day5();
            while (part1Password.Length < 8)
            {
                var hashString = part1Processor._findNextHash();
                part1Password += hashString[5];
            }

            Console.WriteLine(part1Password);

            var part2Password = "________";
            var part2Processor = new Day5();
            while (part2Password.Contains("_"))
            {
                var hash = part2Processor._findNextHash();
                var position = hash[5];
                if (position < '0' || position > '7') continue;
                var posInt = int.Parse(position.ToString());
                if (part2Password[posInt] == '_')
                    // Replace part2Password[posInt] with hash[6]
                    part2Password = part2Password.Remove(posInt, 1).Insert(posInt, hash[6].ToString());
            }

            Console.WriteLine(part2Password);
        }
    }
}