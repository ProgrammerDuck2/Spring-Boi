using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HandAim_TB : MonoBehaviour
{
    S_Hand_TB hand;
    [Required]
    [SerializeField] GameObject indicator;
    [HideInInspector] public GameObject AimingAt;

    [SerializeField] LayerMask AimAssist;
    private void Start()
    {
        hand = GetComponent<S_Hand_TB>();
    }
    private void Update()
    {
        if(hand.GripActivated)
        {
            Aim();
        }
        else if (AimingAt != null)
        {
            Destroy(AimingAt);
        }
    }
    public void Aim()
    {
        if (hand.handForwards.Count <= 9) return;

        Physics.Raycast(transform.position, hand.GetAverageVector3(hand.handForwards), out RaycastHit hit, S_Stats_MA.HandLaunchReach, hand.grabable);

        if(hit.collider)
        {
            if(AimingAt == null)
                AimingAt = Instantiate(indicator);

            Collider[] AimAssists = Physics.OverlapSphere(hit.point, .5f, AimAssist);

            if (AimAssists.Length > 0)
            {
                AimingAt.transform.position = AimAssists[0].transform.position;
            } else
            {
                AimingAt.transform.position = hit.point;
            }


            AimingAt.transform.localScale = Vector3.one * Vector3.Distance(transform.position, hit.point) * .005f;
        } 
        else if(AimingAt != null)
        {
            Destroy(AimingAt);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1000);

        if (hit.collider)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, hit.point);
        }
    }
}
