using TMPro;
using UnityEngine;

public class S_QuestDescription_TB : MonoBehaviour
{
    TMP_Text title;
    TMP_Text description;
    S_MissionsTablet_MA missionsTablet;

    private void OnValidate()
    {
        UpdateDescription();
    }

    private void Start()
    {
        UpdateDescription();
    }

    //private void Update()
    //{
    //    UpdateDescription();
    //}

    public void UpdateDescription()
    {
        if (title == null)
            title = transform.GetChild(0).GetComponent<TMP_Text>();
        if (description == null)
            description = transform.GetChild(2).GetComponent<TMP_Text>();
        if (missionsTablet == null)
            missionsTablet = transform.parent.GetComponent<S_MissionsTablet_MA>();

        if (missionsTablet.selectedQuest == null) return;

        title.text = missionsTablet.selectedQuest.Name;
        description.text = missionsTablet.selectedQuest.Description;
    }
}
