using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

    [SerializeField] private GameObject navCorner1;
    [SerializeField] private GameObject navCorner2;


    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navCorner1 == null || navCorner2 == null) return;

        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;
        navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
        Debug.Log(new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z)));
    }

    // Update is called once per frame
    void Update()
    {
        if (navCorner1 == null || navCorner2 == null) return;

        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
        {
            navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            Attack(333);

            //if (Input.GetMouseButtonDown(0))
            //{
            //    mouse = 0;
            //}
            //else if (Input.GetMouseButtonDown(1))
            //{
            //    mouse = 1;
            //}

            //if (Input.GetMouseButtonDown(mouse))
            //{
            //        //Debug.Log("hit");
            //        Hurt(50, player);
            //}
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
