using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(S_Hand_TB))]
public class S_LaunchArms_TB : MonoBehaviour
{
    S_Hand_TB hand;
    Rigidbody playerRB;
    S_Movement_TB playerMovement;
    CharacterController playerCC;

    S_Grab_TB grab;

    [Required]
    [SerializeField] GameObject handToLaunch;
    GameObject currentHandMissile;

    GameObject handArt;

    bool pullingHand;

    bool holding;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<S_Hand_TB>();
        handArt = transform.GetChild(0).gameObject;
        playerMovement = hand.Player.GetComponent<S_Movement_TB>();
        playerRB = hand.Player.GetComponent<Rigidbody>();
        playerCC = hand.Player.GetComponent<CharacterController>();

        grab = GetComponent<S_Grab_TB>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHandMissile != null)
        {
            Vector3 handVelocity = pullingHand ? speedCalc() : speedCalc() * 2;

            if(pullingHand && !holding)
            {
                currentHandMissile.transform.LookAt(transform);

                if(Vector3.Distance(transform.position, currentHandMissile.transform.position) <= .5f)
                {
                    pullingHand = false;
                    handArt.SetActive(true);
                    grab.enabled = true;
                    playerRB.isKinematic = true;
                    playerCC.enabled = true;

                    if(!hand.OtherController.GetComponent<S_Grab_TB>().holding)
                    {
                        playerMovement.enabled = true;
                    }

                    Destroy(currentHandMissile);
                }
            }
            else if (Vector3.Distance(transform.position, currentHandMissile.transform.position) >= S_Stats_MA.HandLaunchReach)
            {
                activatePull();
            }

            if (hand.GrabActivated)
            {
                if(Physics.CheckSphere(currentHandMissile.transform.position, .5f, grab.grabable))
                {
                    holding = true;

                    SpringJoint spring = currentHandMissile.GetComponent<SpringJoint>();
                    spring.connectedBody = playerRB;
                    playerRB.isKinematic = false;
                    playerMovement.enabled = false;
                    playerCC.enabled = false;
                    holding = true;

                    if (pullingHand)
                    {
                        playerRB.useGravity = false;
                        spring.maxDistance = 0;
                        spring.damper = 0.2f;
                    }
                    else
                    {
                        playerRB.useGravity = true;
                        spring.maxDistance = Vector3.Distance(hand.Player.transform.position, currentHandMissile.transform.position);

                        spring.damper = 20;
                    }
                } 
            } else
            {
                holding = false;
            }

            if (!holding)
            {
                currentHandMissile.transform.position += handVelocity * Time.deltaTime;

                if(Physics.CheckSphere(currentHandMissile.transform.position, .2f, hand.Punch.CanHit) && hand.Punch.OnCooldown)
                {
                    hand.Punch.Punch(Physics.OverlapSphere(currentHandMissile.transform.position, .2f, hand.Punch.CanHit), 3);
                }
            }
        }
    }

    public void LaunchArm(InputAction.CallbackContext context)
    {
        if(currentHandMissile == null)
        {
            pullingHand = false;
            currentHandMissile = Instantiate(handToLaunch, transform.position, transform.rotation);

            handArt.SetActive(false);
            grab.enabled = false;
        }
    }

    public void PullArm(InputAction.CallbackContext context)
    {
        activatePull();
    }

    void activatePull()
    {
        pullingHand = true;
        holding = false;
    }

    Vector3 speedCalc()
    {
        return currentHandMissile.transform.forward * S_Stats_MA.HandLaunchSpeed * 4;
    }
}
