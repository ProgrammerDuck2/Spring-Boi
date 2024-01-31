using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class S_Grab_TB : MonoBehaviour
{
    [Header("Player")]
    GameObject playerBody;

    [Header("Controller")]
    XRController controller;

    [Header("Contolls")]
    [SerializeField] InputActionProperty trigger;
    bool triggerActivated = false;
    [SerializeField] InputActionProperty grip;
    bool gripActivated = false;

    bool grab = false;

    void Start()
    {
        trigger.action.started += toggleTrigger;
        trigger.action.canceled += toggleTrigger;
        grip.action.started += toggleGrip;
        grip.action.canceled += toggleGrip;

        playerBody = transform.parent.parent.parent.gameObject;
        controller = GetComponent<XRController>();
    }

    private void Update()
    {
        if ((triggerActivated && gripActivated) != grab)
        {
            initializedGrab();
        }

        grab = triggerActivated && gripActivated ? true : false;

        if (grab)
        {
            Grab();
        }
    }
    void initializedGrab()
    {
        S_Movement_TB movePlayer = playerBody.GetComponent<S_Movement_TB>();
        movePlayer.enabled = !movePlayer.enabled;

        //controller.enabled = !controller.enabled;
    }
    void Grab()
    {
        
    }

    void toggleTrigger(InputAction.CallbackContext context)
    {
        triggerActivated = !triggerActivated;
    }
    void toggleGrip(InputAction.CallbackContext context)
    {
        gripActivated = !gripActivated;
    }
}
