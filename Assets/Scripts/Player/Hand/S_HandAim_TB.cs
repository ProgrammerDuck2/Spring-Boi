using NaughtyAttributes;
using UnityEngine;

public class S_HandAim_TB : MonoBehaviour
{
    S_Hand_TB hand;
    [Required]
    [SerializeField] GameObject indicator;
    GameObject currentIndicator;
    private void Start()
    {
        hand = GetComponent<S_Hand_TB>();
    }
    public void Aim()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, S_Stats_MA.HandLaunchReach, hand.grabable);

        if(hit.collider)
        {
            if(currentIndicator == null)
                currentIndicator = Instantiate(indicator);

            currentIndicator.transform.position = hit.point;

            print(hit.point); 
        } 
        else if(currentIndicator != null)
        {
            Destroy(currentIndicator);
        }
    }
}
