using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CallTrigger_MA : MonoBehaviour
{
    [HideInInspector] public bool tabletTrigger = false;
    [SerializeField] private AudioClip ringtone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        tabletTrigger = true;
        Debug.Log(tabletTrigger);
        PlayRingtone();
    }
    void PlayRingtone()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.clip = ringtone;
        audio.Play();
    }
}
