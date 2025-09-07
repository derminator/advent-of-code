using System.Text;

namespace advent_of_code_2016;

public static class Day16
{
    private const string Input = "01111010110010011";
    private const int Disk1Size = 272;

    private static string DragonCurve(string a)
    {
        var b = a.Reverse().Select(c => c == '0' ? '1' : '0').ToString();
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
        var data = Input;
        while (data.Length < Disk1Size) data = DragonCurve(data);

        data = data[..Disk1Size];
        Console.WriteLine(ComputeCheckSum(data));
    }
}