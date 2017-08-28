using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatternConfig {

    private static string filePath = "Assets/Script/Patterns.txt";

    private string config;
    private int[] left;
    private int[] middle;
    private int[] right;

    public const int patternLength = 10;
    public const int patternCount = 3;

    private static List<PatternConfig> patterns = new List<PatternConfig>();

    static PatternConfig()
    {
        ReadConfig();
    }

    private static void ReadConfig()
    {
        if (File.Exists(filePath) == false)
            return;

        //StreamReader sr = new StreamReader(filePath);
        //string line;
        //while((line = sr.ReadLine()) != null)
        //{
        //    parsePattern(line);
        //}
        //sr.Close();

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            parsePattern(line);
        }
    }

    public static List<PatternConfig> getAllPattern()
    {
        return patterns;
    }


    public static PatternConfig getPattern(int index)
    {
        return patterns[index];
    }


    public static void CheckEmety()
    {
        if (patterns.Count == 0)
            patterns.Add(new PatternConfig());
    }

    public static void AddPattern(PatternConfig value)
    {
        patterns.Add(value);
    }

    public static int GetLength()
    {
        return patterns.Count;
    }


    public static void WriteToFile()
    {
        FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(fs);
        foreach(PatternConfig config in patterns)
        {
            string str = ArrToString(config.left);
            str += ArrToString(config.middle);
            str += ArrToString(config.right);
            sw.WriteLine(str);
        }
        sw.Flush();
        sw.Close();
    }


    public static PatternConfig getRandomPattern()
    {
        int index = Random.Range(0, patterns.Count);
        return patterns[index];
    }


    public PatternConfig()
    {
        left = new int[patternLength];
        middle = new int[patternLength];
        right = new int[patternLength];
    }


    public PatternConfig(int[] left,int[] middle,int[] right)
    {
        this.left = left;
        this.middle = middle;
        this.right = right;
    }


    public PatternConfig(int[][] data)
    {
        this.left = data[0];
        this.middle = data[1];
        this.right = data[2];
    }


    public int GetPatternDetail(int index,int pos)
    {
        int[][] temp = new int[][]{ left,middle,right};
        return temp[index][pos];
    }


    public void SetPatternDetail(int index,int pos,int value)
    {
        int[][] temp = new int[][] { left, middle, right };
        temp[index][pos] = value;
    }


    private static void parsePattern(string line)
    {
        int[][] result = new int[patternCount][];
        int[] temp = new int[patternLength];

        for (int i = 0;i < patternCount * patternLength;i++)
        {
            int index = (int)(i / patternLength);

            if (i == index * patternLength)
                temp = new int[patternLength];

            if (i < line.Length)
                temp[i - index * patternLength] = int.Parse(line.Substring(i,1));
            else
                temp[i - index * patternLength] = 0;

            if (i == (index + 1) * patternLength - 1)
                result[index] = temp;
        }

        PatternConfig config = new PatternConfig(result);
        patterns.Add(config);
    }


    public static string ArrToString(int[] arr)
    {
        string result = "";
        for (int i = 0;i < arr.Length; i++)
            result += arr[i];
        return result;
    }
}
