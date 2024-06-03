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


    [field: Header("Bouncepad SFX")]

    [field: SerializeField] public EventReference bouncePad { get; private set; }


    [field: Header("Punch SFX")]

    [field: SerializeField] public EventReference punch { get; private set; }


    [field: Header("Throw arm SFX")]

    [field: SerializeField] public EventReference throwarm { get; private set; }


    [field: Header("Arm grab SFX")]

    [field: SerializeField] public EventReference armgrab { get; private set; }


    [field: Header("SpringDadDialogueSpringtown")]

    [field: SerializeField] public EventReference SpringdadDialogueSpringtown { get; private set; }


    [field: Header("SpringetteDialogueSpringtown")]

    [field: SerializeField] public EventReference SpringetteDialogueSpringtown { get; private set; }


    [field: Header("SpringsteenDialogueSpringtown")]

    [field: SerializeField] public EventReference SpringsteenDialogueSpringtown { get; private set; }


    [field: Header("SpringDadDialoguePistonrow")]

    [field: SerializeField] public EventReference SpringdadDialoguePistonrow { get; private set; }


    [field: Header("SpringDadDialogueMagneticfields")]

    [field: SerializeField] public EventReference SpringdadDialogueMagneticfields { get; private set; }


    [field: Header("SpringDadDialogueBatteryavenue")]

    [field: SerializeField] public EventReference SpringdadDialogueBatteryavenue { get; private set; }


    [field: Header("SpringDadDialogueGearhaven")]

    [field: SerializeField] public EventReference SpringdadDialogueGearhaven { get; private set; }



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
