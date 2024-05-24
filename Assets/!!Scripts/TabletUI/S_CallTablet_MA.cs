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

    [SerializeField] private TMP_Text callPrint;
    public List<S_CallInformation_MA> whichCall = new List<S_CallInformation_MA>();

    bool startRinging;

    int callCount;

    [SerializeField] AudioClip ringtone;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["TabletInteractions"].started += Interact;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "IpadCall")
        {

            callCount++;
            PlayRingtone();
        }
    }

    void Interact(InputAction.CallbackContext context)
    {
        StartCoroutine(CallSpeech());
    }

    IEnumerator CallSpeech()
    {
        //whichCall[1].callText[1]
        for (int i = 0; i < whichCall[callCount].callText.Count; i++)
        {
            callPrint.text = whichCall[callCount].callText[i];
            yield return new WaitForSeconds(5);
            print(whichCall[callCount].callText[i]);
        }
    }

    void PlayRingtone()
    {
            AudioSource audio = GetComponent<AudioSource>();

            audio.clip = ringtone;
            audio.Play();
    }
}
