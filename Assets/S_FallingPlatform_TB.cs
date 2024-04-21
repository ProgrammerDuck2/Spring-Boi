using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FallingPlatform_TB : MonoBehaviour
{
    public List<GameObject> support;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!SupportStanding())
        {
            rb.isKinematic = false;
            Destroy(this);
            print("fall");
        }
    }

    bool SupportStanding()
    {
        foreach (var item in support)
        {
            if(item.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
