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

        Destroy();
    }
    [Button]
    private void Destroy()
    {
        fracture.CauseFracture();

        GameObject pieces = GameObject.Find(name + "Fragments");

        for (int i = 0; i < pieces.transform.childCount; i++)
        {
            GameObject piece = pieces.transform.GetChild(i).gameObject;

            piece.AddComponent<S_Pickupable_TB>();
            piece.tag = "Interactable";
            piece.gameObject.layer = 11;
            piece.GetComponent<Rigidbody>().AddForce(-transform.up * 10, ForceMode.Impulse);
        }
    }
}
