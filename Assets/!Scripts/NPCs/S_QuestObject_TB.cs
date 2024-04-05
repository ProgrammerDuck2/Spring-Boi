using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "NPC/Quest", order = 1)]
[System.Serializable]
public class S_QuestObject_TB : ScriptableObject
{
    public string Name;
    [ResizableTextArea]
    public string Description;

    [InfoBox("give the quest a UNIQUE ID")]
    [ShowNonSerializedField][HideInInspector] public int ID;

    [Space(20)]

    public S_QuestEnums_TB.QuestType type;

    [HorizontalLine(color: EColor.Violet)]

    public S_QuestEnums_TB.QuestGoal goal;

    [Space(5)]
    [ShowIf("goal", S_QuestEnums_TB.QuestGoal.GoToLocation)]
    public Vector3 Location;

    [Space(5)]
    [ShowIf("goal", S_QuestEnums_TB.QuestGoal.KillEnemies)]
    public int Amount;

    [Space(5)]
    [ShowIf("goal", S_QuestEnums_TB.QuestGoal.TalkToNPC)]
    public string NPCName;

    [Button]
    public void CompleteQuest()
    {
        S_Quests_TB.completedQuests.Add(this);
    }
    
    [Button]
    public void GenerateID()
    {
        ID = Random.Range(0, 9999);
    }

    
}
