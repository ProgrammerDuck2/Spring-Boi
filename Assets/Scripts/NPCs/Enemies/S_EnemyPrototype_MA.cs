using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class S_EnemyPrototype_MA : MonoBehaviour, S_Enemies_MA
{
    [HideInInspector] public float maxHealth = 100;
    [HideInInspector] public float health;
    private float enemyHealth = 100;

    private GameObject player;
    [SerializeField] private GameObject bullet;
    private GameObject currentBullet;

    public LayerMask layer;
    private int mouse;
    private NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();   
        navMeshAgent.destination = new Vector3(Random.Range(-85f, -40f), 27f, Random.Range(-15f, -110f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
        {
            navMeshAgent.destination = new Vector3(Random.Range(-85, -40), 27, Random.Range(-15, -110));
        }

        //attack
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.destination = player.transform.position;

            if (currentBullet == null)
            {
                Attack(10);
            }
            else if (Physics.CheckSphere(currentBullet.transform.position, .1f, layer))
            {
                int damage = 5;
                S_Stats_MA.playerHealth -= damage;
                Destroy(currentBullet);
            }
            currentBullet.transform.position += transform.forward * Time.deltaTime * 10;
        }
    }

    private void OnTriggerStay(Collider other)
    {
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
            if (other.gameObject.tag == "Player")
            {
                Hurt(50);
            }
        }
    }

    public void Attack(float damage)
    {
        currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        Destroy(currentBullet, 2);
    }

    public void Hurt(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 20)
        {
        }
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}