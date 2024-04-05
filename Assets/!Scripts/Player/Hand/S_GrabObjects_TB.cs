using NaughtyAttributes;
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

        if (other.CompareTag(pickupableTag))
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
        if (toPickup.TryGetComponent<S_PickupReaction_TB>(out S_PickupReaction_TB pr))
        {
            if (pickUp)
            {
                pr.PickedUp();
            }
            else
            {
                pr.PickedUp();
            }
        }
        if (pickUp)
        {
            pickedUp = toPickup;
            pickedUp.transform.parent = transform;
        }
        else
        {
            pickedUp.transform.parent = null;
            pickedUp = null;
        }

        if (toPickup.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = pickUp;

            if (!rb.isKinematic)
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