using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BossTrigger : MonoBehaviour
{
    public bool startedBoss;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            startedBoss = true;
        }
    }

    [Button]
    public void startBoss()
    {
        startedBoss = true;
    }
}
