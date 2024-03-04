using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_HandInteract_TBMA : MonoBehaviour
{
    [SerializeField] LayerMask Interactable;
    [HideInInspector] RaycastHit raycast;

    S_ButtonInterface_TBMA currentClickButton;
    S_ButtonInterface_TBMA currentHoverButton;
    
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
            if (raycast.collider.TryGetComponent<S_ButtonInterface_TBMA>(out S_ButtonInterface_TBMA button))
            {
                button.OnHover();

                if(button != currentClickButton)
                    button.ButtonImage.color = button.HighlightColor;

                currentHoverButton = button;

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
            if (raycast.collider.TryGetComponent<S_ButtonInterface_TBMA>(out S_ButtonInterface_TBMA button))
            {
                button.OnClickEnter();
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
