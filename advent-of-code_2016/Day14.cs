using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_2016;

public static class Day14
{
    private const string Salt = "zpqevtbw";
    private static readonly List<PotentialSolution> PotentialSolutions = new();

    private static string CalculateMd5(int index)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(Salt + index);
        var hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        var sb = new StringBuilder();
        foreach (var b in hashBytes) sb.Append(b.ToString("x2"));

        return sb.ToString();
    }

    public static void Run()
    {
        var foundKeys = 0;
        var index = 0;
        while (foundKeys < 64)
        {
            var hash = CalculateMd5(index);
            char? key = null;
            var duplicateCount = 0;
            foreach (var c in hash)
            {
                if (c == key)
                {
                    duplicateCount++;
                }
                else
                {
                    key = c;
                    duplicateCount = 1;
                }

                switch (duplicateCount)
                {
                    case 3:
                        PotentialSolutions.Add(new PotentialSolution(key.Value, index));
                        break;
                    case 5:
                        var existing = PotentialSolutions.FirstOrDefault(p => p.Character == key.Value);
                        if (existing != null && index - existing.Index <= 1000)
                        {
                            foundKeys++;
                            PotentialSolutions.Remove(existing);
                        }

                        break;
                }
            }

            index++;
        }

        Console.WriteLine(index);
    }

    private record PotentialSolution(char Character, int Index);
}