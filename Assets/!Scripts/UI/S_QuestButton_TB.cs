using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_QuestButton_TB : S_Button_TBMA
{
    [Space(10)]
    [Expandable]
    public S_QuestObject_TB quest;

    S_MissionsTablet_MA missionsTablet
    {
        get { return transform.parent.parent.parent.parent.GetComponent<S_MissionsTablet_MA>(); } 
    }

    TMP_Text text;
    // Start is called before the first frame update
    void OnValidate()
    {
        if (quest == null) return;

        if (text == null)
            text = transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = quest.Name;
    }

    private void Start()
    {
        if (quest == null) return;

        if (text == null)
            text = transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = quest.Name; 
    }

    [Button]
    public void UpdateSelectedQuest()
    {
        missionsTablet.selectedQuest = quest;
        missionsTablet.UpdateQuestDescription();
    }
    public override void OnHoverEnter (ActionBasedController controller)
    {
        base.OnHoverEnter(controller);
        UpdateSelectedQuest();
    }
}
