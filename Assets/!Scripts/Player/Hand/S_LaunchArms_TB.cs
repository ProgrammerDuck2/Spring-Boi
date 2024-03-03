using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(S_Hand_TB))]
public class S_LaunchArms_TB : MonoBehaviour
{
    S_Hand_TB hand;
    Rigidbody playerRB;
    S_Movement_TB playerMovement;

    S_Grab_TB grab;

    [Required]
    [SerializeField] GameObject handToLaunch;
    GameObject currentHandMissile;

    GameObject handArt;

    [SerializeField] bool pullingHand;
    bool holding;

    [SerializeField] float cooldown;

    [SerializeField] Vector3 launchDirectionOffset;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<S_Hand_TB>();
        playerMovement = hand.Player.GetComponent<S_Movement_TB>();
        playerRB = hand.Player.GetComponent<Rigidbody>();
        handArt = transform.GetComponent<ActionBasedController>().modelParent.gameObject;

        grab = GetComponent<S_Grab_TB>();
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
        if (!hand.GripActivated) return;
        if (hand.TriggerActivated) return;
        if (hand.handPostitions.Count <= 9) return;

        float forceRequirement = .4f;

        if (Vector3.Distance(hand.handPostitions[0], hand.handPostitions[hand.handPostitions.Count - 1]) > forceRequirement)
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
        if (!hand.GripActivated)
        {
            holding = false;
        }

        if (Physics.CheckSphere(currentHandMissile.transform.position, .5f, hand.grabable))
        {
            if(hand.GripActivated)
            {
                HoldOnto();
            } else
            {
                ActivatePullArm();
            }
        }

        if (!holding)
        {
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
            grab.enabled = true;
            playerRB.useGravity = true;

            if (!hand.OtherController.GetComponent<S_Grab_TB>().holding)
            {
                playerMovement.enabled = true;
            }

            Destroy(currentHandMissile);
        }
    }
    void HoldOnto()
    {
        SpringJoint spring = currentHandMissile.GetComponent<SpringJoint>();

        if (holding != true)
        {
            holding = true;
            hand.HapticFeedback.TriggerHaptic(.5f, .1f, GetComponent<ActionBasedController>());

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
            hand.Player.GetComponent<S_Movement_TB>().HighSpeed = true;
        }
        else
        {
            playerRB.useGravity = true;
            spring.maxDistance = Vector3.Distance(hand.Player.transform.position, currentHandMissile.transform.position);

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
            grab.enabled = false;
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
        getRot.transform.position = hand.handPostitions[9];
        getRot.transform.LookAt(hand.handPostitions[0]);
        Destroy(getRot, .1f);

        return getRot.transform.eulerAngles + launchDirectionOffset;
    }
}
