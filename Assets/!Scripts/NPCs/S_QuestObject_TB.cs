using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "NPC/Quest", order = 1)]
public class S_QuestObject_TB : ScriptableObject
{
    public string Name;
    [ResizableTextArea]
    public string Description;

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
    public S_NPC_TB NPC;
}