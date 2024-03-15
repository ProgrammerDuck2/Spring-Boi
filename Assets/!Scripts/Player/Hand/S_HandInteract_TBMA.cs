using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_HandInteract_TBMA : S_Hand_TB
{
    [SerializeField] LayerMask Interactable;
    [HideInInspector] RaycastHit raycast;

    S_Button_TBMA currentClickButton;
    S_Button_TBMA currentHoverButton;
    
    bool clicking;


    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out raycast, 2, Interactable);

        if (DebugMode)
        {
            Debug.DrawLine(transform.position, raycast.point, Color.red);
        }

        if (raycast.collider != null)
        {
            Debug.Log("Hover");
            if (raycast.collider.TryGetComponent<S_Button_TBMA>(out S_Button_TBMA button))
            {
                if (currentHoverButton == null)
                {
                    button.OnHoverEnter(GetComponent<ActionBasedController>());

                    currentHoverButton = button;
                } else if(currentHoverButton != button)
                {
                    currentHoverButton.OnHoverExit();

                    button.OnHoverEnter(GetComponent<ActionBasedController>());

                    currentHoverButton = button;
                }

                if(!clicking) 
                    button.OnHover();

                if(button != currentClickButton)
                    button.ButtonImage.color = button.HighlightColor;

            } else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
        else if (currentHoverButton != null)
        {
            currentHoverButton.OnHoverExit();
            currentHoverButton = null;
        }
    }

    public void ClickEnter(InputAction.CallbackContext context)
    {
        if (raycast.collider != null)
        {
            if (raycast.collider.TryGetComponent<S_Button_TBMA>(out S_Button_TBMA button))
            {
                button.OnClickEnter(GetComponent<ActionBasedController>());
                currentClickButton = button;
                clicking = true;
            }
            else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
    }
    public void Click(InputAction.CallbackContext context)
    {
        if (currentClickButton != null)
        {
            currentClickButton.OnClick();
        }
    }
    public void ClickExit(InputAction.CallbackContext context)
    {
        if (currentClickButton != null)
        {
            currentClickButton.OnClickExit();
            currentClickButton = null;
            clicking = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }
}
