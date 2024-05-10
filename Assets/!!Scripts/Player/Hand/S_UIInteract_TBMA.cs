using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_UIInteract_TBMA : S_Hand_TB
{
    [SerializeField] LayerMask Interactable;
    [HideInInspector] RaycastHit raycast;

    S_VRUI_TB currentClickElement;
    public S_VRUI_TB currentHoverElement;
    
    bool clicking;


    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out raycast, 20, Interactable);

        if (DebugMode)
        {
            Debug.DrawLine(transform.position, raycast.point, Color.red);
        }

        if (raycast.collider != null)
        {
            if (raycast.collider.TryGetComponent<S_VRUI_TB>(out S_VRUI_TB uiElement))
            {
                if (currentHoverElement == null)
                {
                    currentHoverElement = uiElement;

                    uiElement.OnHoverEnter(GetComponent<ActionBasedController>());
                } 
                else if(currentHoverElement != uiElement)
                {
                    uiElement.OnHoverEnter(GetComponent<ActionBasedController>());
                    currentHoverElement.OnHoverExit();

                    currentHoverElement = uiElement;
                }
                else if (!clicking) 
                    uiElement.OnHover();

            } else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
        else if (currentHoverElement != null)
        {
            currentHoverElement.OnHoverExit();
            currentHoverElement = null;
        }
    }

    public void ClickEnter(InputAction.CallbackContext context)
    {
        if (raycast.collider != null)
        {
            if (raycast.collider.TryGetComponent<S_VRUI_TB>(out S_VRUI_TB uiElement))
            {
                uiElement.OnClickEnter(GetComponent<ActionBasedController>());
                currentClickElement = uiElement;
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
        if (currentClickElement != null)
        {
            currentClickElement.OnClick();
        }
    }
    public void ClickExit(InputAction.CallbackContext context)
    {
        if (currentClickElement != null)
        {
            currentClickElement.OnClickExit();
            currentClickElement = null;
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
