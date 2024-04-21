using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class S_PistonLong_MA : S_Enemies_MA
{
    private int mouse;
    private float longPistonHealth = 50;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("found player");
        transform.LookAt(player.transform);
    }
    public override IEnumerator Attack(float damage)
    {
        yield return null;
        S_Stats_MA.playerHealth -= damage;
    }

    public override void Hurt(float damage, GameObject WhoDealtDamage)
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
