using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(UITweenPlanner))]
public class UITweenPlannerEditor : Editor
{
    SerializedProperty uiTweenPlans;
    SerializedProperty uiTween;
    SerializedProperty wait;
    SerializedProperty reverse;

    private void OnEnable()
    {
        uiTweenPlans = serializedObject.FindProperty("uiTweenPlans");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Tweens");

        EditorGUI.indentLevel++;

        for (int i = 0; i < uiTweenPlans.arraySize; i++)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField(i.ToString());

            var plan = uiTweenPlans.GetArrayElementAtIndex(i);
            uiTween = plan.FindPropertyRelative("uiTween");
            wait = plan.FindPropertyRelative("wait");
            reverse = plan.FindPropertyRelative("reverse");

            EditorGUILayout.PropertyField(uiTween, new GUIContent("UI Tween"));
            if(uiTween.objectReferenceValue != null)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(wait, new GUIContent("Wait till end"));
                EditorGUILayout.PropertyField(reverse, new GUIContent("Reverse (Hide)"));

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}
