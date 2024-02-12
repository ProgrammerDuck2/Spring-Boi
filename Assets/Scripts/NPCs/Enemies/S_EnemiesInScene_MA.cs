using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemiesInScene_MA : MonoBehaviour
{
    [SerializeField] private GameObject enemiesDeadText;
    private GameObject enemiesDead;
    private bool text = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            if (text == true)
            {
                enemiesDead = Instantiate(enemiesDeadText);
                text = false;
            }
        }
        if (text == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                enemiesDead.gameObject.SetActive(false);
            }
        }
    }
}
