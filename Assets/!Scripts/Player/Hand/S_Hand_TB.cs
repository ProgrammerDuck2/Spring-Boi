using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class S_Hand_TB : MonoBehaviour
{
    public GameObject player { get { return FindFirstObjectByType<S_Movement_TB>().gameObject; } }
    [HideInInspector] public S_Movement_TB playerMovement;
    [HideInInspector] public Rigidbody playerRB;
    [HideInInspector] public LayerMask grabable;
    [HideInInspector] public PlayerInput playerInput;

    public S_Hand_TB otherController
    {
        get;
        private set;
    }

    public GameObject HandArt
    {
        get { return transform.GetChild(transform.childCount - 1).GetChild(0).gameObject;  }
    }
    public Animator HandArtAnimation
    {
        get { return HandArt.GetComponent<Animator>(); }
    }

    public bool DebugMode;

    public S_Grab_TB grab { get; private set; }
    public S_LaunchArms_TB launchArms { get; private set; }
    public S_Punch_TB punch { get; private set; }
    public S_HandAim_TB aim { get; private set; }
    public S_HapticFeedback_TB hapticFeedback { get; private set; }
    public S_UIInteract_TBMA interact { get; private set; }
    public S_AnimateHand_TB anim { get; private set; }
    public S_HandInput_TB handInput { get; private set; }
    public S_HandMotion_TB motion { get; private set; }
    public S_HandPostion_TB handPostioning { get; private set; }

    public List<Vector3> handPostitions
    {
        get { return motion.handPos; }
    }
    public List<Vector3> handRotations
    {
        get { return motion.handRot; }
    }
    public List<Vector3> handForwards
    {
        get { return motion.handForwrds; }
    }

    public void Awake()
    {

        grabable = LayerMask.GetMask("Ground", "StickGround", "Grabable", "Enemy");

        grab = GetComponent<S_Grab_TB>();
        launchArms = GetComponent<S_LaunchArms_TB>();
        punch = GetComponent<S_Punch_TB>();
        aim = GetComponent<S_HandAim_TB>();
        interact = GetComponent<S_UIInteract_TBMA>();
        anim = GetComponent<S_AnimateHand_TB>();
        handInput = GetComponent<S_HandInput_TB>();
        motion = GetComponent<S_HandMotion_TB>();
        handPostioning = GetComponent<S_HandPostion_TB>();

        otherController = findOtherController();

        hapticFeedback = player.GetComponent<S_HapticFeedback_TB>();
        playerMovement = player.GetComponent<S_Movement_TB>();
        playerRB = player.GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        //playerInput.SwitchCurrentControlScheme("XR");
    }

    S_Hand_TB findOtherController()
    {
        S_Hand_TB[] bothControllers = Resources.FindObjectsOfTypeAll(typeof(S_Hand_TB)) as S_Hand_TB[];

        for (int i = 0; i < bothControllers.Length; i++)
        {
            if (bothControllers[i].name != name)
                return bothControllers[i];
        }

        return bothControllers[0];
    }
}
