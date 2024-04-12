using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Destructable_TB : MonoBehaviour
{
    [Layer]
    [SerializeField] int destroyes;

    Fracture fracture;

    private void Start()
    {
        fracture = GetComponent<Fracture>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != destroyes) return;


        fracture.CauseFracture();
    }
}
