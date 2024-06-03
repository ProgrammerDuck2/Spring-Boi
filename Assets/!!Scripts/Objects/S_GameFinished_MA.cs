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
    [SerializeField] GameObject instantiateFire;
    GameObject currentFirework;

    //oilCans
    [SerializeField] GameObject oilCan;
    [SerializeField] GameObject instantiateOil;
    GameObject currentOil;
    Rigidbody rb;
    bool oil;
    bool grounded;

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
            currentFirework = Instantiate(fireworks, instantiateFire.transform);
            Destroy(currentFirework, 3);

            if (!oil)
            {
                Oil();
                oil = true;
            }

            tabletCall.startRinging = true;
            //also this is the oil distrubition system so make that make sense ig
        }
    }
    void Oil()
    {
        for (int i = 0; i < 10; i++)
        {
            currentOil = Instantiate(oilCan, instantiateOil.transform);
            currentOil.AddComponent<S_FinalOil_MA>();
        }
    }
}
