using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_GameManager_TB : MonoBehaviour
{
    public GameObject Player;
    public GameObject VRManager;
    public GameObject XRInteractionManager;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if(!FindFirstObjectByType<S_VrManager_TB>())
        {
            Instantiate(VRManager);
        }
        if (!FindFirstObjectByType<XRInteractionManager>())
        {
            Instantiate(XRInteractionManager);
        }
        if (!FindFirstObjectByType<S_Movement_TB>())
        {
            Debug.LogError("No player in Scene");
        }
    }


    void Update()
    {
        if(Debug.isDebugBuild)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
