using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MissionsTablet_MA : MonoBehaviour
{
    [SerializeField] List<S_QuestObject_TB> activeQuests = new List<S_QuestObject_TB>();
    [SerializeField] GameObject questButtonPrefab;

    Transform content
    {
        get { return transform.GetChild(1).GetChild(0).GetChild(0); }
    }

    [Space(20)]
    public S_QuestObject_TB selectedQuest;

    
    // Start is called before the first frame update
    void Start()
    {
        UpdateQuests();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    void UpdateQuests()
    {
        int originalChildCound = transform.childCount - 1;

        for (int i = 0; i < originalChildCound; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject, true);
        }

        for (int i = 0; i < activeQuests.Count; i++)
        {
            GameObject current = Instantiate(questButtonPrefab, content);
            current.GetComponent<S_QuestButton_TB>().quest = activeQuests[i];
        }
    }
}
