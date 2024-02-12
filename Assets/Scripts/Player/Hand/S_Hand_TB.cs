using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(S_Grab_TB))]
[RequireComponent(typeof(S_LaunchArms_TB))]
[RequireComponent(typeof(S_Punch_TB))]
public class S_Hand_TB : MonoBehaviour
{
    [Required]
    public GameObject Player;
    PlayerInput playerInput;

    public GameObject OtherController;
    [HorizontalLine(color: EColor.Violet)]

    [Header("Components")]
    [HideInInspector] public S_Grab_TB Grab;
    [HideInInspector] public S_LaunchArms_TB LaunchArms;
    [HideInInspector] public S_Punch_TB Punch;

    [HorizontalLine(color: EColor.Violet)]

    [Header("Contolls")]
    [SerializeField] string ActionMap;
    [SerializeField] InputActionProperty trigger;
    [SerializeField] InputActionProperty grip;
    [HideInInspector] public bool TriggerActivated = false;
    [HideInInspector] public bool GripActivated = false;
    [HideInInspector] public bool GrabActivated = false;

    [SerializeField] InputActionProperty inputPosition;
    [SerializeField] InputActionProperty inputRotation;
    [HideInInspector] public Vector3 ControllerPosition;
    [HideInInspector] public Quaternion ControllerRotation;

    [SerializeField] InputActionProperty buttonToLaunch;


    // Start is called before the first frame update
    void Start()
    {
        Grab = GetComponent<S_Grab_TB>();
        LaunchArms = GetComponent<S_LaunchArms_TB>();
        Punch = GetComponent<S_Punch_TB>();

        playerInput = Player.GetComponent<PlayerInput>();

        playerInput.SwitchCurrentActionMap(ActionMap);

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
        ControllerPosition = inputPosition.action.ReadValue<Vector3>();
        ControllerRotation = inputRotation.action.ReadValue<Quaternion>();
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
