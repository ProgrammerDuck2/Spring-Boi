using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerHealth_MA : MonoBehaviour
{
    [SerializeField] private GameObject playerDeath;
    // Start is called before the first frame update
    void Start()
    {
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (S_Stats_MA.playerHealth <= 0)
        {
            transform.position = GetComponent<S_Respawn_MA>().respawnPoint;
            S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
            Instantiate(playerDeath);
        }
    }
}
