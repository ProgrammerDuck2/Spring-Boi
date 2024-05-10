using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CallTablet_MA : MonoBehaviour
{
    //call should always be page 0;

    private GameObject player;
    PlayerInput PlayerInput;

    [SerializeField] private TMP_Text callText;
    public List<string> callList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["TabletInteractions"].started += Interact;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Interact(InputAction.CallbackContext context)
    {
        StartCoroutine(CallSpeech());
    }

    IEnumerator CallSpeech()
    {
        for (int i = 0; i < callList.Count; i++)
        {
            callText.text = callList[i];
            yield return new WaitForSeconds(5);
            print(callList[i]);
        }
    }
}
