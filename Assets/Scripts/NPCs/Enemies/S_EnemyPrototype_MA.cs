using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyPrototype_MA : MonoBehaviour, S_Enemies_MA
{
    [HideInInspector] public float maxHealth = 100;
    [HideInInspector] public float health;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    private GameObject currentBullet;
    public LayerMask layer;


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
            int damage = 90;
            S_Stats_MA.playerHealth -= damage;
            //Debug.Log("ouch");
            Destroy(currentBullet);
        }
        //Debug.Log(Physics.CheckSphere(currentBullet.transform.position, 1, layer));
        currentBullet.transform.position += transform.forward * Time.deltaTime * 10;
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
        
        //enemy will take damage here
    }
}