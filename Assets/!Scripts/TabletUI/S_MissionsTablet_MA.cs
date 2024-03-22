using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MissionsTablet_MA : MonoBehaviour
{
    [SerializeField] List<S_QuestObject_TB> _activeQuests = S_Quests_TB.activeQuests;
    List<S_QuestObject_TB> activeQuests
    {
        get { return S_Quests_TB.activeQuests; }
        set { 
            S_Quests_TB.activeQuests = value;
            _activeQuests = value;
        }
    }

    [SerializeField] List<S_QuestObject_TB> _completedQuests = S_Quests_TB.completedQuests;
    List<S_QuestObject_TB> completedQuests
    {
        get { return S_Quests_TB.completedQuests; }
        set
        {
            S_Quests_TB.completedQuests = value;
            _completedQuests = value;
        }
    }

    [SerializeField] GameObject questButtonPrefab;

    Transform content
    {
        get { return transform.GetChild(1).GetChild(0).GetChild(0); }
    }

    [Space(20)]
    [Expandable]
    public S_QuestObject_TB selectedQuest;

    
    void Start()
    {
        UpdateQuests();
    }

    [Button]
    public void UpdateQuests()
    {
        activeQuests = _activeQuests;
        completedQuests = _completedQuests;

        int originalChildCound = content.childCount;

        for (int i = 0; i < originalChildCound; i++)
        {
            DestroyImmediate(content.GetChild(0).gameObject, true);
        }

        for (int i = 0; i < activeQuests.Count; i++)
        {
            GameObject current = Instantiate(questButtonPrefab, content);
            current.GetComponent<S_QuestButton_TB>().quest = activeQuests[i];
        }
    }
}
