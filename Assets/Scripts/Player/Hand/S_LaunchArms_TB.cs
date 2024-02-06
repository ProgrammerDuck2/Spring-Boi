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

    [Range(0, 10)]
    [SerializeField] int speed;
    [Range(1, 100)]
    [SerializeField] int reach;

    S_Grab_TB grab;

    [Required]
    [SerializeField] GameObject handToLaunch;
    GameObject currentHandMissile;

    GameObject handArt;

    bool pullingHand;

    bool stop;

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

            if(pullingHand)
            {
                currentHandMissile.transform.LookAt(transform);

                if(Vector3.Distance(transform.position, currentHandMissile.transform.position) <= .5f)
                {
                    pullingHand = false;
                    handArt.SetActive(true);
                    grab.enabled = true;
                    playerRB.isKinematic = true;
                    playerCC.enabled = true;

                    if(!hand.otherController.GetComponent<S_Grab_TB>().holding)
                    {
                        playerMovement.enabled = true;
                    }

                    Destroy(currentHandMissile);
                }
            } else
            {
                if(Physics.CheckSphere(currentHandMissile.transform.position, .5f, grab.grabable))
                {
                    if(hand.grabActivated)
                    {
                        if(pullingHand)
                        {
                            currentHandMissile.GetComponent<SpringJoint>().connectedBody = playerRB;
                            playerRB.isKinematic = false;
                            playerMovement.enabled = false;
                            playerCC.enabled = false;
                            stop = true;
                        } else
                        {
                            currentHandMissile.GetComponent<SpringJoint>().connectedBody = playerRB;
                            playerRB.isKinematic = false;
                            playerRB.useGravity = true;
                            playerMovement.enabled = false;
                            playerCC.enabled = false;
                            stop = true;
                        }
                    } 
                    else
                    {
                        activatePull();
                    }
                } 

                if(Vector3.Distance(transform.position, currentHandMissile.transform.position) >= reach)
                {
                    activatePull();
                }
            }

            if(!stop)
                currentHandMissile.transform.position += handVelocity * Time.deltaTime;
        }
    }

    public void LaunchArm(InputAction.CallbackContext context)
    {
        if(currentHandMissile == null)
        {
            pullingHand = false;
            currentHandMissile = Instantiate(handToLaunch, transform.position, hand.controllerRotation);

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
        stop = false;
    }

    Vector3 speedCalc()
    {
        return currentHandMissile.transform.forward * speed * 4;
    }
}
