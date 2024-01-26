using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class S_AnimateHand_TB : MonoBehaviour
{
    [SerializeField] InputActionProperty pinchAnimAction;
    [SerializeField] InputActionProperty gripAnimAction;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimAction.action.ReadValue<float>();
        float gripValue = gripAnimAction.action.ReadValue<float>();

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}
