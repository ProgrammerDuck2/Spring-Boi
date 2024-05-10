using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AimHelper_TB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = S_Stats_MA.AimAssistRadius;
    }
}
