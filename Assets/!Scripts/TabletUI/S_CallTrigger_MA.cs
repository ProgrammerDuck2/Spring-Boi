using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CallTrigger_MA : MonoBehaviour
{
    [HideInInspector] public bool tabletTrigger = false;
    [SerializeField] private AudioClip ringtone;

    GameObject Ipad;

    // Start is called before the first frame update
    void Start()
    {
        Ipad = FindFirstObjectByType<S_Ipad_MA>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tabletTrigger = true;
            Debug.Log(tabletTrigger);
            PlayRingtone();
        }
    }
    void PlayRingtone()
    {
        AudioSource audio = Ipad.GetComponent<AudioSource>();

        audio.clip = ringtone;
        audio.Play();
    }
}
