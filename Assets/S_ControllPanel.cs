using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ControllPanel : MonoBehaviour
{
    [SerializeField] S_BossTrigger trigger;
    [SerializeField] GameObject toSpawn;
    [SerializeField] Transform spawnLocation;
    bool started = false;

    void Update()
    {
        if (started) return;
        if(trigger.startedBoss)
        {
            InvokeRepeating(nameof(Spawn), 1, 10);
            started = true;
        }
    }

    void Spawn()
    {
        Instantiate(toSpawn, spawnLocation.position, spawnLocation.rotation);
    }
}
