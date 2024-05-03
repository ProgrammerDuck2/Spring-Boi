using NaughtyAttributes;
using UnityEngine;

public class S_ObjectInteract_TB : S_Hand_TB
{
    S_InteractableObject_TB holding;

    bool canInteract;

    private void Update()
    {
        if (holding == null) return;
        if (!handInput.grabActivated) { holding.EndInteract(this); holding = null; }
    }
    private void OnTriggerEnter(Collider other)
    {
        canInteract = !handInput.grabActivated;
    }
    private void OnTriggerStay(Collider other)
    {
        if(holding != null) return;

        if (!canInteract)
        {
            canInteract = !handInput.grabActivated;
            return;
        }
        if (!handInput.grabActivated) return;

        if (other.TryGetComponent<S_InteractableObject_TB>(out S_InteractableObject_TB interactableObject))
        {
            holding = interactableObject;
            holding.Interact(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }

    //void togglePickedUp(bool pickUp, Transform toPickup)
    //{
    //    if (toPickup.TryGetComponent<S_PickupReaction_TB>(out S_PickupReaction_TB pr))
    //    {
    //        if (pickUp)
    //        {
    //            pr.PickedUp();
    //        }
    //        else
    //        {
    //            pr.PickedUp();
    //        }
    //    }
    //    if (pickUp)
    //    {
    //        holding = toPickup;
    //        holding.transform.parent = transform;
    //    }
    //    else
    //    {
    //        holding.transform.parent = null;
    //        holding = null;
    //    }

    //    if (toPickup.TryGetComponent<Rigidbody>(out Rigidbody rb))
    //    {
    //        rb.isKinematic = pickUp;

    //        if (!rb.isKinematic)
    //        {
    //            rb.velocity += motion.CalculateHandVelocity() * 3;
    //        }
    //    }

    //    if (toPickup.TryGetComponent<Collider>(out Collider collider))
    //    {
    //        collider.enabled = !pickUp;
    //    }
    //}
}
