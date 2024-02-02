using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class S_AnimateHand_TB : MonoBehaviour
{
    [SerializeField] InputActionProperty pinchAnimAction;
    [SerializeField] InputActionProperty gripAnimAction;
    Animator animator;

    [SerializeField] int mouseButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue;
        float gripValue;

        if (S_Settings_TB.IsVRConnected)
        {
            triggerValue = pinchAnimAction.action.ReadValue<float>();
            gripValue = gripAnimAction.action.ReadValue<float>();
        } 
        else
        {
            triggerValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
            gripValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
        }

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}
