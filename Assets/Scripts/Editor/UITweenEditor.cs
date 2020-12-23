using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UITween))]
public class UITweenEditor : Editor
{
    SerializedProperty move;
    SerializedProperty scale;
    SerializedProperty atFirstOnStart;
    SerializedProperty showHide;
    SerializedProperty showOnStart;
    SerializedProperty times;
    SerializedProperty poses;
    SerializedProperty sizes;

    bool foldout;

    private void OnEnable()
    {
        move = serializedObject.FindProperty("move");
        scale = serializedObject.FindProperty("scale");
        atFirstOnStart = serializedObject.FindProperty("atFirstOnStart");
        showHide = serializedObject.FindProperty("showHide");
        showOnStart = serializedObject.FindProperty("showOnStart");
        times = serializedObject.FindProperty("times");
        poses = serializedObject.FindProperty("poses");
        sizes = serializedObject.FindProperty("sizes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 이동, 사이즈 조절
        EditorGUILayout.BeginVertical("box");
        EditorGUI.BeginDisabledGroup(poses.arraySize != 0 || sizes.arraySize != 0);
        EditorGUILayout.PropertyField(move, new GUIContent("Move"));
        EditorGUILayout.PropertyField(scale, new GUIContent("Scale"));
        EditorGUI.EndDisabledGroup();

        if (move.boolValue || scale.boolValue)
        {            
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, new GUIContent("Pos / Size"));
            if(foldout)
            {
                EditorGUI.indentLevel++;

                // 저장된 이동 좌표와 사이즈
                int count = move.boolValue ? poses.arraySize : sizes.arraySize;
                for (int i = 0; i < count; i++)
                {
                    EditorGUILayout.BeginVertical("box");
                    if (move.boolValue) EditorGUILayout.PropertyField(poses.GetArrayElementAtIndex(i), new GUIContent("Pos"));
                    if (scale.boolValue) EditorGUILayout.PropertyField(sizes.GetArrayElementAtIndex(i), new GUIContent("Size"));
                    if ((poses.arraySize > 1 || sizes.arraySize > 1) && i > 0)
                        EditorGUILayout.PropertyField(times.GetArrayElementAtIndex(i - 1), new GUIContent("Tween Time"));
                    EditorGUILayout.EndVertical();
                }

                GUILayout.Space(10);

                // 현재 위치와 사이즈를 리스트에 추가
                if (GUILayout.Button("Add Current Pos / Size"))
                {
                    if (move.boolValue)
                    {
                        poses.InsertArrayElementAtIndex(poses.arraySize);
                        SerializedProperty newPos = poses.GetArrayElementAtIndex(poses.arraySize - 1);
                        newPos.vector2Value = ((UITween)target).transform.position;
                    }
                    if (scale.boolValue)
                    {
                        sizes.InsertArrayElementAtIndex(sizes.arraySize);
                        SerializedProperty newSize = sizes.GetArrayElementAtIndex(sizes.arraySize - 1);
                        newSize.vector2Value = ((RectTransform)((UITween)target).transform).sizeDelta;
                    }
                    if (poses.arraySize > 1 || sizes.arraySize > 1) times.InsertArrayElementAtIndex(times.arraySize);
                }

                // 리스트 초기화
                if (GUILayout.Button("Clear Poses / Sizes"))
                {
                    if (move.boolValue) poses.ClearArray();
                    if (scale.boolValue) sizes.ClearArray();
                }

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        EditorGUILayout.EndVertical();

        // 활성화 & 비활성화
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(showHide, new GUIContent("Show & Hide"));
        EditorGUILayout.EndVertical();

        // 최초 상태
        EditorGUILayout.BeginVertical("box");
        if (move.boolValue || showHide.boolValue)
        {
            EditorGUILayout.LabelField(new GUIContent("On Start"));

            EditorGUI.indentLevel++;

            // 최초에 시작 위치에 위치할지, 마지막 위치에 위치할지
            if (move.boolValue) EditorGUILayout.PropertyField(atFirstOnStart, new GUIContent("At First On Start"));
            // 최초에 활성화할지, 비활성화할지
            if (showHide.boolValue) EditorGUILayout.PropertyField(showOnStart, new GUIContent("Show on Start"));

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy)]
    static void DrawGizmo(UITween tween, GizmoType gizmoType)
    {
        if (!tween.move && !tween.scale) return;

        if (tween.move && tween.scale)
        {
            for (int i = 0; i < tween.poses.Length - 1; i++)
                Gizmos.DrawLine(tween.poses[i], tween.poses[i + 1]);

            for (int i = 0; i < tween.poses.Length; i++)
            {
                if (i == tween.poses.Length - 1) Gizmos.color = Color.red;
                Gizmos.DrawWireCube(tween.poses[i], tween.sizes[i]);
            }
        }
        else if (tween.move)
        {
            for (int i = 0; i < tween.poses.Length - 1; i++)
                Gizmos.DrawLine(tween.poses[i], tween.poses[i + 1]);

            for (int i = 0; i < tween.poses.Length; i++)
            {
                if (i == tween.poses.Length - 1) Gizmos.color = Color.red;
                Gizmos.DrawWireCube(tween.poses[i], Vector2.one * 80);
            }
        }
        else if (tween.scale)
        {
            for (int i = 0; i < tween.sizes.Length; i++)
            {
                if (i == tween.sizes.Length - 1) Gizmos.color = Color.red;
                Gizmos.DrawWireCube(tween.transform.position, tween.sizes[i]);
            }
        }
    }
}
