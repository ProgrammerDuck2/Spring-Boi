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

    [SerializeField] private Vector3 punchLenght;
    [SerializeField] private float punchSpeed;
    private float currentPunchLenght;

    private float attackRate = 0.1f;
    private float nextAttack = 0.0f;

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
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.destination = player.transform.position - transform.forward * 1.5f;


        }
    }

    private void OnTriggerStay(Collider other)
    {
        //attack
        hand = leftHand;
        Vector3 newscale = hand.transform.localScale;

        //int whichHand = Random.Range(0, 1);
        //if (whichHand == 0)
        //{
        //    hand = leftHand;
        //    leftHand.transform.position += transform.forward;
        //}
        //else if (whichHand == 1)
        //{
        //    hand = rightHand;
        //}
        transform.localScale = new Vector3(0, 0, 10f) + transform.localScale;

        if (hand == leftHand)
        {
            hand.transform.position += transform.forward;
        }
        
        //currentPunchLenght = Vector3.Distance(hand.transform.localScale, hand.transform.localScale + punchLenght);

        //if (currentPunchLenght < punchLenght.y)
        //{
        //    if (Time.time > nextAttack)
        //    {
        //        attackRate = Time.time + nextAttack;

        //        transform.localScale = new Vector3(0, 0, 10f) + transform.localScale;
        //        hand.transform.localScale = hand.transform.forward + punchLenght;
        //    }
        //}
        //else if (currentPunchLenght >= punchLenght.y)
        //{
        //    transform.localScale = new Vector3(0, 0, -10f) + transform.localScale;
        //}

        //punchLenght - how far the arm stretches (5)
        //current punchLenght - to see if it is stretched
        //
        hand.transform.position += hand.transform.forward * Time.deltaTime * punchSpeed;


        //hurt
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
        
    }

    public void Hurt(float damage)
    {
        
    }
}
