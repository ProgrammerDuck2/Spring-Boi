using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HandMotion_TB : S_Hand_TB
{
    [Header("Motion")]
    [ShowIf("DebugMode")]
    public List<Vector3> handPos = new List<Vector3>();
    [ShowIf("DebugMode")]
    public List<Vector3> handRot = new List<Vector3>();
    [ShowIf("DebugMode")]
    public List<Vector3> handForwrds = new List<Vector3>();

    float timer;

    private void Update()
    {
        timer += Time.deltaTime * 2;

        if (timer > .1f)
        {
            handPos.Insert(0, transform.localPosition);
            handRot.Insert(0, handPostioning.controllerRotation.eulerAngles);
            handForwrds.Insert(0, transform.forward);
        }

        if (handPos.Count > 10)
        {
            handPos.Remove(handPos[10]);
            handRot.Remove(handRot[10]);
            handForwrds.Remove(handForwrds[10]);
        }
    }

    public Vector3 GetAverageVector3(List<Vector3> Vector3s)
    {
        Vector3 average = Vector3.zero;

        for (int i = 0; i < Vector3s.Count; i++)
        {
            average += Vector3s[i];
        }

        average /= handForwrds.Count;

        return average;
    }
}
