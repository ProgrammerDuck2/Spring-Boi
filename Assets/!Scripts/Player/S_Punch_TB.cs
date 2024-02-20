using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(S_Hand_TB))]
public class S_Punch_TB : MonoBehaviour
{
    S_Hand_TB hand;
    
    List<Vector3> handPostitions = new List<Vector3>();

    bool AttemptPunch;
    float punchTimer;

    [InfoBox("Only Enemies :)")]
    public LayerMask CanHit;

    [HideInInspector] public bool OnCooldown = true;
    

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<S_Hand_TB>();
    }

    // Update is called once per frame
    void Update()
    {
        punchTimer += Time.deltaTime;

        if (punchTimer > .1f)
        {

            handPostitions.Insert(0, hand.ControllerPosition);
        }

        if (handPostitions.Count < 11) return;

        float forceRequirement = hand.GrabActivated ? .4f : .7f;

        if (Vector3.Distance(handPostitions[0], handPostitions[handPostitions.Count - 1]) > forceRequirement && OnCooldown)
        {
            Punch(Physics.OverlapSphere(transform.position, .5f, CanHit), Vector3.Distance(handPostitions[0], handPostitions[handPostitions.Count - 1]) + 1);
            handPostitions.Clear();
        }
        else
        {
            handPostitions.Remove(handPostitions[handPostitions.Count - 1]);
        }
    }

    public void Punch(Collider[] hit, float multiplier)
    {
        float damage = Mathf.Round(S_Stats_MA.Damage * multiplier);

        for (int i = 0; i < hit.Length; i++)
        {
            hit[i].GetComponent<S_Enemies_MA>().Hurt(damage);
            print("Dealt Damage");
        }

        StartCoroutine(PunchCooldown());
    }

    IEnumerator PunchCooldown()
    {
        OnCooldown = false;
        yield return new WaitForSeconds(0.3f);
        OnCooldown = true;
    }
}
