using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PlayerHealth_MA : MonoBehaviour
{
    Transform deathSpawn;

    [SerializeField] Material handMat;
    [SerializeField] Color originalColor;
    [SerializeField] Color lowColor;

    float value;
    bool dying;
    // Start is called before the first frame update
    void Start()
    {
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
        handMat.color = originalColor;

        deathSpawn = FindFirstObjectByType<S_DeathArea_TB>().transform.GetChild(0);
    }

    private void Update()
    {
        value = S_Stats_MA.playerHealth / S_Stats_MA.maxHealth;

        handMat.color = lerpColor(lowColor, originalColor, value);

        if (S_Stats_MA.playerHealth <= 0 && !dying)
        {
            dying = true;
            StartCoroutine(Death());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        {
            dying = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        print("death");
        float deathCounter = 1;
        while(deathCounter > 0.5f)
        {
            deathCounter -= Time.unscaledDeltaTime;

            if(Time.timeScale - Time.unscaledDeltaTime > 0)
                Time.timeScale -= Time.unscaledDeltaTime;
            else
                Time.timeScale = 0;

            yield return new WaitForEndOfFrame();
        }

        transform.position = deathSpawn.position;
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
        Time.timeScale = 1;
        dying = false;
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
