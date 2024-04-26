using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HandProjectile_TB : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            S_Enemies_MA enemy = other.GetComponent<S_Enemies_MA>();
            enemy.Hurt(100, gameObject);
        }
    }
}
