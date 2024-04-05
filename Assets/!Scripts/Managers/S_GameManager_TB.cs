using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_GameManager_TB : MonoBehaviour
{
    public GameObject player;
    public GameObject vrManager;
    public GameObject xrInteractionManager;

    GameObject currentPlayer;

    [Scene]
    [ShowNonSerializedField][HideInInspector] int level;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if(!FindFirstObjectByType<S_VrManager_TB>())
        {
            Instantiate(vrManager);
        }
        if (!FindFirstObjectByType<XRInteractionManager>())
        {
            Instantiate(xrInteractionManager);
        }
        if (!FindFirstObjectByType<S_Player_TB>())
        {
            Debug.LogError("No player in Scene");
        } else
        {
            currentPlayer = FindFirstObjectByType<S_Player_TB>().gameObject;
        }
    }

    private void Start()
    {
        S_ProgressData_TB data = S_SaveSystem_TB.Load();

        if (data == null) return;

        currentPlayer.transform.position = new Vector3(data.playerPositionX, data.playerPositionY, data.playerPositionZ);
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
