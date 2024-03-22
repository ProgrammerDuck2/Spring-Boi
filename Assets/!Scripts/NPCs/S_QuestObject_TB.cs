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
}
