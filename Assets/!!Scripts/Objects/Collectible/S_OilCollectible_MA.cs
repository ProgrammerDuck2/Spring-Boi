using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_OilCollectible_MA : MonoBehaviour
{
    [Range(0,1)]
    public float move;
    bool direction;
    public float pos;

    // Start is called before the first frame update
    void Start()
    {
        move = 1;
        pos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 *Time.deltaTime, 0, Space.World);

        transform.position = new Vector3(transform.position.x, pos + Mathf.Clamp(move, 0, .5f), transform.position.z);

        if (direction)
        {
            move += .1f * Time.deltaTime;
            if (move > .2f)
            {
                direction = false;
            }
        }
        if (!direction)
        {
            move -= .1f * Time.deltaTime; ;
            if (move < 0)
            {
                direction = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        S_Stats_MA.oilCollected++;
        S_Stats_MA.playerHealth = S_Stats_MA.maxHealth;
        Destroy(gameObject);

        //dette funker ikke -Torje
        //S_AudioManager_HA.instance.PlayOneShot(S_FMODEvents_HA.instance.oilcanCollected, transform.position);
    }

}
