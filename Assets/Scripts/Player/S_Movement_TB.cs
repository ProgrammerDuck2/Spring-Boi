using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class S_Movement_TB : MonoBehaviour
{
    [Header("VR")]
    [SerializeField] InputActionProperty leftJoystick;
    [SerializeField] InputActionProperty RightJoystick;
    [SerializeField] InputActionProperty rightPrimaryButton;
    [SerializeField] InputActionProperty rightSecondaryButton;

    [SerializeField] Transform VrCamera;

    Vector2 moveValue;
    Vector2 turnValue;


    [Space]
    [HorizontalLine(color: EColor.Violet)]
    [Header("PC")]
    PlayerInput PlayerInput;

    [Space]
    [HorizontalLine(color: EColor.Violet)]
    [Header("Stats")]
    [MinMaxSlider(0f, 30f)] 
    [SerializeField] Vector2 Speed; //X is walking speed, Y is running speed
    [Range(1, 10)]
    [SerializeField] float JumpPower;

    [Space]
    [HorizontalLine(color: EColor.Violet)]
    [Header("Physics")]
    [SerializeField] bool UsePhysics;
    [ShowIf("UsePhysics")]
    [SerializeField] float GravityMultiplier = 3.5f;
    [ShowIf("UsePhysics")]
    [ShowNonSerializedField] Vector3 velocity;
    float MaxVelocity = 100;
    [ShowIf("UsePhysics")]
    [SerializeField] LayerMask groundLayer;
    [ShowIf("UsePhysics")]
    [SerializeField] LayerMask stickGroundLayer;
    [ShowIf("UsePhysics")]
    public bool Grounded; //ground :)
    Vector3 groundCheckPos;

    [HorizontalLine(color: EColor.Violet)]
    [Header("Other")]
    Transform bodyArt;

    CharacterController cc;

    bool Sprint;

    // Start is called before the first frame update
    void Start()
    {
        rightPrimaryButton.action.started += JumpPressed;

        rightSecondaryButton.action.started += SprintHeld;

        cc = GetComponent<CharacterController>();
        PlayerInput = GetComponent<PlayerInput>();
        bodyArt = transform.GetChild(2);

        PlayerInput.actions["Jump"].started += JumpPressed;

        PlayerInput.actions["Sprint"].started += SprintHeld;

        PlayerInput.actions["Crouch"].started += Crouch;
        PlayerInput.actions["Crouch"].canceled += Crouch;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(S_Settings_TB.IsVRConnected)
        {
            Turn();
        }

        if(UsePhysics)
            Gravity();
    }

    private void FixedUpdate()
    {
        groundCheckPos = transform.position - transform.up * 0.9f;

        if (Grounded != Physics.CheckSphere(groundCheckPos, 1 * 0.5f, groundLayer))
        {
            Collider[] ground = Physics.OverlapSphere(groundCheckPos, 1 * 0.5f, stickGroundLayer);

            Grounded = !Grounded;
            transform.parent = ground.Length >= 1 ? ground[0].transform : null;
        }
    }

    private void OnDestroy()
    {
        rightPrimaryButton.action.started -= JumpPressed;
        rightSecondaryButton.action.started -= SprintHeld;
    }

    void Movement()
    {
        Vector3 move;

        if (S_Settings_TB.IsVRConnected)
        {
            moveValue = leftJoystick.action.ReadValue<Vector2>();

            bodyArt.eulerAngles = new Vector3(0, VrCamera.eulerAngles.y, 0);

            move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(moveValue.x, 0, moveValue.y)) * Time.deltaTime);
        }
        else
        {
            move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * Time.deltaTime);
        }

        move *= Sprint ? Speed.y : Speed.x;

        cc.Move(move);
    }
    void Turn()
    {
        turnValue = RightJoystick.action.ReadValue<Vector2>();

        VrCamera.Rotate(turnValue * 10);
    }

    float t;
    void Gravity()
    {
        if(Grounded & velocity.y < 0)
        {
            velocity = Vector3.zero;
        } else
        {
            velocity.y += Physics.gravity.y * GravityMultiplier * Time.deltaTime;

            velocity = new Vector3(Mathf.Lerp(velocity.x, 0, t), velocity.y + Physics.gravity.y * GravityMultiplier * Time.deltaTime, Mathf.Lerp(velocity.z, 0, t));

            velocity = new Vector3(velocity.x, Mathf.Clamp(velocity.y, -MaxVelocity, MaxVelocity), velocity.z);

            t = 2f * Time.deltaTime;
        }

        cc.Move(velocity * Time.deltaTime);
    }
    void Jump()
    {
        if (Grounded)
        {
            velocity.y = Mathf.Sqrt(JumpPower * 5 * -3f * Physics.gravity.y);
        }
    }
    void Crouch(InputAction.CallbackContext context)
    {
        transform.position += !context.canceled ?  new Vector3(0, .5f, 0) : new Vector3(0, -.5f, 0);
        transform.localScale = !context.canceled ? new Vector3(1, .5f, 1) : Vector3.one;
    }

    //InputActions
    #region
    void JumpPressed(InputAction.CallbackContext context)
    {
        Jump();
    }
    void SprintHeld(InputAction.CallbackContext context)
    {
        print("sprint");
        Sprint = !Sprint;
    }
    #endregion
}
