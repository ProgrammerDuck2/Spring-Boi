using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class S_LaunchArms_TB : S_Hand_TB
{
    Transform head;
    [Required]
    [SerializeField] GameObject handToLaunch;
    GameObject currentHandMissile;

    GameObject handArt;

    [SerializeField] bool pullingHand;
    bool holding;

    [SerializeField] float cooldown;

    [SerializeField] Vector3 launchDirectionOffset;

    [SerializeField]LayerMask grabable;

    float lauchedHandHitbox = .5f;
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        handArt = transform.GetComponent<ActionBasedController>().modelParent.gameObject;
        head = transform.parent.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHandMissile != null)
        {
            ControllHandMissile();
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        if (!handInput.gripActivated) return;
        if (handInput.triggerActivated) return;
        if (handInput.handPostitions.Count <= 9) return;

        float forceRequirement = .4f;

        if (Vector3.Distance(handInput.handPostitions[0], handInput.handPostitions[handPostitions.Count - 1]) > forceRequirement)
        {
            if (currentHandMissile == null)
            {
                Debug.Log("Launching Arm");
                ActivateLaunchArm();
            }
            else
            {
                Debug.Log("pulling Arm");
                ActivatePullArm();
            }

            cooldown = .5f;

        }
    }
    void ControllHandMissile()
    {
        if (!handInput.gripActivated)
        {
            holding = false;
            pullingHand = true;
        }

        if (Physics.CheckSphere(currentHandMissile.transform.position, lauchedHandHitbox, grabable))
        {
            if(handInput.gripActivated)
            {
                HoldOnto();
            } else
            {
                ActivatePullArm();
            }
        }

        if (!holding)
        {
            lauchedHandHitbox = .5f + Vector3.Distance(transform.position, currentHandMissile.transform.position)/10;

            if (pullingHand)
            {
                SendArmBack();
            }
            else if (Vector3.Distance(transform.position, currentHandMissile.transform.position) >= S_Stats_MA.HandLaunchReach)
            {
                ActivatePullArm();
            }

            //Speed
            Vector3 handVelocity = pullingHand ? speedCalc() : speedCalc() * 2;
            currentHandMissile.transform.position += handVelocity * Time.deltaTime;
        }
    }
    void SendArmBack()
    {
        currentHandMissile.transform.LookAt(transform);

        if (Vector3.Distance(transform.position, currentHandMissile.transform.position) <= .5f)
        {
            pullingHand = false;
            handArt.SetActive(true);
            //grab.enabled = true;
            playerRB.useGravity = true;

            //if (!otherController.GetComponent<S_Grab_TB>().holding)
            //{
            //    playerMovement.enabled = true;
            //}
            playerMovement.enabled = true;

            Destroy(currentHandMissile);
        }
    }
    void HoldOnto()
    {
        SpringJoint spring = currentHandMissile.GetComponent<SpringJoint>();

        if (holding != true)
        {
            Collider[] inRange = Physics.OverlapSphere(currentHandMissile.transform.position, lauchedHandHitbox, grabable);

            Physics.Raycast(currentHandMissile.transform.position, inRange[0].transform.position - currentHandMissile.transform.position, out RaycastHit hit, lauchedHandHitbox, grabable);
            
            if(hit.point != Vector3.zero)
                currentHandMissile.transform.position = hit.point;

            holding = true;
            hapticFeedback.TriggerHaptic(.5f, .1f, GetComponent<ActionBasedController>());

            spring.connectedBody = playerRB;
            playerMovement.enabled = false;
            holding = true;
            currentHandMissile.GetComponent<Light>().enabled = true;    
        }

        if (pullingHand)
        {
            playerRB.useGravity = false;
            spring.maxDistance = 0;
            spring.damper = 0.2f;
            player.GetComponent<S_Movement_TB>().highSpeed = true;
        }
        else
        {
            playerRB.useGravity = true;
            spring.maxDistance = Vector3.Distance(player.transform.position, currentHandMissile.transform.position);

            spring.damper = 20;
        }
    }

    //for VR buttons
    #region 
    public void LaunchArm(InputAction.CallbackContext context)
    {
        ActivateLaunchArm();
    }
    public void PullArm(InputAction.CallbackContext context)
    {
        ActivatePullArm();
    }
    #endregion  
    void ActivateLaunchArm()
    {
        if (currentHandMissile == null)
        {
            pullingHand = false;
            currentHandMissile = Instantiate(handToLaunch, transform.position, Quaternion.Euler(MissileRotationCalc()));



            //if (hand.Aim.AimingAt)
            //    currentHandMissile.transform.LookAt(hand.Aim.AimingAt.transform.position);

            currentHandMissile.name = gameObject.name + " Missile";

            handArt.SetActive(false);
            //grab.enabled = false;
        }
    }
    void ActivatePullArm()
    {
        if (pullingHand) return;

        pullingHand = true;
        holding = false;
        playerRB.useGravity = true;
        playerMovement.enabled = true;
        currentHandMissile.GetComponent<Light>().enabled = false;
    }

    Vector3 speedCalc()
    {
        return currentHandMissile.transform.forward * S_Stats_MA.HandLaunchSpeed * 4;
    }

    Vector3 MissileRotationCalc()
    {
        GameObject getRot = new GameObject();
        Destroy(getRot, .1f);

        getRot.transform.position = handPostitions[5];
        getRot.transform.LookAt(handPostitions[0]);

        //getRot.transform.position = handPostitions[9] + playerRB.transform.position;

        //getRot.transform.LookAt(transform.localPosition + launchDirectionOffset + playerRB.transform.position);

        //Physics.Raycast(getRot.transform.position, getRot.transform.forward, out RaycastHit pointingAt, S_Stats_MA.HandLaunchReach, grabable);
        //Physics.Raycast(head.position, head.forward, out RaycastHit lookingAt, S_Stats_MA.HandLaunchReach, grabable);

        //if(pointingAt.point == Vector3.zero || lookingAt.point == Vector3.zero) return getRot.transform.eulerAngles;

        //if (Vector3.Distance(pointingAt.point, lookingAt.point) < 10)
        //{

        //}

        return getRot.transform.eulerAngles;
    }
}
