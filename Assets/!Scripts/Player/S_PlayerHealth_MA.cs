using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PlayerHealth_MA : MonoBehaviour
{
    [SerializeField] private GameObject playerDeath;

    [SerializeField] Material handMat;
    [SerializeField] Color originalColor;
    [SerializeField] Color lowColor;

    float value;
    // Start is called before the first frame update
    void Start()
    {
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
        handMat.color = originalColor;
    }

    private void Update()
    {
        value = S_Stats_MA.playerHealth / S_Stats_MA.maxHealth;

        handMat.color = lerpColor(lowColor, originalColor, value);

        if (S_Stats_MA.playerHealth <= 0)
        {
            if(S_Settings_TB.IsVRConnected)
            {
                S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

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

    public void Hurt(float damage, GameObject WhoDealtDamage)
    {
        S_Stats_MA.playerHealth -= damage;
    }

    Color lerpColor(Color a, Color b, float t)
    {
        return new Color
            (
                math.lerp(a.r, b.r, t),
                math.lerp(a.g, b.g, t),
                math.lerp(a.b, b.b, t)
            );
    }
}
