using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

[RequireComponent(typeof(S_Grab_TB))]
[RequireComponent(typeof(S_LaunchArms_TB))]
[RequireComponent(typeof(S_Punch_TB))]
[RequireComponent(typeof(S_HandAim_TB))]
[RequireComponent(typeof(PlayerInput))]
public class S_Hand_TB : MonoBehaviour
{
    [Required]
    public GameObject Player;
    public LayerMask grabable;
    [HideInInspector] public PlayerInput playerInput;
    public GameObject OtherController;
    [HorizontalLine(color: EColor.Violet)]

    [Header("Components")]
    [HideInInspector] public S_Grab_TB Grab;
    [HideInInspector] public S_LaunchArms_TB LaunchArms;
    [HideInInspector] public S_Punch_TB Punch;
    [HideInInspector] public S_HandAim_TB Aim;

    [HorizontalLine(color: EColor.Violet)]

    [Header("Contolls")]
    [HideInInspector] public bool TriggerActivated = false;
    [HideInInspector] public bool GripActivated = false;
    [HideInInspector] public bool GrabActivated = false; 

    [HideInInspector] public Vector3 ControllerPosition;
    [HideInInspector] public Quaternion ControllerRotation;

    [Header("Motion")]
    public List<Vector3> handPostitions = new List<Vector3>();
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        Grab = GetComponent<S_Grab_TB>();
        LaunchArms = GetComponent<S_LaunchArms_TB>();
        Punch = GetComponent<S_Punch_TB>();
        Aim = GetComponent<S_HandAim_TB>();

        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Pinch"].started += toggleTrigger;
        playerInput.actions["Pinch"].canceled += toggleTrigger;
        playerInput.actions["Grip"].started += toggleGrip;
        playerInput.actions["Grip"].canceled += toggleGrip;

        playerInput.actions["Launch"].started += LaunchArms.LaunchArm;
        playerInput.actions["Launch"].canceled += LaunchArms.PullArm;
    }

    // Update is called once per frame
    void Update()
    {
        ControllerPosition = playerInput.actions["Position"].ReadValue<Vector3>();
        ControllerRotation = playerInput.actions["Rotation"].ReadValue<Quaternion>();

        timer += Time.deltaTime;

        if (timer > .1f)
        {
            handPostitions.Insert(0, ControllerPosition);
        }

        if(handPostitions.Count > 10)
        {
            handPostitions.Remove(handPostitions[10]);
        }
    }
    private void LateUpdate()
    {
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
}
