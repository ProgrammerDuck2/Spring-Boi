using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FirstOilCan_MA : MonoBehaviour
{
    [SerializeField] S_EnemiesInScene_MA EnemiesInScene;
    [SerializeField] GameObject oilCan;
    [SerializeField] GameObject arenaCenter;
    bool hasHappened;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 arenaTransform = arenaCenter.transform.position;
        if (EnemiesInScene.enemiesInScene && !hasHappened)
        {
            //Debug.Log("w");
            Instantiate(oilCan, arenaTransform, oilCan.transform.rotation);
            hasHappened = true;
        }
    }
    //gameobjectWithScript.GetComponent<type>().variable
}
