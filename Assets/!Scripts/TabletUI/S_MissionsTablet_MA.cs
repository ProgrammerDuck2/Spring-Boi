using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MissionsTablet_MA : MonoBehaviour
{
    List<S_QuestObject_TB> currentQuests
    {
        get 
        {
            return S_Quests_TB.activeQuests; 
        }
    }
    List<S_QuestObject_TB> completedQuests
    {
        get { return S_Quests_TB.completedQuests; }
    }

    List<S_QuestObject_TB> shownCurrentQuests
    {
        get 
        {
            List<S_QuestObject_TB> current = new List<S_QuestObject_TB>();
            Transform content = currentQuestsContent;

            for (int i = 0; i < content.childCount; i++)
            {
                current.Add(content.GetChild(i).GetComponent<S_QuestButton_TB>().quest);
            }

            return current;
        }
    }
    List<S_QuestObject_TB> shownCompletedQuests
    {
        get
        {
            List<S_QuestObject_TB> current = new List<S_QuestObject_TB>();
            Transform content = completedQuestsContent;

            for (int i = 0; i < content.childCount; i++)
            {
                current.Add(content.GetChild(i).GetComponent<S_QuestButton_TB>().quest);
            }

            return current;
        }
    }

    [SerializeField] GameObject questButtonPrefab;

    Transform currentQuestsContent
    {
        get { return transform.GetChild(3).GetChild(0).GetChild(0); }
    }
    Transform completedQuestsContent
    {
        get { return transform.GetChild(5).GetChild(0).GetChild(0); }
    }

    [SerializeField] S_QuestDescription_TB description;

    [Space(20)]
    [Expandable]
    public S_QuestObject_TB selectedQuest;

    
    void Start()
    {
        UpdateCurrentQuests();
    }

    private void Update()
    {
        if (!CompareLists(shownCurrentQuests, currentQuests))
        {
            UpdateCurrentQuests();
        }
        if (!CompareLists(shownCompletedQuests, completedQuests))
        {
            UpdateCompletedQuests();
        }
    }

    public void UpdateCurrentQuests()
    {
        int originalChildCound = currentQuestsContent.childCount;

        for (int i = 0; i < originalChildCound; i++)
        {
            DestroyImmediate(currentQuestsContent.GetChild(0).gameObject, true);
        }

        for (int i = 0; i < currentQuests.Count; i++)
        {
            GameObject current = Instantiate(questButtonPrefab, currentQuestsContent);
            current.GetComponent<S_QuestButton_TB>().quest = currentQuests[i];
        }
    }
    public void UpdateCompletedQuests()
    {
        int originalChildCound = completedQuestsContent.childCount;

        for (int i = 0; i < originalChildCound; i++)
        {
            DestroyImmediate(completedQuestsContent.GetChild(0).gameObject, true);
        }

        for (int i = 0; i < completedQuests.Count; i++)
        {
            GameObject current = Instantiate(questButtonPrefab, completedQuestsContent);
            current.GetComponent<S_QuestButton_TB>().quest = completedQuests[i];
        }
    }
    public void UpdateQuestDescription()
    {
        description.UpdateDescription();
    }

    bool CompareLists(List<S_QuestObject_TB> listOne, List<S_QuestObject_TB> listTwo)
    {
        if (listOne.Count != listTwo.Count)
        {
            return false;
        }
        for (int i = 0; i < listOne.Count; i++)
        {
            if (listOne[i] != listTwo[i]) return false;
        }

        return true;
    }
}
