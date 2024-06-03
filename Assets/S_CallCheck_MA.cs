using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CallCheck_MA : MonoBehaviour
{
    public bool call;
    private void OnCollisionEnter(Collision collision)
    {
        print(call);
        if (collision.gameObject.tag == "IpadCall")
        {
            call = true;
        }
    }
}