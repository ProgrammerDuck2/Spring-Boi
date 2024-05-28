using NaughtyAttributes;
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
    [Expandable]
    public List<S_CallInformation_MA> whichCall = new List<S_CallInformation_MA>();

    public bool startRinging;

    int counter;
    S_CallCheck_MA call;

    AudioSource audioSource;

    [SerializeField] AudioClip ringtone;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        call = FindFirstObjectByType<S_CallCheck_MA>();
        AudioSource äudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Debug.Log("call.call is" + call.call);
        if (call.call)
        {
            Debug.Log("found call");
            StartCoroutine(CallSpeech());
            call.call = false;
            Debug.Log("made call" + call);
        }
    }

    IEnumerator CallSpeech()
    {
        yield return StartCoroutine(PlayRingtone());

        Debug.Log(whichCall[counter].callText.Count);
        //whichCall[1].callText[1]
        for (int i = 0; i < whichCall[counter].callText.Count; i++)
        {
            Debug.Log(whichCall[counter].callText[i]);
            callPrint.text = whichCall[counter].callText[i];
            audioSource.clip = whichCall[counter].audioClips[i];
            audioSource.Play();

            yield return new WaitForSeconds(5);
        }
        counter++;
    }

    IEnumerator PlayRingtone()
    {

            //audio.clip = ringtone;
            //audio.Play();
        yield return new WaitForSeconds(5);
    }
}
