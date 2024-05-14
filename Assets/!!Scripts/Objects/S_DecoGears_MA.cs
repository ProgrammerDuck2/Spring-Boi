using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DecoGears_MA : MonoBehaviour
{
    [SerializeField] bool reverse;
    [SerializeField] bool randomDirection;
    // Start is called before the first frame update
    void Start()
    {
        if (randomDirection)
        {
            int range = Random.Range(0, 2);
            if (range == 0) { reverse = false; }
            if (range == 1) { reverse = true; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (reverse) { transform.Rotate(0, -90 * Time.deltaTime, 0, Space.Self); }

        if (!reverse) { transform.Rotate(0, 90 * Time.deltaTime, 0, Space.Self); }

    }
}
