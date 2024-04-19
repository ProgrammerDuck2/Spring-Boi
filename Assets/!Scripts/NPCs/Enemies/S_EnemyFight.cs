using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    private float punchLenght = 5;
    float punchSpeed = .01f;

    private float attackRate = 2f;
    private float nextAttack = 0.0f;

    [SerializeField]private float enemyHealth = 100;

    [SerializeField] private GameObject navCorner1;
    [SerializeField] private GameObject navCorner2;

    [SerializeField] List<Fracture> fractureArt;

    [SerializeField]GameObject mapIcon;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navCorner1 == null || navCorner2 == null) return;

        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;
        navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));

        hand = rightHand;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0) return;
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
        if (attackRate <= nextAttack)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                StartCoroutine(Attack(50));
            }
        }

        nextAttack += Time.deltaTime;
        //Debug.Log(nextAttack);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (attackRate <= nextAttack)
    //    {
    //        int whichHand = Random.Range(0, 2);
    //        if (whichHand == 0)
    //        {
    //            hand = leftHand;
    //        }
    //        else if (whichHand == 1)
    //        {
    //            hand = rightHand;
    //        }
    //        Attack(20);
    //    }

    //    else if (attackRate / 2 <= nextAttack)
    //    {
    //        Ready();
    //    }

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        mouse = 0;
    //    }
    //    else if (Input.GetMouseButtonDown(1))
    //    {
    //        mouse = 1;
    //    }

    //    if (Input.GetMouseButtonDown(mouse))
    //    {
    //        if (other.gameObject.tag == "Player")
    //        {
    //            Hurt(20, other.gameObject);
    //        }
    //    }
    //}

    public IEnumerator Attack(float damage)
    {
        nextAttack = 0;

        int whichHand = Random.Range(0, 2);
        if (whichHand == 0)
        {
            hand = leftHand;
        }
        else if (whichHand == 1)
        {
            hand = rightHand;
        }

        yield return StartCoroutine(Ready(1f));

        float timer = 0;
        float value = 0;
        
        while (timer < punchSpeed)
        {
            hand.transform.localScale = new Vector3(
                Mathf.Lerp(.5f, punchLenght, value), 
                hand.transform.localScale.y, 
                hand.transform.localScale.z
                );

            value += Time.deltaTime * (1 / punchSpeed);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            S_Stats_MA.playerHealth -= damage;
        }
    }

    public IEnumerator Ready(float timeToReady)
    {
        float timer = 0;
        float value = 0;

        while (timer < timeToReady)
        {
            hand.transform.localScale = new Vector3(
                Mathf.Lerp(1, .5f, value),
                hand.transform.localScale.y,
                hand.transform.localScale.z
                );
            timer += Time.deltaTime;
            value += Time.deltaTime * (1 / timeToReady);

            yield return new WaitForEndOfFrame();
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

        GameObject pieces = GameObject.Find(name + "Fragments");

        for (int i = 0; i < pieces.transform.childCount; i++)
        {
            pieces.transform.GetChild(i).gameObject.layer = 11;
            pieces.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(-(player.transform.position - transform.position).normalized * 1.5f, ForceMode.Impulse);
        }

        print(pieces);

        mapIcon.SetActive(false);

        Destroy(gameObject, 10);
    }
}
