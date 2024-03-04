using NaughtyAttributes;
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
public class S_Hand_TB : MonoBehaviour
{
    [HideInInspector] public GameObject Player;
    [HideInInspector] S_Movement_TB PlayerMovement;
    public LayerMask grabable;
    [HideInInspector] public PlayerInput playerInput;
    [Required]
    public GameObject OtherController;

    public bool DebugMode;

    [Header("Tracking")]
    [SerializeField] Vector3 HandOffset;

    [HorizontalLine(color: EColor.Violet)]

    [Header("Components")]
    [HideInInspector] public S_Grab_TB Grab;
    [HideInInspector] public S_LaunchArms_TB LaunchArms;
    [HideInInspector] public S_Punch_TB Punch;
    [HideInInspector] public S_HandAim_TB Aim;
    [HideInInspector] public S_HapticFeedback_TB HapticFeedback;
    [HideInInspector] public S_HandInteract_TBMA Interact;
    [HorizontalLine(color: EColor.Violet)]

    [Header("Contolls")]
    [HideInInspector] public bool TriggerActivated = false;
    [HideInInspector] public bool GripActivated = false;
    [HideInInspector] public bool GrabActivated = false; 

    [HideInInspector] public Vector3 ControllerPosition;
    [HideInInspector] public Quaternion ControllerRotation;

    [Header("Motion")]
    public List<Vector3> handPostitions = new List<Vector3>();
    public List<Vector3> handRotations = new List<Vector3>();
    public List<Vector3> handForwards = new List<Vector3>();
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        Grab = GetComponent<S_Grab_TB>();
        LaunchArms = GetComponent<S_LaunchArms_TB>();
        Punch = GetComponent<S_Punch_TB>();
        Aim = GetComponent<S_HandAim_TB>();
        HapticFeedback = Player.GetComponent<S_HapticFeedback_TB>();
        PlayerMovement = Player.GetComponent<S_Movement_TB>();
        Interact = GetComponent<S_HandInteract_TBMA>();

        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Pinch"].started += toggleTrigger;
        playerInput.actions["Pinch"].canceled += toggleTrigger;
        playerInput.actions["Grip"].started += toggleGrip;
        playerInput.actions["Grip"].canceled += toggleGrip;

        playerInput.actions["Launch"].started += LaunchArms.LaunchArm;
        playerInput.actions["Launch"].canceled += LaunchArms.PullArm;

        playerInput.actions["Interact"].started += Interact.Interact;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = playerInput.actions["Position"].ReadValue<Vector3>() + HandOffset - PlayerMovement.IRLPosOffset;
        transform.localRotation = playerInput.actions["Rotation"].ReadValue<Quaternion>();

        //print(playerInput.actions["Position"].ReadValue<Vector3>());

        ControllerPosition = transform.localPosition;
        ControllerRotation = transform.localRotation;

        timer += Time.deltaTime;

        if (timer > .1f)
        {
            handPostitions.Insert(0, ControllerPosition);
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
}
