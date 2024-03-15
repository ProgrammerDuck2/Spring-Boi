using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_EnemiesInScene_MA : MonoBehaviour
{
    public bool enemiesInScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            enemiesInScene = true;
        }
    }
}
