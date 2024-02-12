using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Respawn_MA : MonoBehaviour
{

    [HideInInspector] public Vector3 respawnPoint = new Vector3(0, 1, 0);
    [SerializeField] private float outOfWorld;
    S_Movement_TB movement;
    private bool hasHappened;
    private bool enemyTerritory = true;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<S_Movement_TB>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (enemyTerritory == true)
        {

            if (movement.Grounded == true)
            {
                if (hasHappened == false)
                {
                    respawnPoint = gameObject.transform.position;
                    hasHappened = true;
                }
            }
            else
            {
                hasHappened = false;
            }
        }

    }

    void FixedUpdate()
    {
        if (transform.position.y < outOfWorld)
            transform.position = respawnPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyArea")
        {
            enemyTerritory = false;
                Debug.Log("false");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyArea")
        {
            enemyTerritory = true;
            Debug.Log("true");
        }

    }
}
