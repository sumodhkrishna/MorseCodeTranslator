using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var signals = ".?";
        Console.WriteLine(String.Join(",", Possibilities(signals)));

        var sampleString = "Beth,Charles,Danielle,Adam,Eric \n17945,10091,10088,3907,10132 \n2,12,13,48,11";
        string value = SortCsvColumns(sampleString);
        Console.WriteLine(value);
    }
    public static string SortCsvColumns(string csvData)
    {
        string[] lines = csvData.Split(
             new string[] { "\n" },
            StringSplitOptions.None
        );
        var values = lines
           .Select(l => new { Fields = l.Split(','), Line = l })
           .Select(x => x.Fields)
           .ToArray();
        var orderdArray = Rotate(values).OrderBy(x => x[0]);
        var stringBuilder = new System.Text.StringBuilder();
        foreach (var x in Rotate(orderdArray.ToArray()))
            stringBuilder.AppendLine(String.Join(',', x));
        return stringBuilder.ToString();
    }

    private static string[][] Rotate(string[][] input)
    {
        int length = input[0].Length;
        string[][] rotatedArray = new string[length][];
        for (int x = 0; x < length; x++)
        {
            rotatedArray[x] = input.Select(p => p[x]).ToArray();
        }
        return rotatedArray;
    }

    public static string[] Possibilities(string signals)
    {
        var signalDictionary = new Dictionary<string, string>()
        {
            {"..","I" },
            {".-","A" },
            {"...","S" },
            {"..-","U" },
            {".-.","R" },
            {".--","W" },
            {"-..","D"},
            {".","E" },
            {"-.-","K" },
            {"--.","G" },
            {"-.","N" },
            {"--","M" },
            {"---","O" },
            {"-","T" }
        };
        Regex regex = new Regex("^" + Regex.Escape(signals).Replace("\\?", ".") + "$");
        var keys = signalDictionary.Keys.Where(k => regex.IsMatch(k)).ToList();
        return keys.Where(k => signalDictionary.ContainsKey(k)).Select(k => signalDictionary[k]).ToArray();
    }



}