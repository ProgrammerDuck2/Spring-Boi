using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_InteractableObject_TB : MonoBehaviour
{

    public virtual void Interact(S_Hand_TB hand)
    {
        hand.hapticFeedback.TriggerHaptic(.3f, .1f, hand.GetComponent<ActionBasedController>());
    }

    public virtual void EndInteract(S_Hand_TB hand)
    {

    }
}
