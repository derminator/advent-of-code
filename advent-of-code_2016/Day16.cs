using System.Text;

namespace advent_of_code_2016;

public static class Day16
{
    private const string Input = "01111010110010011";
    private const int Disk1Size = 272;
    private const int Disk2Size = 35651584;

    private static string DragonCurve(string a)
    {
        var b = string.Join("", a.Reverse().Select(c => c == '0' ? '1' : '0'));
        return $"{a}0{b}";
    }

    private static string ComputeCheckSum(string input)
    {
        while (true)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < input.Length; i += 2) builder.Append(input[i] != input[i + 1] ? 0 : 1);

            var result = builder.ToString();
            if (result.Length % 2 != 0) return result;
            input = result;
        }
    }

    public static void Run()
    {
        GenerateDiskChecksum(Disk1Size);
        GenerateDiskChecksum(Disk2Size);
    }

    private static void GenerateDiskChecksum(int diskSize)
    {
        var data = Input;
        while (data.Length < diskSize) data = DragonCurve(data);

        data = data[..diskSize];
        Console.WriteLine(ComputeCheckSum(data));
    }
}