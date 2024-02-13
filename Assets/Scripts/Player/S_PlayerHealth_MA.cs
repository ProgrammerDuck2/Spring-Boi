using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PlayerHealth_MA : MonoBehaviour
{
    [SerializeField] private GameObject playerDeath;
    // Start is called before the first frame update
    void Start()
    {
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
    }

    private void Update()
    {
        if (S_Stats_MA.playerHealth <= 0)
        {
            transform.position = GetComponent<S_Respawn_MA>().respawnPoint;
            Instantiate(playerDeath);

            if (playerDeath.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
