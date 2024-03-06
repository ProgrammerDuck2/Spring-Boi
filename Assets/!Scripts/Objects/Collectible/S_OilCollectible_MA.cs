using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_OilCollectible_MA : MonoBehaviour
{
    [Range(0,1)]
    public float move;
    bool torje;
    float pos;

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

        if (torje)
        {
            move += .1f * Time.deltaTime;
            if (move > .2f)
            {
                torje = false;
            }
        }
        if (!torje)
        {
            move -= .1f * Time.deltaTime; ;
            if (move < 0)
            {
                torje = true;
            }
        }
        Debug.Log(move);
    }
    //transform.position += transform.up* Time.deltaTime;

    private void OnTriggerEnter(Collider other)
    {
        S_Stats_MA.oilCollected++;
        Destroy(gameObject);
    }

}
