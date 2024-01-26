using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StretchHands_TB : MonoBehaviour
{
    [SerializeField] GameObject[] hands;

    KeyCode[] grabs = new KeyCode[]
    {
        KeyCode.Q,
        KeyCode.E
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                Rigidbody handRB = hands[i].GetComponent<Rigidbody>();
                handRB.velocity = transform.forward * 100;
            }

            if (Input.GetKey(grabs[i]))
            {
                Rigidbody handRB = hands[i].GetComponent<Rigidbody>();


                hands[i].transform.parent = null;
                handRB.isKinematic = true;
            } else
            {
                Rigidbody handRB = hands[i].GetComponent<Rigidbody>();;


                hands[i].transform.parent = transform;
                handRB.isKinematic = false;
            }
        }
    }
}
