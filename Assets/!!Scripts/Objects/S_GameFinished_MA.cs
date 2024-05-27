using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameFinished_MA : MonoBehaviour
{
    [SerializeField] S_Lever_TB lever;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lever == null) return;
        if (lever.active)
        {
            //add whatever
            //fireworks
            //call from dad
            //also this is the oil distrubition system so make that make sense ig
        }
    }
}
