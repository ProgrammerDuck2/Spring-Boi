using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Respawn_MA : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private float outOfWorld;

    void FixedUpdate()
    {
        if (transform.position.y < outOfWorld)
            transform.position = respawnPoint.transform.position;
    }
}
