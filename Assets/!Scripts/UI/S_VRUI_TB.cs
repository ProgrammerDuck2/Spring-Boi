using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_VRUI_TB : MonoBehaviour
{
    S_HapticFeedback_TB hapticFeedback
    {
        get { return FindFirstObjectByType<S_HapticFeedback_TB>(); }
    }

    public virtual void OnClickEnter(ActionBasedController controller)
    {
        hapticFeedback.TriggerHaptic(.3f, .1f, controller);
    }
    public virtual void OnClick()
    {
    }
    public virtual void OnClickExit()
    {
    }
    public virtual void OnHoverEnter(ActionBasedController controller)
    {
        hapticFeedback.TriggerHaptic(.3f, .1f, controller);
    }
    public virtual void OnHover()
    {
    }

    public virtual void OnHoverExit()
    {

    }
}
