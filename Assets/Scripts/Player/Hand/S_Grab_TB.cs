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
    Vector3 controllerPosition;
    Vector3 initializedGrabPosition;
    Vector3 initializedPlayerPosition;

    [HideInInspector] public LayerMask grabable = ;
    [HideInInspector] public bool grab = false;

    void Start()
    {
        hand = GetComponent<S_Hand_TB>();

        playerBody = transform.parent.parent.parent.gameObject;

        otherControllerGrab = hand.otherController.GetComponent<S_Grab_TB>();
    }

    private void Update()
    {
        if ((hand.triggerActivated && hand.gripActivated) != grab && Physics.CheckSphere(transform.position, radius, grabable))
        {
            initializedGrab();
        }

        grab = hand.triggerActivated && hand.gripActivated ? true : false;

        print(Physics.CheckSphere(transform.position, radius, grabable));

        if (grab && Physics.CheckSphere(transform.position, radius, grabable))
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
}
