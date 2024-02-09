using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(S_Hand_TB))]
public class S_Grab_TB : MonoBehaviour
{
    S_Hand_TB hand;

    [Header("Player")]
    GameObject playerBody;

    [Header("Stats")]
    [SerializeField] float radius;

    [Header("Controller")]
    S_Grab_TB otherControllerGrab;

    [Header("Other")]
    [ShowNonSerializedField] Vector3 initializedGrabPosition;
    [ShowNonSerializedField] Vector3 initializedPlayerPosition;

    public LayerMask grabable;
    [HideInInspector] public bool holding = false;

    void Start()
    {
        hand = GetComponent<S_Hand_TB>();

        playerBody = transform.parent.parent.parent.gameObject;

        otherControllerGrab = hand.otherController.GetComponent<S_Grab_TB>();
    }

    private void Update()
    {
        if ((hand.triggerActivated && hand.gripActivated) != hand.grabActivated && !hand.grabActivated && Physics.CheckSphere(transform.position, radius, grabable))
        {
            initializedGrab();
        }

        if((hand.triggerActivated && hand.gripActivated) != hand.grabActivated && hand.grabActivated)
        {
            EndGrab();
        }

        if (holding)
        {
            Grab();
        }
    }

    public void initializedGrab()
    {
        Debug.Log("Initialized grab");

        if (otherControllerGrab.holding)
        {
            otherControllerGrab.EndGrab();
        }

        S_Movement_TB movePlayer = playerBody.GetComponent<S_Movement_TB>();
        movePlayer.enabled = false;

        initializedGrabPosition = hand.controllerPosition;
        initializedPlayerPosition = playerBody.transform.position;

        holding = true;
    }
    void Grab()
    {
        //Debug.Log("initialized pos = " + initializedGrabPosition + " currentPos = " + controllerPosition + " offset = " + (initializedGrabPosition - controllerPosition));

        Vector3 offset = initializedGrabPosition - hand.controllerPosition;

        playerBody.transform.position = initializedPlayerPosition + offset;
        transform.localPosition -= offset;
    }
    void EndGrab()
    {
        Debug.Log("Ended grab");

        if(!otherControllerGrab.holding)
        {
            S_Movement_TB movePlayer = playerBody.GetComponent<S_Movement_TB>();
            movePlayer.enabled = true;
        }

        holding = false;
    }
}
