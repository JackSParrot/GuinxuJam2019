using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generator))]
public class generatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Generator myScript = (Generator)target;
        if (GUILayout.Button("Prepare"))
        {
            myScript.Prepare();
        }
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            myScript.Clear();
        }
        if (GUILayout.Button("FullClear"))
        {
            myScript.FullClear();
        }
    }
}
