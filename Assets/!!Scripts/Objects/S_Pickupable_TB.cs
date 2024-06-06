using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_Pickupable_TB : S_InteractableObject_TB
{
    [SerializeField] bool isEnemy;
    Rigidbody rb;
    Collider col;

    NavMeshAgent agent;
    S_EnemyFight_TB enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if(isEnemy)
        {
            agent = GetComponent<NavMeshAgent>();
            enemy = GetComponent<S_EnemyFight_TB>();
        }
    }

    public override void Interact(S_Hand_TB hand)
    {
        if (isEnemy)
            toggleEnemy();

        base.Interact(hand);
        transform.parent = hand.transform;
        rb.isKinematic = true;
        col.enabled = false;
    }
    public override void EndInteract(S_Hand_TB hand)
    {
        if (isEnemy)
            toggleEnemy();

        base.EndInteract(hand);
        transform.parent = null;
        rb.isKinematic = false;
        col.enabled = true;

        rb.velocity += hand.motion.CalculateHandVelocity() * 3;
    }

    void toggleEnemy()
    {
        agent.enabled = !agent.enabled;
        enemy.enabled = !enemy.enabled;
    }
}
