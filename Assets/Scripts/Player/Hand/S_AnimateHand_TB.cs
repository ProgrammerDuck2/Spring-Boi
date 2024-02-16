using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class S_AnimateHand_TB : MonoBehaviour
{
    Animator animator;
    [SerializeField] PlayerInput input;

    [SerializeField] int mouseButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        input.SwitchCurrentControlScheme("XR");
    }

    // Update is called once per frame
    void Update()
    {
        float pinchValue = 0;
        float gripValue = 0;

        if (S_Settings_TB.IsVRConnected)
        {
            //Debug.Log(input.currentControlScheme);

            if (input.currentControlScheme == "XR")
            {
                pinchValue = input.actions["PinchValue"].ReadValue<float>();
                gripValue = input.actions["GripValue"].ReadValue<float>();
            }
            else
            {
                Debug.LogError(name + " controlscheme is not XR");
            }
        }
        else
        {
            pinchValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
            gripValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
        }

        animator.SetFloat("Trigger", pinchValue);
        animator.SetFloat("Grip", gripValue);
    }
}
