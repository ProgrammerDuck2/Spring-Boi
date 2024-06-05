using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using FMODUnity;

[CreateAssetMenu(fileName = "CallInformation", menuName = "Callinformation")]
public class S_CallInformation_MA : ScriptableObject
{
    public List<string> callText = new List<string>();
    public List<EventReference> audioClips;
}
