using System;
using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_2016
{
    public class Day5
    {
        private const string Input = "ffykfhsq";

        public static void Run()
        {
            var md5 = MD5.Create();
            var index = 0;
            var password = "";
            while (password.Length < 8)
            {
                var hashInput = Input + index;
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(hashInput));
                var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                if (hashString.StartsWith("00000")) password += hashString[5];

                index++;
            }

            Console.WriteLine(password);
        }
    }
}