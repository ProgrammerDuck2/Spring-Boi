using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FinalOil_MA : MonoBehaviour
{
    Rigidbody rb;
    S_OilCollectible_MA script;
    Vector3 force { get { return new Vector3(Random.Range(-400, 600), 0, Random.Range(-400, 600)); } }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        script = GetComponent<S_OilCollectible_MA>();
        script.enabled = false;
        rb.AddForce(force, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("slayed");
        if (collider.gameObject.layer == 3)
        {
            script.pos = transform.position.y;
            Debug.Log(collider);
            script.enabled = true;
            rb.isKinematic = true;
        }
    }
}
