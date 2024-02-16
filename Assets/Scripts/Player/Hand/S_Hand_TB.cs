using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(S_Grab_TB))]
[RequireComponent(typeof(S_LaunchArms_TB))]
[RequireComponent(typeof(S_Punch_TB))]
[RequireComponent(typeof(PlayerInput))]
public class S_Hand_TB : MonoBehaviour
{
    [Required]
    public GameObject Player;
    [HideInInspector] public PlayerInput playerInput;

    public GameObject OtherController;
    [HorizontalLine(color: EColor.Violet)]

    [Header("Components")]
    [HideInInspector] public S_Grab_TB Grab;
    [HideInInspector] public S_LaunchArms_TB LaunchArms;
    [HideInInspector] public S_Punch_TB Punch;

    [HorizontalLine(color: EColor.Violet)]

    [Header("Contolls")]
    [HideInInspector] public bool TriggerActivated = false;
    [HideInInspector] public bool GripActivated = false;
    [HideInInspector] public bool GrabActivated = false; 

    [HideInInspector] public Vector3 ControllerPosition;
    [HideInInspector] public Quaternion ControllerRotation;


    // Start is called before the first frame update
    void Start()
    {
        Grab = GetComponent<S_Grab_TB>();
        LaunchArms = GetComponent<S_LaunchArms_TB>();
        Punch = GetComponent<S_Punch_TB>();

        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Pinch"].started += toggleTrigger;
        playerInput.actions["Pinch"].canceled += toggleTrigger;
        playerInput.actions["Grip"].started += toggleGrip;
        playerInput.actions["Grip"].canceled += toggleGrip;

        playerInput.actions["Launch"].started += LaunchArms.LaunchArm;
        playerInput.actions["Launch"].canceled += LaunchArms.PullArm;

        print(playerInput.currentActionMap);
    }

    // Update is called once per frame
    void Update()
    {
        ControllerPosition = playerInput.actions["Position"].ReadValue<Vector3>();
        ControllerRotation = playerInput.actions["Rotation"].ReadValue<Quaternion>();
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
