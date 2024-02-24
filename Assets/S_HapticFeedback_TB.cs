using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_HapticFeedback_TB : MonoBehaviour
{
    [SerializeField] S_HapticFeedbackInensities_TB Low;
    [SerializeField] S_HapticFeedbackInensities_TB Medium;
    [SerializeField] S_HapticFeedbackInensities_TB High;

    public void TriggerHaptic(int Intensity, ActionBasedController controller)
    {
        switch (Intensity)
        {
            case 1:
                controller.SendHapticImpulse(Low.Intensity, Low.Duration);
                break;
            case 2:
                controller.SendHapticImpulse(Medium.Intensity, Medium.Duration);
                break;
            case 3:
                controller.SendHapticImpulse(High.Intensity, High.Duration);
                break;
            default:
                Debug.LogError("Keep HapticFeedback intensity within 1-3");
                break;
        }
    }
}
