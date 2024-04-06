using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_NPC_TB : MonoBehaviour
{
    public TMP_Text NPCText;
    [SerializeField] private List<string> speech = new List<string>();

    
    [SerializeField] string npcName;

    [HideInInspector] public GameObject player;
    PlayerInput PlayerInput;
    bool inTrigger = false;

    [SerializeField] bool givesQuest;
    [ShowIf("givesQuest")]
    [SerializeField] S_QuestObject_TB questToGive;

    [SerializeField] bool requiresQuest;
    [ShowIf("requiresQuest")]
    [SerializeField] S_QuestObject_TB requiredQuest;

    private void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["Interact"].started += StartSpeech;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inTrigger = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inTrigger = false;
    }

    public virtual void StartSpeech(InputAction.CallbackContext context)
    {
        if (inTrigger)
        {
            StartCoroutine(Speech());

            for (int i = 0; i < S_Quests_TB.activeQuests.Count; i++)
            {
                switch (S_Quests_TB.activeQuests[i].goal)
                {
                    case S_QuestEnums_TB.QuestGoal.TalkToNPC:
                        if(S_Quests_TB.activeQuests[i].NPCName == npcName)
                        {
                            print("SpringSteen");
                            S_Quests_TB.activeQuests[i].CompleteQuest();
                            S_Quests_TB.activeQuests.Remove(S_Quests_TB.activeQuests[i]);
                            
                        }
                        break;
                    default: 
                        break;
                }
            }
        }
    }

    public virtual IEnumerator Speech()
    {
        for (int i = 0; i < speech.Count; i++)
        {
            NPCText.text = speech[i];
            yield return new WaitForSeconds(textTime(speech[i]));
        }

        if(givesQuest)
        {
            S_Quests_TB.activeQuests.Add(questToGive);
            givesQuest = false;
        }
    }

    float textTime(string text)
    {
        char[] characters = text.ToCharArray();
        return characters.Length / 3;
    }
}
