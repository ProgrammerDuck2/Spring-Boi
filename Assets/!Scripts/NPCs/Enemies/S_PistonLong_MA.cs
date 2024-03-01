using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class S_PistonLong_MA : MonoBehaviour, S_Enemies_MA
{
    private int mouse;
    private float longPistonHealth = 50;
    private GameObject player;
    private NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = new Vector3(Random.Range(-85f, -40f), 27f, Random.Range(-15f, -110f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 1)
        {
            navMeshAgent.destination = new Vector3(Random.Range(-85f, -40f), 27f, Random.Range(-15f, -110f));
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            Attack(1);

            if (Input.GetMouseButtonDown(0))
            {
                mouse = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                mouse = 1;
            }

            if (Input.GetMouseButtonDown(mouse))
            {
                    //Debug.Log("hit");
                    Hurt(50, player);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("found player");
        transform.LookAt(player.transform);
    }
    public void Attack(float damage)
    {
        S_Stats_MA.playerHealth -= damage;
    }

    public void Hurt(float damage, GameObject WhoDealtDamage)
    {
        longPistonHealth -= damage;
        if (longPistonHealth <= 20)
        {
            //GetComponent<Renderer>().material.color = Color.red;
        }
        if (longPistonHealth <= 0)
        {
            //Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}
