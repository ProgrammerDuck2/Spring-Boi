using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class S_FMODEvents_HA : MonoBehaviour
{

    [field: Header("OilCan SFX")]

    [field: SerializeField] public EventReference oilcanCollected { get; private set; }

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
