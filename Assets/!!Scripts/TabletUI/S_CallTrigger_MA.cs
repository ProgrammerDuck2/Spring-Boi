using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CallTrigger_MA : MonoBehaviour
{
    [HideInInspector] public bool tabletTrigger = false;

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            tabletTrigger = true;
            Destroy(this);
        }
    }

}
