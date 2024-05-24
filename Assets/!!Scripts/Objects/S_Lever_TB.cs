using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lever_TB : S_InteractableObject_TB
{
    [HideInInspector] public bool active;

    [Button]
    public void Activate()
    {
        StartCoroutine(activate());
    }

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
                transform.localEulerAngles = Vector3.Lerp(Vector3.right * -30, Vector3.right * 30, value);
            else
                transform.localEulerAngles = Vector3.Lerp(Vector3.right * 30, Vector3.right * -30, value);
            value += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        active = !active;
    }
}
