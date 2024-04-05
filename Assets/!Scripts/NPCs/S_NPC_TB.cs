using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_NPC_TB : MonoBehaviour
{
    public TMP_Text NPCText;
    [SerializeField] private List<string> speech = new List<string>();

    [HideInInspector] public GameObject player;
    PlayerInput PlayerInput;
    bool inTrigger = false;

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


        }
    }

    public virtual IEnumerator Speech()
    {
        for (int i = 0; i < speech.Count; i++)
        {
            print(speech[i]);
            NPCText.text = speech[i];
            yield return new WaitForSeconds(3);
        }
    }
}
