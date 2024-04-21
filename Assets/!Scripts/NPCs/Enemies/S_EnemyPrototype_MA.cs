using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemyPrototype_MA : S_Enemies_MA
{
    [SerializeField] private GameObject bullet;
    private GameObject currentBullet;

    public LayerMask layer;
    private int mouse;

    private float fireRate = 1.0f;
    private float nextFire = 0.0f;

    // Update is called once per frame
    public override void Update()
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

    public override IEnumerator Attack(float damage)
    {
        currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        Destroy(currentBullet, 2);
        yield return null;
    }

    public override void Hurt(float damage, GameObject WhoDealtDamage)
    {
        maxHealth -= damage;
        if (maxHealth <= 20)
        {
        }
        if (maxHealth <= 0)
        {
            Destroy(gameObject);
            //Destroy(currentBullet);
        }
    }
}