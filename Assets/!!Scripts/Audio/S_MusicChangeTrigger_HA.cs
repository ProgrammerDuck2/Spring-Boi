using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MusicChangeTrigger_HA : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private S_MusicArea_HA area;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) // Specify the tag here
        {
            S_AudioManager_HA.instance.SetMusicArea(area);
        }
    }
}
