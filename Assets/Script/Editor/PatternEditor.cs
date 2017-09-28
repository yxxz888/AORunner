

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternEditor : EditorWindow
{
    private int curConfigIndex = 0;

    [MenuItem("PatternEditor/PatternEditor")]
    static void Init()
    {
        PatternEditor editor = EditorWindow.GetWindow<PatternEditor>() as PatternEditor;
        editor.Show();

        PatternConfig.CheckEmety();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(100,20,130,20), "Add New Pattern"))
        {
            PatternConfig config = new PatternConfig();
            PatternConfig.AddPattern(config);
            curConfigIndex = PatternConfig.GetLength() - 1;
        }


        initConfigs();

        initMark();

        if(GUI.Button(new Rect(100, 370, 100, 20), "Apply"))
        {
            PatternConfig.WriteToFile();
        }
    }


    private void initConfigs()
    {
        int Length = PatternConfig.GetLength();
        int[] values = new int[Length];
        string[] names = new string[Length];
        for(int i = 0;i < Length; i++)
        {
            values[i] = i;
            names[i] = i + "";
        }

        curConfigIndex = EditorGUI.IntPopup(new Rect(20, 20, 50, 20), curConfigIndex, names, values);
        PatternConfig pattern = PatternConfig.getPattern(curConfigIndex);

        EditorGUILayout.Space();

        EditorGUI.LabelField(new Rect(35, 50, 100, 20), "Pattern Details");

        int startX = 20;
        int StartY = 70;
        int SpaceX = 10;
        int SpaceY = 10;
        int Width = 30;
        int Height = 20;

        for(int i = 0;i < PatternConfig.patternCount;i++)
        {
            for(int j = 0;j < PatternConfig.patternLength;j++)
            {
                int value = EditorGUI.IntField(new Rect(startX + i * (SpaceX + Width),StartY + j * (SpaceY + Height),Width,Height), pattern.GetPatternDetail(i,j));
                pattern.SetPatternDetail(i, j, value);
            }
        }
    }


    private void initMark()
    {
        EditorGUI.LabelField(new Rect(150, 70, 100, 20), "0 = Empty");
        EditorGUI.LabelField(new Rect(150, 90, 100, 20), "1 = MoneyBag");
        EditorGUI.LabelField(new Rect(150, 110, 100, 20), "2 = ModelBoard");
        EditorGUI.LabelField(new Rect(150, 130, 100, 20), "3 = ModelStone");
    }
}
