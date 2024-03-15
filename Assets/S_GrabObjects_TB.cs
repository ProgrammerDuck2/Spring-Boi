using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GrabObjects_TB : S_Hand_TB
{
    [Tag]
    [SerializeField] string pickupableTag;

    Transform pickedUp;

    private void Update()
    {
        if (pickedUp == null) return;
        if (!handInput.grabActivated) togglePickedUp(false, pickedUp);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!handInput.grabActivated) return;

        if(other.CompareTag(pickupableTag))
        {
            togglePickedUp(true, other.transform);
        }
    }

    void togglePickedUp(bool pickUp, Transform toPickup)
    {
        if(pickUp)
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
        }

        if (toPickup.TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = !pickUp;
        }
    }
}
