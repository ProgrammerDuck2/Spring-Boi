using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerHealth_MA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
        //Debug.Log(S_Stats_MA.playerHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (S_Stats_MA.playerHealth <= 0)
        {
            transform.position = GetComponent<S_Respawn_MA>().respawnPoint;
            //Debug.Log(GetComponent<S_Respawn_MA>().respawnPoint);
            //Debug.Log(transform.position);
            S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
        }
    }
}
