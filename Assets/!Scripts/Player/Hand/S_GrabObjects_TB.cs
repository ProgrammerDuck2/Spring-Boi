using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GrabObjects_TB : S_Hand_TB
{
    [Tag]
    [SerializeField] string pickupableTag;

    Transform pickedUp;

    bool canPickup;

    private void Update()
    {
        if (pickedUp == null) return;
        if (!handInput.grabActivated) togglePickedUp(false, pickedUp);
    }
    private void OnTriggerEnter(Collider other)
    {
        canPickup = !handInput.grabActivated;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!canPickup)
        {
            canPickup = !handInput.grabActivated;
            return;
        }
        if (!handInput.grabActivated) return;

        if(other.CompareTag(pickupableTag))
        {
            togglePickedUp(true, other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canPickup = false;
    }

    void togglePickedUp(bool pickUp, Transform toPickup)
    {
        if(pickUp)
        {
            toPickup.GetComponent<S_PickupReaction_TB>().PickedUp();
            pickedUp = toPickup;
            pickedUp.transform.parent = transform;
        }
        else
        {
            toPickup.GetComponent<S_PickupReaction_TB>().LetGo();
            pickedUp.transform.parent = null;
            pickedUp = null;
        }

        if (toPickup.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = pickUp;

            if(!rb.isKinematic)
            {
                rb.velocity += motion.CalculateHandVelocity() * 3;
            }
        }

        if (toPickup.TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = !pickUp;
        }
    }
}
