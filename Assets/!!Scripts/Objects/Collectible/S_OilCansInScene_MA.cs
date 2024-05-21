using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_OilCansInScene_MA : MonoBehaviour
{
    int activeCans;
    // Start is called before the first frame update
    void Awake()
    {
        S_Stats_MA.maxOil = transform.childCount;
        S_Stats_MA.oilCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(S_Stats_MA.oilCollected);
    }
}
