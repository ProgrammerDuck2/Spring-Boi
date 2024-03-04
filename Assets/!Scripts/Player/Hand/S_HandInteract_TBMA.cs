using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_HandInteract_TBMA : MonoBehaviour
{
    [SerializeField] LayerMask Interactable;
    [HideInInspector] RaycastHit raycast;

    S_Interactable_TBMA currentClickButton;
    S_Interactable_TBMA currentHoverButton;
    
    S_Hand_TB hand;

    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out raycast, 2, Interactable);

        if (hand.DebugMode)
        {
            Debug.DrawLine(transform.position, raycast.point, Color.red);
        }

        if (raycast.collider != null)
        {
            Debug.Log("Hover");
            if (raycast.collider.TryGetComponent<S_Interactable_TBMA>(out S_Interactable_TBMA button))
            {
                button.OnHover();

                if(currentHoverButton == null)
                {
                    hand.Player.GetComponent<S_HapticFeedback_TB>().TriggerHaptic(.1f, .1f, GetComponent<ActionBasedController>());
                    currentHoverButton = button;
                }

                if(button != currentClickButton)
                    button.ButtonImage.color = button.HighlightColor;

            } else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
    }

    public void Click(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        if (raycast.collider != null)
        {
            if (raycast.collider.TryGetComponent<S_Interactable_TBMA>(out S_Interactable_TBMA button))
            {
                button.OnClickEnter();
                hand.Player.GetComponent<S_HapticFeedback_TB>().TriggerHaptic(.1f, .1f, GetComponent<ActionBasedController>());
                button.ButtonImage.color = button.PressedColor;
                currentClickButton = button;
            }
            else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
    }
    public void UnClick(InputAction.CallbackContext context)
    {
        currentClickButton.OnClickExit();
        currentClickButton.ButtonImage.color = currentClickButton.ButtonColor;
        currentClickButton = null;
    }
    private void OnValidate()
    {
        hand = GetComponent<S_Hand_TB>();
    }
    private void OnDrawGizmos()
    {
        if (hand.DebugMode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }
}
