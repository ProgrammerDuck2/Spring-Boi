using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class S_AnimateHand_TB : MonoBehaviour, S_VRDependant_TB
{
    Animator animator;
    public PlayerInput input;

    [SerializeField] int mouseButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        IfElseVR();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue;
        float gripValue;

        if (S_Settings_TB.IsVRConnected)
        {
            triggerValue = input.actions["PinchValue"].ReadValue<float>();
            gripValue = input.actions["GripValue"].ReadValue<float>();
        } 
        else
        {
            triggerValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
            gripValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
        }

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }

    public void IfElseVR()
    {
        if (input == null && S_Settings_TB.IsVRConnected)
        {
            input = transform.parent.parent.GetComponent<PlayerInput>();
            print("anim");
        }
        else
        {
            input = null;
        }
    }
}
