using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lever_TB : S_InteractableObject_TB
{
    public bool active;

    public override void Interact(S_Hand_TB hand)
    {
        base.Interact(hand);

        StartCoroutine(activate());
    }

    IEnumerator activate()
    {
        float value = 0f;
        while(value < 1)
        {
            if(!active)
                transform.eulerAngles = Vector3.Lerp(transform.right * -30, transform.right * 30, value);
            else
                transform.eulerAngles = Vector3.Lerp(transform.right * 30, transform.right * -30, value);
            value += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        active = !active;
    }
}
