using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Respawn_MA : MonoBehaviour
{
    //[SerializeField] private GameObject respawnPoint;
    [HideInInspector] public Vector3 respawnPoint = Vector3.zero;
    [HideInInspector] public Vector3 nextRespawnPoint = Vector3.zero;
    [SerializeField] private float outOfWorld;
    S_Movement_TB movement;
    private bool hasHappened;
    //for navmesh bool false if in trigger

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<S_Movement_TB>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (movement.Grounded == true)
        {
            if(hasHappened == false)
            {
                //Debug.Log("new ground");
                respawnPoint = nextRespawnPoint;
                nextRespawnPoint = gameObject.transform.position;
                hasHappened = true;
            }
        }
        else
        {
            hasHappened = false;
        }

    }

    //movement.Grounded - gets the grounded

    void FixedUpdate()
    {
        if (transform.position.y < outOfWorld)
            transform.position = respawnPoint;
    }
}
