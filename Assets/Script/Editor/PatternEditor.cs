

using UnityEditor;
using UnityEngine;

public class PatternEditor : EditorWindow
{

    [MenuItem("Window/PatternEditor")]
    static void Init()
    {
        PatternEditor editor = EditorWindow.GetWindow<PatternEditor>() as PatternEditor;
        //editor.Show();
    }

    void OnGUI()
    {

        
    }
}
