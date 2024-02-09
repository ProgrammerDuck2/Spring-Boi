using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(S_Grab_TB))]
[RequireComponent(typeof(S_LaunchArms_TB))]
[RequireComponent(typeof(S_Punch_TB))]
public class S_Hand_TB : MonoBehaviour
{
    [Required]
    public GameObject Player;

    public GameObject otherController;
    [HorizontalLine(color: EColor.Violet)]

    [Header("Components")]
    [HideInInspector] public S_Grab_TB grab;
    [HideInInspector] public S_LaunchArms_TB launchArms;

    [HorizontalLine(color: EColor.Violet)]

    [Header("Contolls")]
    [SerializeField] InputActionProperty trigger;
    [SerializeField] InputActionProperty grip;
    [HideInInspector] public bool triggerActivated = false;
    [HideInInspector] public bool gripActivated = false;
    [HideInInspector] public bool grabActivated = false;

    [SerializeField] InputActionProperty inputPosition;
    [SerializeField] InputActionProperty inputRotation;
    [HideInInspector] public Vector3 controllerPosition;
    [HideInInspector] public Quaternion controllerRotation;

    [SerializeField] InputActionProperty buttonToLaunch;


    // Start is called before the first frame update
    void Start()
    {
        grab = GetComponent<S_Grab_TB>();
        launchArms = GetComponent<S_LaunchArms_TB>();

        trigger.action.started += toggleTrigger;
        trigger.action.canceled += toggleTrigger;
        grip.action.started += toggleGrip;
        grip.action.canceled += toggleGrip;

        buttonToLaunch.action.started += launchArms.LaunchArm;
        buttonToLaunch.action.canceled += launchArms.PullArm;
    }

    // Update is called once per frame
    void Update()
    {
        controllerPosition = inputPosition.action.ReadValue<Vector3>();
        controllerRotation = inputRotation.action.ReadValue<Quaternion>();
    }
    private void LateUpdate()
    {
        grabActivated = triggerActivated && gripActivated ? true : false;
    }

    void toggleTrigger(InputAction.CallbackContext context)
    {
        triggerActivated = !triggerActivated;
    }
    void toggleGrip(InputAction.CallbackContext context)
    {
        gripActivated = !gripActivated;
    }
}
