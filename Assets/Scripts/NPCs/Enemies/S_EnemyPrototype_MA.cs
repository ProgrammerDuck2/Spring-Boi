using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_EnemyPrototype_MA : MonoBehaviour, S_Enemies_MA
{
    [HideInInspector] public float maxHealth = 100;
    [HideInInspector] public float health;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    private GameObject currentBullet;
    public LayerMask layer;
    private int mouse;
    private float enemyHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //movement and aim
        transform.LookAt(player.transform);

        if (currentBullet == null)
        {
            Attack(10);
        }
        else if (Physics.CheckSphere(currentBullet.transform.position, .1f, layer))
        {
            int damage = 5;
            S_Stats_MA.playerHealth -= damage;
            //Debug.Log("ouch");
            Destroy(currentBullet);
        }
        //Debug.Log(Physics.CheckSphere(currentBullet.transform.position, 1, layer));
        currentBullet.transform.position += transform.forward * Time.deltaTime * 10;
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
                Debug.Log("hit");
                Hurt(20);
            }
        }
    }

    public void Attack(float damage)
    {
        //S_Stats_MA.playerHealth -= damage;
        currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        Destroy(currentBullet, 2);
        //deal damage
    }

    public void Hurt(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 20)
        {
            //GetComponent<Renderer>().material.color = Color.red;
        }
        if (enemyHealth <= 0)
        {
            //Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}