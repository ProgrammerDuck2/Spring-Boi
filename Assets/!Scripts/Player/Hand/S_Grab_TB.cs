using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class S_Grab_TB : S_Hand_TB
{
    //[Header("Controller")]
    //S_Grab_TB otherControllerGrab;

    //[Header("Other")]
    //[ShowNonSerializedField] Vector3 initializedGrabPosition;
    //[ShowNonSerializedField] Vector3 initializedPlayerPosition;

    //[HideInInspector] public bool holding = false;

    //public override void Start()
    //{
    //    base.Start();
    //    otherControllerGrab = otherController.GetComponent<S_Grab_TB>();
    //}

    //private void Update()
    //{
    //    if ((handInput.triggerActivated && handInput.gripActivated) != handInput.grabActivated && !handInput.grabActivated && Physics.CheckSphere(transform.position, S_Stats_MA.HandGrabRadius, notGrabable))
    //    {
    //        initializedGrab();
    //    }

    //    if ((handInput.triggerActivated && handInput.gripActivated) != handInput.grabActivated && handInput.grabActivated)
    //    {
    //        EndGrab();
    //    }

    //    if (holding)
    //    {
    //        Grab();
    //    }
    //}

    //public void initializedGrab()
    //{
    //    Debug.Log("Initialized grab");

    //    if (otherControllerGrab.holding)
    //    {
    //        otherControllerGrab.EndGrab();
    //    }

    //    S_Movement_TB movePlayer = playerMovement;
    //    movePlayer.enabled = false;

    //    initializedGrabPosition = handPostioning.controllerPosition;
    //    initializedPlayerPosition = player.transform.position;

    //    holding = true;

    //    handInput.hapticFeedback.TriggerHaptic(.1f, .1f, GetComponent<ActionBasedController>());
    //}
    //void Grab()
    //{
    //    Debug.Log("initialized pos = " + initializedGrabPosition + " currentPos = " + controllerPosition + " offset = " + (initializedGrabPosition - controllerPosition));

    //    Vector3 offset = initializedGrabPosition - transform.localPosition;

    //    player.transform.position = initializedPlayerPosition + offset;
    //    transform.localPosition -= offset;
    //}
    //void EndGrab()
    //{
    //    Debug.Log("Ended grab");

    //    if (!otherControllerGrab.holding)
    //    {
    //        S_Movement_TB movePlayer = playerMovement;
    //        movePlayer.enabled = true;
    //    }

    //    holding = false;
    //}
    //private void OnDrawGizmos()
    //{
    //    if (DebugMode)
    //    {
    //        Gizmos.color = new Color(0, 1, 0, .2f);
    //        Gizmos.DrawSphere(transform.position, S_Stats_MA.HandGrabRadius);
    //    }
    //}
}
