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
    [SerializeField] InputActionProperty inputPosition;
    [SerializeField] GameObject otherController;
    S_Grab_TB otherControllerGrab;

    [Header("Contolls")]
    [SerializeField] InputActionProperty trigger;
    bool triggerActivated = false;
    [SerializeField] InputActionProperty grip;
    bool gripActivated = false;

    [Header("Other")]
    Vector3 controllerPosition;
    Vector3 initializedGrabPosition;
    Vector3 initializedPlayerPosition;

    public LayerMask grabable;

    public bool grab = false;

    void Start()
    {
        trigger.action.started += toggleTrigger;
        trigger.action.canceled += toggleTrigger;
        grip.action.started += toggleGrip;
        grip.action.canceled += toggleGrip;

        playerBody = transform.parent.parent.parent.gameObject;

        otherControllerGrab = otherController.GetComponent<S_Grab_TB>();
    }

    private void Update()
    {
        controllerPosition = inputPosition.action.ReadValue<Vector3>();

        if ((triggerActivated && gripActivated) != grab)
        {
            initializedGrab();
        }

        grab = triggerActivated && gripActivated && Physics.CheckSphere(transform.position, .5f, grabable) ? true : false;

        if (grab)
        {
            Grab();
        }
    }

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

        playerBody.transform.position = initializedPlayerPosition + offset * 2;
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
