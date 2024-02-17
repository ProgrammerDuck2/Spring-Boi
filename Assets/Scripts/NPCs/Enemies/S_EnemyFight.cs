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
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
        {
            navMeshAgent.destination = new Vector3(Random.Range(-85, -40), 27, Random.Range(-15, -110));
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.destination = player.transform.position - transform.forward * 1.5f;
        }

        nextAttack += Time.deltaTime;
        Debug.Log(nextAttack);
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
                Hurt(20);
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

    public void Hurt(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
