using System;
using System.Collections.Generic;
using System.Linq;

public static class GraphValidator
{
    public static bool IsGraphicalSequence(List<int> degrees)
    {
        var seq = new List<int>(degrees);
        seq.Sort((a, b) => b.CompareTo(a));

        if (seq.Sum() % 2 != 0) return false;

        while (seq.Count > 0)
        {
            int d1 = seq[0];
            seq.RemoveAt(0);

            if (d1 == 0) return true;
            if (d1 > seq.Count) return false;

            for (int i = 0; i < d1; i++)
            {
                seq[i]--;
                if (seq[i] < 0) return false;
            }
            seq.Sort((a, b) => b.CompareTo(a));
        }
        return true;
    }
}

class Program
{
    static void Main()
    {
        var testCases = new List<(List<int> seq, bool expected)>
        {
            (new List<int> {4, 3, 3, 2, 2, 2, 1, 1}, true),
            (new List<int> {3, 3, 3, 1}, false), 
            (new List<int> {5, 5, 4, 3, 2, 1}, false)
        };

        foreach (var test in testCases)
        {
            bool result = GraphValidator.IsGraphicalSequence(test.seq);
            Console.WriteLine($"Secuencia: [{string.Join(",", test.seq)}] -> {result}");
        }
    }
}