#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameItem))]
public class GameItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GameItem item = (GameItem)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemButton"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemDescription"));

        if (item.itemType == ItemType.Composite)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Composite Components", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredItemA"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredItemB"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
