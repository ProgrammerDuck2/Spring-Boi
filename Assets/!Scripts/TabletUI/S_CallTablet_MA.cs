using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_CallTablet_MA : MonoBehaviour
{
    private GameObject player;
    PlayerInput PlayerInput;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["TabletInteractions"].started += Interact;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<S_Ipad_MA>().isActive)
        {

        }
    }

    void Interact(InputAction.CallbackContext context)
    {

    }
}
