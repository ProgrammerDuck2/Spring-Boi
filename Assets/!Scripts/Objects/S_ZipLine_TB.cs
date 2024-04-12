using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ZipLine_TB : S_InteractableObject_TB
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [Range(0f, 1f)]
    [SerializeField] float amount;

    [SerializeField] float timeToComplete;

    Vector3 lastFramePos;
    Vector3 CalculateVelocity
    {
        get { return (transform.position - lastFramePos) / Time.deltaTime; }
    }

    bool active;
    bool reverse;

    private void Start()
    {
        transform.position = start.position;
    }
    private void Update()
    {
        if(active)
        {
            amount += reverse ? -Time.deltaTime / timeToComplete : Time.deltaTime / timeToComplete;
        }
        amount = Mathf.Clamp(amount, 0f, 1f);

        if(active)
        {
            if (amount == 0 || amount == 1)
            {
                active = false;
            }
        }

        transform.position = lerpPos(start.position, end.position, amount);
    }

    private void LateUpdate()
    {
        lastFramePos = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        transform.position = lerpPos(start.position, end.position, amount);
    }

    public override void Interact(S_Hand_TB hand)
    {
        if (active) return;
        base.Interact(hand);
        active = true;
        reverse = amount > .5f;

        hand.player.transform.parent = transform;
        hand.playerRB.isKinematic = true;
    }
    public override void EndInteract(S_Hand_TB hand)
    {
        base.EndInteract(hand);

        hand.player.transform.parent = null;
        hand.playerRB.isKinematic = false;

        hand.playerRB.velocity += CalculateVelocity;
        print(CalculateVelocity);
    }

    Vector3 lerpPos(Vector3 value1, Vector3 value2, float t)
    {
        return new Vector3(
            Mathf.Lerp(value1.x, value2.x, t),
            Mathf.Lerp(value1.y, value2.y, t),
            Mathf.Lerp(value1.z, value2.z, t)
            );
    }
}
