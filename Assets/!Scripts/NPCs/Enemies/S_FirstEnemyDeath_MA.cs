using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FirstEnemyDeath_MA : MonoBehaviour
{
    [SerializeField] GameObject oilCan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Instantiate(oilCan, gameObject.transform);
        }
    }
}
