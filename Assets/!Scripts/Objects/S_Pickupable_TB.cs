using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Pickupable_TB : S_InteractableObject_TB
{
    Rigidbody rb;
    Collider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public override void Interact(S_Hand_TB hand)
    {
        base.Interact(hand);
        transform.parent = hand.transform;
        rb.isKinematic = true;
        col.enabled = false;
        print("ball");
    }
    public override void EndInteract(S_Hand_TB hand)
    {
        base.EndInteract(hand);
        transform.parent = null;
        rb.isKinematic = false;
        col.enabled = true;

        rb.velocity += hand.motion.CalculateHandVelocity() * 3;
    }
}
