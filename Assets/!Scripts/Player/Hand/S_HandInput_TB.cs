using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_HandInput_TB : S_Hand_TB
{
    [Header("Input")]
    public bool useReference;
    [ShowIf("useReference")]
    public InputActionProperty pos;
    [ShowIf("useReference")]
    public InputActionProperty rot;
    [ShowIf("useReference")]
    public InputActionProperty grip;
    [ShowIf("useReference")]
    public InputActionProperty trigger;

    [Header("Contolls")]
    [ShowIf("DebugMode")]
    public bool triggerActivated = false;
    [ShowIf("DebugMode")]
    public bool gripActivated = false;
    [ShowIf("DebugMode")]
    public bool grabActivated = false;
    // Start is called before the first frame update
    public override void Start()
    {
        if (!useReference)
        {
            playerInput.actions["Pinch"].started += toggleTrigger;
            playerInput.actions["Pinch"].canceled += toggleTrigger;
            playerInput.actions["Grip"].started += toggleGrip;
            playerInput.actions["Grip"].canceled += toggleGrip;

            playerInput.actions["Launch"].started += launchArms.LaunchArm;
            playerInput.actions["Launch"].canceled += launchArms.PullArm;

            playerInput.actions["Interact"].started += interact.ClickEnter;
            playerInput.actions["Interact"].performed += interact.Click;
            playerInput.actions["Interact"].canceled += interact.ClickExit;
        }
        else
        {
            trigger.action.started += toggleTrigger;
            trigger.action.canceled += toggleTrigger;
            grip.action.started += toggleGrip;
            grip.action.canceled += toggleGrip;

            trigger.action.started += interact.ClickEnter;
            trigger.action.performed += interact.Click;
            trigger.action.canceled += interact.ClickExit;
        }
    }

    private void LateUpdate()
    {
        //transform.position -= PlayerMovement.IRLPosOffset;
        grabActivated = triggerActivated && gripActivated ? true : false;
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
