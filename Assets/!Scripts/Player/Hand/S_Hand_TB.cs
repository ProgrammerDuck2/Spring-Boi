using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

[RequireComponent(typeof(S_Grab_TB))]
[RequireComponent(typeof(S_LaunchArms_TB))]
[RequireComponent(typeof(S_Punch_TB))]
[RequireComponent(typeof(S_HandAim_TB))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(S_HandInteract_TBMA))]
[RequireComponent(typeof(S_AnimateHand_TB))]
public class S_Hand_TB : MonoBehaviour
{
    [HideInInspector] public GameObject Player;
    [HideInInspector] S_Movement_TB PlayerMovement;
    public LayerMask grabable;
    [HideInInspector] public PlayerInput playerInput;
    [Required]
    public GameObject OtherController;

    public GameObject HandArt;
    public Animator HandArtAnimation;

    public bool DebugMode;

    [Header("Tracking")]
    [SerializeField] Vector3 HandOffset;

    [Header("Input")]
    [SerializeField] bool useReference;
    [ShowIf("useReference")]
    [SerializeField] InputActionProperty pos;
    [ShowIf("useReference")]
    [SerializeField] InputActionProperty rot;
    [ShowIf("useReference")]
    [SerializeField] InputActionProperty grip; 
    [ShowIf("useReference")]
    [SerializeField] InputActionProperty trigger;

    [HorizontalLine(color: EColor.Violet)]

    [Header("Components")]
    [HideInInspector] public S_Grab_TB Grab;
    [HideInInspector] public S_LaunchArms_TB LaunchArms;
    [HideInInspector] public S_Punch_TB Punch;
    [HideInInspector] public S_HandAim_TB Aim;
    [HideInInspector] public S_HapticFeedback_TB HapticFeedback;
    [HideInInspector] public S_HandInteract_TBMA Interact;
    [HideInInspector] public S_AnimateHand_TB Anim;
    [HorizontalLine(color: EColor.Violet)]

    [Header("Contolls")]
    [HideInInspector] public bool TriggerActivated = false;
    [HideInInspector] public bool GripActivated = false;
    [HideInInspector] public bool GrabActivated = false; 

    [HideInInspector] public Vector3 ControllerPosition;
    [HideInInspector] public Quaternion ControllerRotation;

    [Header("Motion")]
    [ShowIf("DebugMode")]
    public List<Vector3> handPostitions = new List<Vector3>();
    [ShowIf("DebugMode")]
    public List<Vector3> handRotations = new List<Vector3>();
    [ShowIf("DebugMode")]
    public List<Vector3> handForwards = new List<Vector3>();
    float timer;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        Grab = GetComponent<S_Grab_TB>();
        LaunchArms = GetComponent<S_LaunchArms_TB>();
        Punch = GetComponent<S_Punch_TB>();
        Aim = GetComponent<S_HandAim_TB>();
        HapticFeedback = Player.GetComponent<S_HapticFeedback_TB>();
        PlayerMovement = Player.GetComponent<S_Movement_TB>();
        Interact = GetComponent<S_HandInteract_TBMA>();
        Anim = GetComponent<S_AnimateHand_TB>();

        playerInput = GetComponent<PlayerInput>();

        if (!useReference)
        {
            playerInput.actions["Pinch"].started += toggleTrigger;
            playerInput.actions["Pinch"].canceled += toggleTrigger;
            playerInput.actions["Grip"].started += toggleGrip;
            playerInput.actions["Grip"].canceled += toggleGrip;

            playerInput.actions["Launch"].started += LaunchArms.LaunchArm;
            playerInput.actions["Launch"].canceled += LaunchArms.PullArm;

            playerInput.actions["Interact"].started += Interact.ClickEnter;
            playerInput.actions["Interact"].performed += Interact.Click;
            playerInput.actions["Interact"].canceled += Interact.ClickExit;
        } else
        {
            trigger.action.started += toggleTrigger;
            trigger.action.canceled += toggleTrigger;
            grip.action.started += toggleGrip;
            grip.action.canceled += toggleGrip;

            trigger.action.started += Interact.ClickEnter;
            trigger.action.performed += Interact.Click;
            trigger.action.canceled += Interact.ClickExit;
        }

        Anim.hand = this;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        HandArt = transform.GetChild(transform.childCount - 1).GetChild(0).gameObject;
        HandArtAnimation = HandArt.GetComponent<Animator>();
        print("no hand");

        //playerInput.SwitchCurrentControlScheme("XR");
    }

    // Update is called once per frame
    void Update()
    {
        if(!useReference)
        {
            transform.localPosition = playerInput.actions["Position"].ReadValue<Vector3>() + HandOffset - PlayerMovement.IRLPosOffset;
            transform.localRotation = playerInput.actions["Rotation"].ReadValue<Quaternion>();

            if (playerInput.actions["Position"].ReadValue<Vector3>() == Vector3.zero)
            {
                Debug.LogWarning("Controller position not found");
                HandArt.transform.GetChild(0).gameObject.SetActive(false);

            }
            else
            {
                HandArt.transform.GetChild(0).gameObject.SetActive(true);
            }
        } else
        {
            transform.localPosition = pos.action.ReadValue<Vector3>() + HandOffset - PlayerMovement.IRLPosOffset;
            transform.localRotation = rot.action.ReadValue<Quaternion>();

            if (pos.action.ReadValue<Vector3>() == Vector3.zero)
            {
                Debug.LogWarning("Controller position not found");
                HandArt.transform.GetChild(0).gameObject.SetActive(false);

            }
            else
            {
                HandArt.transform.GetChild(0).gameObject.SetActive(true);
            }
        }


        ControllerPosition = transform.localPosition;
        ControllerRotation = transform.localRotation;

        timer += Time.deltaTime * 2;

        if (timer > .1f)
        {
            handPostitions.Insert(0, transform.localPosition);
            handRotations.Insert(0, ControllerRotation.eulerAngles);
            handForwards.Insert(0, transform.forward);
        }

        if(handPostitions.Count > 10)
        {
            handPostitions.Remove(handPostitions[10]);
            handRotations.Remove(handRotations[10]);
            handForwards.Remove(handForwards[10]);
        }
    }
    private void LateUpdate()
    {
        //transform.position -= PlayerMovement.IRLPosOffset;
        GrabActivated = TriggerActivated && GripActivated ? true : false;
    }

    void toggleTrigger(InputAction.CallbackContext context)
    {
        TriggerActivated = !TriggerActivated;
    }
    void toggleGrip(InputAction.CallbackContext context)
    {
        GripActivated = !GripActivated;
    }

    public Vector3 GetAverageVector3(List<Vector3> Vector3s)
    {
        Vector3 average = Vector3.zero;

        for (int i = 0; i < Vector3s.Count; i++)
        {
            average += Vector3s[i];
        }

        average /= handForwards.Count;

        return average;
    }

    [Button]
    public void ChangeSchemeToXR()
    {
        playerInput.SwitchCurrentControlScheme("XR");
    }
    [Button]
    public void ChangeSchemeToKeyboard()
    {
        playerInput.SwitchCurrentControlScheme("KeyboardMouse");
    }
}
