
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_NPCInteractions_MA : MonoBehaviour
{
    public TMP_Text NPCText;
    [SerializeField] private List<string> speech = new List<string>();
    //private int currentSpeech = 0;
    private GameObject player;
    PlayerInput PlayerInput;
    bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["Interact"].started += StartSpeech;
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = player.transform;
        Vector3 direction = playerTransform.position - NPCText.transform.position;
        Quaternion rotation = Quaternion.LookRotation(-direction);
        NPCText.transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {    
        //        StartCoroutine(Speech());
        //    }
        //}
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    IEnumerator Speech()
    {
        for (int i = 0; i < speech.Count; i++) 
        {
            print(speech[i]);
            NPCText.text = speech[i];
            //currentSpeech++;
            yield return new WaitForSeconds(3);
        }
    }

    void StartSpeech(InputAction.CallbackContext context)
    {
        if (inTrigger)
        {
            StartCoroutine(Speech());
        }
    }
}