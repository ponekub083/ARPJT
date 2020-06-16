using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(DrawManager))]
public class EditorTest : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawManager manager = (DrawManager)target;
        if (GUILayout.Button("Generate Points"))
        {
            manager.StartGenerated();
        }
        if (GUILayout.Button("Clear All Points"))
        {
            manager.ClearAll();
        }
    }
}

#endif