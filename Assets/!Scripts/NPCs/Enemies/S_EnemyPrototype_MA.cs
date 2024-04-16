using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private float fireRate = 1.0f;
    private float nextFire = 0.0f;

    [SerializeField] private GameObject navCorner1;
    [SerializeField] private GameObject navCorner2;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;

        health = maxHealth;
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;

        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
        {
            navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
        }

        //attack
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.destination = player.transform.position;

            if (currentBullet == null)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    StartCoroutine(Attack(10));
                }
            }
            else if (Physics.CheckSphere(currentBullet.transform.position, .1f, layer))
            {
                int damage = 5;
                S_Stats_MA.playerHealth -= damage;
                Destroy(currentBullet);
            }
            else
            {
                currentBullet.transform.position += transform.forward * Time.deltaTime * 10;
            }
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
                Hurt(50, other.gameObject);
            }
        }
    }

    public IEnumerator Attack(float damage)
    {
        currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        Destroy(currentBullet, 2);
        yield return null;
    }

    public void Hurt(float damage, GameObject WhoDealtDamage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 20)
        {
        }
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            //Destroy(currentBullet);
        }
    }
}