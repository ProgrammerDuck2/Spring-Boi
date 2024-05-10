using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_NPCMovement_MA : MonoBehaviour
{
    [SerializeField] private GameObject navCorner1;
    [SerializeField] private GameObject navCorner2;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navCorner1 == null || navCorner2 == null) return;

        Vector3 c1 = navCorner1.transform.position;
        Vector3 c2 = navCorner2.transform.position;
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2)
        {
            navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
    }
}
