using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HapticFeedbackIntensity", menuName = "Player/HapticFeedback", order = 1)]
[System.Serializable]
public class S_HapticFeedbackInensities_TB : ScriptableObject
{
    [Range(.1f, 1f)]
    public float Intensity = .1f;
    [Range(.1f, 1f)]
    public float Duration = .1f;
}
