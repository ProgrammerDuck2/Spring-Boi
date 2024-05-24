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
    GameObject art;

    [SerializeField] private TMP_Text callPrint;
    public List<S_CallInformation_MA> whichCall = new List<S_CallInformation_MA>();

    bool startRinging;

    int callCount;
    S_CallCheck_MA call;

    [SerializeField] AudioClip ringtone;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        call = FindFirstObjectByType<S_CallCheck_MA>();
    }
    private void Update()
    {
        Debug.Log("call.call" + call.call);
        if (call.call)
        {
            Debug.Log("found call");
            callCount++;
            //PlayRingtone();
            StartCoroutine(CallSpeech());
            call.call = false;
            Debug.Log("made call" + call);
        }
    }

    IEnumerator CallSpeech()
    {
        Debug.Log("in callspeech");
        //whichCall[1].callText[1]
        for (int i = 0; i < whichCall[callCount].callText.Count; i++)
        {
            print(whichCall[callCount].callText[i]);
            callPrint.text = whichCall[callCount].callText[i];
            yield return new WaitForSeconds(5);
        }
    }

    void PlayRingtone()
    {
            AudioSource audio = GetComponent<AudioSource>();

            audio.clip = ringtone;
            audio.Play();
    }
}
