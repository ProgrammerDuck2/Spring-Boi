using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(S_Hand_TB))]
public class S_Grab_TB : MonoBehaviour
{
    S_Hand_TB hand;

    [SerializeField] bool DebugMode;

    [Header("Player")]
    GameObject playerBody;

    [Header("Controller")]
    S_Grab_TB otherControllerGrab;

    [Header("Other")]
    [ShowNonSerializedField] Vector3 initializedGrabPosition;
    [ShowNonSerializedField] Vector3 initializedPlayerPosition;

    [HideInInspector] public bool holding = false;

    void Start()
    {
        hand = GetComponent<S_Hand_TB>();

        playerBody = transform.parent.parent.parent.gameObject;

        otherControllerGrab = hand.OtherController.GetComponent<S_Grab_TB>();
    }

    private void Update()
    {
        if ((hand.TriggerActivated && hand.GripActivated) != hand.GrabActivated && !hand.GrabActivated && Physics.CheckSphere(transform.position, S_Stats_MA.HandGrabRadius, hand.grabable))
        {
            initializedGrab();
        }

        if((hand.TriggerActivated && hand.GripActivated) != hand.GrabActivated && hand.GrabActivated)
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

        initializedGrabPosition = hand.ControllerPosition;
        initializedPlayerPosition = playerBody.transform.position;

        holding = true;

        hand.HapticFeedback.TriggerHaptic(1, GetComponent<ActionBasedController>());
    }
    void Grab()
    {
        //Debug.Log("initialized pos = " + initializedGrabPosition + " currentPos = " + controllerPosition + " offset = " + (initializedGrabPosition - controllerPosition));

        Vector3 offset = initializedGrabPosition - hand.ControllerPosition;

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
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = new Color(0, 1, 0, .2f);
            Gizmos.DrawSphere(transform.position, S_Stats_MA.HandGrabRadius);
        }
    }
}
