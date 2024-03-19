using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class S_EnemyFight : MonoBehaviour, S_Enemies_MA
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    private int mouse;

    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private GameObject hand;

    private float punchLenght = 1;

    private float attackRate = 1f;
    private float nextAttack = 0.0f;

    private float enemyHealth = 100;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(navCorner1 == null || navCorner2 == null) return;

        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
        {
            navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.destination = player.transform.position - transform.forward * 1.5f;
        }
        Debug.Log(navMeshAgent.destination);

        nextAttack += Time.deltaTime;
        //Debug.Log(nextAttack);
    }

    private void OnTriggerStay(Collider other)
    {
        if (attackRate <= nextAttack)
        {
            int whichHand = Random.Range(0, 2);
            if (whichHand == 0)
            {
                hand = leftHand;
            }
            else if (whichHand == 1)
            {
                hand = rightHand;
            }
            Attack(20);
        }

        else if (attackRate/2 <= nextAttack)
        {
            Ready();
        }

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
                Hurt(20, other.gameObject);
            }
        }
    }

    public void Attack(float damage)
    {
        hand.transform.localScale = new Vector3(hand.transform.localScale.x, punchLenght, hand.transform.localScale.z);

        nextAttack = 0;

        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            S_Stats_MA.playerHealth -= damage;
        }
    }

    public void Ready()
    {
        hand.transform.localScale = new Vector3(hand.transform.localScale.x, punchLenght/2, hand.transform.localScale.z);
    }

    public void Hurt(float damage, GameObject WhoDealtDamage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
