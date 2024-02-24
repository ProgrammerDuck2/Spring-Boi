using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(S_Hand_TB))]
public class S_Punch_TB : MonoBehaviour
{
    S_Hand_TB hand;

    [InfoBox("Only Enemies :)")]
    public LayerMask CanHit;

    [HideInInspector] public bool OnCooldown = false;
    

    // Start is called before the first frame update
    void Start()
    {
        OnCooldown = false;
        hand = GetComponent<S_Hand_TB>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.handPostitions.Count <= 9) return;
        if (!hand.GripActivated) return;
        if (!hand.TriggerActivated) return;
        float forceRequirement = .5f;

        if (Vector3.Distance(hand.handPostitions[0], hand.handPostitions[hand.handPostitions.Count - 1]) > forceRequirement && !OnCooldown)
        {
            Punch(Physics.OverlapSphere(transform.position, .5f, CanHit), Vector3.Distance(hand.handPostitions[0], hand.handPostitions[hand.handPostitions.Count - 1]) + 1);
        }
    }

    public void Punch(Collider[] hit, float multiplier)
    {
        float damage = Mathf.Round((S_Stats_MA.Damage + hand.Player.GetComponent<Rigidbody>().velocity.magnitude * 10) * multiplier);

        for (int i = 0; i < hit.Length; i++)
        {
            hit[i].GetComponent<S_Enemies_MA>().Hurt(damage);
            hand.HapticFeedback.TriggerHaptic(1, GetComponent<ActionBasedController>());
            print(damage);
        }

        StartCoroutine(PunchCooldown());
    }

    IEnumerator PunchCooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(0.3f);
        OnCooldown = false;
    }
}
