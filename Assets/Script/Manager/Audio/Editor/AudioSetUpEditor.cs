using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioSetUp))]
public class AudioSetUpEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AudioSetUp setUp = (AudioSetUp)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Run StartUp"))
        {
            setUp.RunSetup();
        }
    }
}
