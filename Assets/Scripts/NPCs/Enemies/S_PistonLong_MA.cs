using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PistonLong_MA : MonoBehaviour, S_Enemies_MA
{
    [SerializeField] private GameObject player;
    public bool Grounded;
    Vector3 groundCheckPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stickGroundLayer;
    private int mouse;
    private float longPistonHealth = 50;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10;
        transform.Rotate (new Vector3(0,.2f, 0));
        
        
        if (Grounded)
        {
            transform.position += transform.up * Time.deltaTime * 10;
        }
        if (!Grounded)
        {
            transform.position -=transform.up * Time.deltaTime * 10;
        }
    }

    private void FixedUpdate()
    {
        groundCheckPos = transform.position - transform.up * 0.9f;

        if (Grounded != Physics.CheckSphere(groundCheckPos, 1 * 0.5f, groundLayer))
        {
            Collider[] ground = Physics.OverlapSphere(groundCheckPos, 1 * 0.5f, stickGroundLayer);

            Grounded = !Grounded;
            transform.parent = ground.Length >= 1 ? ground[0].transform : null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("found player");
        //transform.LookAt(player.transform);
        Attack(1);

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
                //Debug.Log("hit");
                Hurt(50);
            }
        }
    }
    public void Attack(float damage)
    {
        S_Stats_MA.playerHealth -= damage;
    }

    public void Hurt(float damage)
    {
        longPistonHealth -= damage;
        if (longPistonHealth <= 20)
        {
            //GetComponent<Renderer>().material.color = Color.red;
        }
        if (longPistonHealth <= 0)
        {
            //Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}
