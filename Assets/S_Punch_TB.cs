using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(S_Hand_TB))]
[RequireComponent (typeof(BoxCollider))]
public class S_Punch_TB : MonoBehaviour
{
    S_Hand_TB hand;
    bool AttemptPunch;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<S_Hand_TB>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(hand.controllerPosition, new Vector3(0.2f, 0.8f, 0.1f)) < 0.1f && hand.grabActivated)
        {
            AttemptPunch = true;
            print(hand.controllerPosition);
        }
    }
}
