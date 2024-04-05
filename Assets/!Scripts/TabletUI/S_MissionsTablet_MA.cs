using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MissionsTablet_MA : MonoBehaviour
{
    [SerializeField] GameObject questButtonPrefab;

    Transform content
    {
        get { return transform.GetChild(1).GetChild(0).GetChild(0); }
    }

    [Space(20)]
    public S_QuestObject_TB selectedQuest;

    
    void Start()
    {
        UpdateQuests();
    }

    [Button]
    public void UpdateQuests()
    {
        int originalChildCound = content.childCount;

        for (int i = 0; i < originalChildCound; i++)
        {
            DestroyImmediate(content.GetChild(0).gameObject, true);
        }

        for (int i = 0; i < S_Quests_TB.activeQuests.Count; i++)
        {
            GameObject current = Instantiate(questButtonPrefab, content);
            current.GetComponent<S_QuestButton_TB>().quest = S_Quests_TB.activeQuests[i];
        }
    }
}
