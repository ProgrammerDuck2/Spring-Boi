using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class S_FMODEvents_HA : MonoBehaviour
{

    [field: Header("Ambience")]

    [field: SerializeField] public EventReference ambience { get; private set; }


    [field: Header("Music")]

    [field: SerializeField] public EventReference Music { get; private set; }


    [field: Header("OilCan SFX")]

    [field: SerializeField] public EventReference oilcanCollected { get; private set; }

    [field: Header("Punch SFX")]

    [field: SerializeField] public EventReference punch { get; private set; }

    public static S_FMODEvents_HA instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Found more than one instance of S_FMODEvents_HA instance in the scene.");
        }
        instance = this;
    }
}
