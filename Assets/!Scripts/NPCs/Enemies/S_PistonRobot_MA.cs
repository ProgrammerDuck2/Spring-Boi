using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_PistonRobot_MA : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    //NPC only moves by default if activated
    public bool move;

    private float attackRate = 1f;
    private float nextAttack = 0.0f;

    [SerializeField] private float enemyHealth = 100;

    [SerializeField] private GameObject navCorner1;
    [SerializeField] private GameObject navCorner2;

    [SerializeField] List<Fracture> fractureArt;

    [SerializeField] GameObject mapIcon;

    [SerializeField] GameObject oilCan;


    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        if (move)
        {
            if (navCorner1 == null || navCorner2 == null) return;

            Vector3 c1 = navCorner1.transform.position;
            Vector3 c2 = navCorner2.transform.position;
            navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 2)
        {
            navMeshAgent.speed = 3.5f;
        }
            if (enemyHealth <= 0) return;

        //Navmesh random default movement
        if (move)
        {
            if (navCorner1 == null || navCorner2 == null) return;

            Vector3 c1 = navCorner1.transform.position;
            Vector3 c2 = navCorner2.transform.position;
            if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
            {
                navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
            }
        }
        //ATTACK
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.destination = player.transform.position - transform.forward * 1.5f;
            navMeshAgent.speed = 6;
        }
        if (attackRate <= nextAttack)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 5)
            {
                StartCoroutine(Attack(50));
            }
        }

        nextAttack += Time.deltaTime;
    }

    public IEnumerator Attack(float damage)
    {
        nextAttack = 0;

        yield return new WaitForSeconds(.5f);

        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            S_Stats_MA.playerHealth -= damage;
        }
    }

    public void Hurt(float damage, GameObject WhoDealtDamage)
    {
        print("hurt");
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Kill();
        }
    }

    [Button]
    void Kill()
    {
        enemyHealth = 0;
        navMeshAgent.enabled = false;

        foreach (var item in fractureArt)
        {
            item.CauseFracture();
        }

        mapIcon.SetActive(false);

        Instantiate(oilCan, transform.position, transform.rotation);

        Destroy(gameObject, 10);
    }
}
