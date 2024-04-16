using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_Punch_TB : S_Hand_TB
{
    S_Hand_TB hand;
    S_PunchParticle_OR particle;

    [SerializeField] private GameObject particleHolder;

    [HideInInspector] public bool OnCooldown = false;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OnCooldown = false;
        hand = GetComponent<S_Hand_TB>();
        particle = particleHolder.GetComponent<S_PunchParticle_OR>();
    }

    // Update is called once per frame
    void Update()
    {
        if (handPostitions.Count <= 9) return;
        if (!handInput.gripActivated) return;
        if (!handInput.triggerActivated) return;
        float forceRequirement = .5f;

        if (Vector3.Distance(handPostitions[0], handPostitions[handPostitions.Count - 1]) > forceRequirement && !OnCooldown)
        {
            Punch(Physics.OverlapSphere(transform.position, .5f), Vector3.Distance(handPostitions[0], handPostitions[handPostitions.Count - 1]) + 1);
        }
    }

    public void Punch(Collider[] hit, float multiplier)
    {
        float damage = Mathf.Round((S_Stats_MA.Damage + hand.player.GetComponent<Rigidbody>().velocity.magnitude * 5) * multiplier);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].TryGetComponent<S_Enemies_MA>(out S_Enemies_MA enemy))
            {
                enemy.Hurt(damage, gameObject);
                hand.hapticFeedback.TriggerHaptic(.1f, .1f, GetComponent<ActionBasedController>());
            }
        }

        // Make sure S_PunchParticle_OR is found
        if (particle) { particle.ParticlesOnImpact(); }
        else { Debug.LogError("No S_PunchParticle_OR found"); }

        StartCoroutine(PunchCooldown());
    }

    IEnumerator PunchCooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(0.3f);
        OnCooldown = false;
    }
}
