using System;
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

    public static int patternLength = 10;
    public static int patternCount = 3;

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
            string str = ArrToString(config.left) + ",";
            str += ArrToString(config.middle) + ",";
            str += ArrToString(config.right);
            sw.WriteLine(str);
        }
        sw.Flush();
        sw.Close();
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
        int[][] temp = new int[][] {};
        char[] chars = line.ToCharArray();
        
        for(int i = 0;i < line.Length;i++)
        {
            int index = (int)(i / patternLength);
            temp[index][i - index * patternLength] = (int)chars[i];
        }

        PatternConfig config = new PatternConfig(temp);
        patterns.Add(config);
    }


    private static string ArrToString(int[] arr)
    {
        string result = "";
        for (int i = 0;i < arr.Length; i++)
        {
            result += arr[i];
            result += ",";
        }
        result = result.Substring(0, result.Length - 1);
        return result;
    }
}
