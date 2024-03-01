using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_HandInteract_TBMA : MonoBehaviour
{
    [SerializeField] LayerMask Interactable;
    [HideInInspector] public RaycastHit raycast;

    S_Hand_TB hand;

    private void Start()
    {
        hand = GetComponent<S_Hand_TB>();
    }
    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out raycast, 2, Interactable);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        raycast.collider.GetComponent<S_ButtonInterface_TBMA>().OnClick();
    }
}
