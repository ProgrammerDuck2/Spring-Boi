using NaughtyAttributes;
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
    [SerializeField] InputActionProperty InputPosition;

    [Header("Contolls")]
    [SerializeField] InputActionProperty trigger;
    bool triggerActivated = false;
    [SerializeField] InputActionProperty grip;
    bool gripActivated = false;

    [Header("Other")]
    Vector3 controllerPosition;
    Vector3 initializedGrabPosition;
    Vector3 initializedPlayerPosition;

    bool grab = false;

    void Start()
    {
        trigger.action.started += toggleTrigger;
        trigger.action.canceled += toggleTrigger;
        grip.action.started += toggleGrip;
        grip.action.canceled += toggleGrip;

        playerBody = transform.parent.parent.parent.gameObject;
    }

    private void Update()
    {
        controllerPosition = InputPosition.action.ReadValue<Vector3>();

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

    [Button]
    public void initializedGrab()
    {
        S_Movement_TB movePlayer = playerBody.GetComponent<S_Movement_TB>();
        movePlayer.enabled = !movePlayer.enabled;

        initializedGrabPosition = controllerPosition;
        initializedPlayerPosition = playerBody.transform.position;
    }

    void Grab()
    {
        //Debug.Log("initialized pos = " + initializedGrabPosition + " currentPos = " + controllerPosition + " offset = " + (initializedGrabPosition - controllerPosition));

        Vector3 offset = initializedGrabPosition - controllerPosition;

        playerBody.transform.position = initializedPlayerPosition + offset * 3;
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
