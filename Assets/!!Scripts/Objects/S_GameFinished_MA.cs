using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameFinished_MA : MonoBehaviour
{
    [SerializeField] S_Lever_TB lever;

    //call dad
    S_CallTablet_MA tabletCall;

    //fireworks
    [SerializeField] GameObject fireworks;

    //oilCans
    [SerializeField] GameObject oilCan;

    // Start is called before the first frame update
    void Start()
    {
        tabletCall = FindFirstObjectByType<S_CallTablet_MA>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever == null) return;
        if (lever.active)
        {
            //add whatever
            Instantiate(fireworks);

            Instantiate(oilCan);

            tabletCall.startRinging = true;
            //also this is the oil distrubition system so make that make sense ig
        }
    }
}
