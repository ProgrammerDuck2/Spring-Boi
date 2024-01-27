using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Grab_TB : MonoBehaviour
{
    [Header("Contolls")]
    [SerializeField] InputActionProperty trigger;
    bool triggerActivated = false;
    [SerializeField] InputActionProperty grip;
    bool gripActivated = false;

    void Start()
    {
        trigger.action.started += toggleTrigger;
        trigger.action.canceled += toggleTrigger;
        grip.action.started += toggleGrip;
        grip.action.canceled += toggleGrip;
    }

    private void Update()
    {
        if(triggerActivated && gripActivated)
        {
            Grab();
        }
    }
    void Grab()
    {
        print("omg such grab");
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
