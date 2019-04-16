using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(RayButton), true)]
[CanEditMultipleObjects]
public class RayButtonEditor : ButtonEditor  
{

    private SerializedProperty currentMode;
    private SerializedProperty childText;
    protected override void OnEnable()
    {
        base.OnEnable();
        currentMode = serializedObject.FindProperty("currentMode");
        childText = serializedObject.FindProperty("childText");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();

        EditorGUILayout.PropertyField(currentMode);
        EditorGUILayout.PropertyField(childText);

        serializedObject.ApplyModifiedProperties();

    }

}