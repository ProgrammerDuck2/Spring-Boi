using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(S_GameManager_TB))]
[CanEditMultipleObjects]
public class S_InspectorModifier_TB : Editor
{

    GUIStyle questStyle
    {
        get
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            return style;
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        S_GameManager_TB gameManger = (S_GameManager_TB)target;

        if(GUILayout.Button("Save"))
        {
            gameManger.Save();
        }

        EditorGUILayout.LabelField("Active Quests: ");

        foreach (var item in S_Quests_TB.activeQuests)
        {
            EditorGUILayout.LabelField(item.Name, questStyle);
        }

        EditorGUILayout.LabelField("Completed Quests: ");

        foreach (var item in S_Quests_TB.completedQuests)
        {
            EditorGUILayout.LabelField(item.Name, questStyle);
        }
    }
}
