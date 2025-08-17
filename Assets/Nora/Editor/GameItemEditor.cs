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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("shouldDisappearOnPickup"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemDescription"));
        //Laura's Edit 
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemType2"));

        if (item.itemType == ItemType.actions)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Composite Components", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredItemA"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredItemB"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredItemC"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
