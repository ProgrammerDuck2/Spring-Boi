using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_HapticFeedback_TB : MonoBehaviour
{
    public void TriggerHaptic(float Intensity, float Duration, ActionBasedController controller)
    {
        controller.SendHapticImpulse(Intensity, Duration);
    }
}
