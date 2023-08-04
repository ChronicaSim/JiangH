using UnityEditor;
using UnityEngine;

namespace JiangH.Views.Controls
{
    [CustomEditor(typeof(TabControl))]
    public class TabControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("groups"), true);
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                var tabControl = target as TabControl;
                tabControl.Initialize();
            }
        }
    }
}
