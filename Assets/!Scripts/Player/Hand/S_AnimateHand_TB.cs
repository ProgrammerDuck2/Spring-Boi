using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_AnimateHand_TB : MonoBehaviour
{
    Animator animator;
    [HideInInspector] public S_Hand_TB hand;

    [SerializeField] int mouseButton;

    [SerializeField] bool useReference;

    [ShowIf("useReference")]
    [SerializeField] InputActionProperty grip;
    [ShowIf("useReference")]
    [SerializeField] InputActionProperty trigger;

    // Update is called once per frame
    void Update()
    {
        float pinchValue = 0;
        float gripValue = 0;

        if (S_Settings_TB.IsVRConnected)
        {
            if (!useReference)
            {
                pinchValue = hand.playerInput.actions["PinchValue"].ReadValue<float>();
                gripValue = hand.playerInput.actions["GripValue"].ReadValue<float>();
            }
            else
            {
                pinchValue = trigger.action.ReadValue<float>();
                gripValue = grip.action.ReadValue<float>();
            }
        }
        else
        {
            pinchValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
            gripValue = Input.GetMouseButton(mouseButton) ? 1 : 0;
        }

        if (hand.HandArtAnimation == null) return;

        hand.HandArtAnimation.SetFloat("Trigger", pinchValue);
        hand.HandArtAnimation.SetFloat("Grip", gripValue);
    }
}
