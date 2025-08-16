using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_2016;

public static class Day14
{
    private const string Salt = "zpqevtbw";
    private static readonly Dictionary<char, Queue<int>> Pending = new();

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
        Pending.Clear(); // ensure no state leakage across runs
        var keys = new List<int>();
        var index = 0;
        while (keys.Count < 64)
        {
            var hash = CalculateMd5(index);

            // Find the first triple in this hash
            char? tripleChar = null;
            var runChar = (char?)null;
            var runLen = 0;
            foreach (var c in hash)
            {
                if (runChar == c)
                {
                    runLen++;
                }
                else
                {
                    runChar = c;
                    runLen = 1;
                }

                if (tripleChar == null && runLen >= 3) tripleChar = c;
                // Only the first triple counts
                // Do not break; continue to also detect quintuples in the same pass
            }

            // Detect all characters that have a quintuple in this hash
            var quintChars = new HashSet<char>();
            runChar = null;
            runLen = 0;
            foreach (var c in hash)
            {
                if (runChar == c)
                {
                    runLen++;
                }
                else
                {
                    runChar = c;
                    runLen = 1;
                }

                if (runLen >= 5) quintChars.Add(c);
            }

            // Expire old pending triples for all characters
            var expireBefore = index - 1000;
            foreach (var kvp in Pending)
            {
                var q = kvp.Value;
                while (q.Count > 0 && q.Peek() < expireBefore) q.Dequeue();
            }

            // Collect validated triples (strictly earlier than the current index) and add in global ascending order
            var validatedThisIndex = new List<int>();
            foreach (var c in quintChars)
            {
                if (!Pending.TryGetValue(c, out var q) || q.Count == 0) continue;
                // Only validate triples where i >= expireBefore AND i < index
                while (q.Count > 0 && q.Peek() >= expireBefore && q.Peek() < index) validatedThisIndex.Add(q.Dequeue());
            }

            if (validatedThisIndex.Count > 0)
            {
                validatedThisIndex.Sort(); // ensure global order among all validated triples at this index
                foreach (var i in validatedThisIndex)
                {
                    keys.Add(i);
                    if (keys.Count != 64) continue;
                    Console.WriteLine(keys[63]);
                    return;
                }
            }

            // Enqueue triple AFTER processing quintuples to avoid same-index validation
            if (tripleChar is { } t)
            {
                if (!Pending.TryGetValue(t, out var q))
                {
                    q = new Queue<int>();
                    Pending[t] = q;
                }

                q.Enqueue(index);
            }

            index++;
        }

        // Fallback (normally returned earlier)
        Console.WriteLine(keys[63]);
    }
}