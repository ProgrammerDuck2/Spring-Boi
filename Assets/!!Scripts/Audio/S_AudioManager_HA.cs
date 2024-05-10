using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class S_AudioManager_HA : MonoBehaviour
{

    public static S_AudioManager_HA instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of AudioManager found!");           
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
