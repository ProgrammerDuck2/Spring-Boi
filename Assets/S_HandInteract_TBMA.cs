using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_HandInteract_TBMA : MonoBehaviour
{
    [SerializeField] LayerMask Interactable;
    [HideInInspector] RaycastHit raycast;

    [Required]
    [SerializeField] S_Hand_TB hand;

    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out raycast, 100, Interactable);

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
            } else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        if (raycast.collider != null)
        {
            if (raycast.collider.TryGetComponent<S_ButtonInterface_TBMA>(out S_ButtonInterface_TBMA button))
            {
                button.OnClick();
            }
            else
            {
                Debug.LogError("No button hit or button lack interface");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (hand.DebugMode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);
        }
    }
}
