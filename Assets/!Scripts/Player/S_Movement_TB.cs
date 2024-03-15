using NaughtyAttributes;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class S_Movement_TB : MonoBehaviour
{
    public bool DebugMode;

    [Header("Input")]
    PlayerInput playerInput;

    [Header("VR")]
    Transform VrCamera;
    Transform VrCameraOffset;

    Vector2 moveValue;
    Vector2 turnValue;

    [Header("Input")]
    Transform pcPov;

    [Header("Physics")]
    [ShowIf("DebugMode")]
    [Range(0.1f, 1f)]
    public float groundCheckRadius = 0.9f;
    [ShowIf("DebugMode")]
    public Mesh Capsule;
    [ShowIf("DebugMode")]
    [Range(0, 100)]
    public float gravityStrength = 15;

    Rigidbody rb;
    [ShowIf("DebugMode")]
    public bool HighSpeed;

    [ShowIf("DebugMode")]
    public bool Grounded; //ground :)
    LayerMask groundLayer;
    LayerMask stickGroundLayer;

    [HorizontalLine(color: EColor.Violet)]
    [Header("Other")]
    Transform bodyArt;

    bool Sprint;

    [ShowIf("DebugMode")] 
    public Vector3 IRLPosOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        bodyArt = transform.GetChild(1);
        pcPov = transform.GetChild(2);

        groundLayer = LayerMask.GetMask("Ground", "StickGround");
        stickGroundLayer = LayerMask.GetMask("StickGround");

        VrCameraOffset = transform.GetChild(0).GetChild(0);
        VrCamera = VrCameraOffset.GetChild(0);

        playerInput.actions["Jump"].started += JumpPressed;

        playerInput.actions["Sprint"].started += SprintHeld;

        playerInput.actions["Crouch"].started += Crouch;
        playerInput.actions["Crouch"].canceled += Crouch;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();

        if (Grounded && rb.velocity.magnitude < new Vector3(S_Stats_MA.AerialMaxVelocity.x, 0, S_Stats_MA.AerialMaxVelocity.z).magnitude / 2)
        {
            HighSpeed = false;
        }

        if (S_Settings_TB.IsVRConnected)
        {
            transform.position -= IRLPosOffset;
            IRLPosOffset = Vector3.zero;
            IRLPosOffset += new Vector3(playerInput.actions["IRLPosition"].ReadValue<Vector3>().x, 0, playerInput.actions["IRLPosition"].ReadValue<Vector3>().z);
            transform.position += IRLPosOffset;
        }

    }
    private void FixedUpdate()
    {
        if (!HighSpeed)
        {
            if (Sprint)
            {
                rb.velocity = Clamp(rb.velocity, S_Stats_MA.MaxVelocity * 2);
            }
            else
            {
                rb.velocity = Clamp(rb.velocity, S_Stats_MA.MaxVelocity);
            }
        }
        else
        {
            rb.velocity = Clamp(rb.velocity, S_Stats_MA.AerialMaxVelocity);
        }

        Movement();

        rb.AddForce(Vector3.down * gravityStrength, ForceMode.Acceleration);

        if (S_Settings_TB.IsVRConnected)
        {
            //Turn();
        }
    }

    void CheckGround()
    {

        if (Grounded != Physics.CheckCapsule(groundCheckBottomPos(), groundCheckTopPos(), groundCheckRadius, groundLayer))
        {
            Collider[] ground = Physics.OverlapCapsule(groundCheckBottomPos(), groundCheckTopPos(), groundCheckRadius, groundLayer);

            Grounded = !Grounded;

            if(ground.Length >= 1)
            {
                //transform.parent =  ground[0].transform;
                print("landed on stick ground, Object is: " + ground[0].gameObject);
            }
            else
            {
                //transform.parent = null;
            }
        }
    }

    private void OnDestroy()
    {
        playerInput.actions["Jump"].started -= JumpPressed;
        playerInput.actions["Sprint"].started -= SprintHeld;
        playerInput.actions["Crouch"].started -= Crouch;
        playerInput.actions["Crouch"].canceled -= Crouch;
    }
    //Movements
    #region
    void Movement()
    {
        Vector3 move;
        moveValue = playerInput.actions["Move"].ReadValue<Vector2>();

        bodyArt.eulerAngles = S_Settings_TB.IsVRConnected ? new Vector3(0, VrCamera.eulerAngles.y, 0) : bodyArt.eulerAngles = new Vector3(0, pcPov.eulerAngles.y, 0);

        move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(moveValue.x, 0, moveValue.y)));
        move *= Sprint ? S_Stats_MA.Speed.y : S_Stats_MA.Speed.x;

        rb.velocity += move;
    }

    void Turn()
    {
        turnValue = playerInput.actions["Turn"].ReadValue<Vector2>();

        transform.eulerAngles += new Vector3(0, turnValue.x * S_Stats_MA.TurnSpeed * Time.fixedDeltaTime, 0);
    }

    void Jump()
    {
        if (Grounded)
        {
            print("Jump");
            rb.velocity += Vector3.up * S_Stats_MA.JumpPower;
        }
    }
    void Crouch(InputAction.CallbackContext context)
    {
        transform.position += !context.canceled ? new Vector3(0, .5f, 0) : new Vector3(0, -.5f, 0);
        transform.localScale = !context.canceled ? new Vector3(1, .5f, 1) : Vector3.one;
    }
    #endregion

    //InputActions
    #region
    void JumpPressed(InputAction.CallbackContext context)
    {
        Jump();
    }
    void SprintHeld(InputAction.CallbackContext context)
    {

        Sprint = !Sprint;
    }
    #endregion

    //Gizmos
    #region
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = new Color(0, 1, 0, .5f);
            Gizmos.DrawMesh(Capsule, groundCheckTopPos() + groundCheckBottomPos() - transform.up, transform.rotation, groundCheckSize());
        }
    }
    #endregion

    //Calculations
    #region 
    Vector3 Clamp(Vector3 toClamp, Vector3 MaxVelocity)
    {
        return new Vector3(
            Mathf.Clamp(toClamp.x, -MaxVelocity.x, MaxVelocity.x),
            Mathf.Clamp(toClamp.y, -MaxVelocity.y, MaxVelocity.y),
            Mathf.Clamp(toClamp.z, -MaxVelocity.z, MaxVelocity.z));
    }
    Vector3 groundCheckTopPos()
    {
        return transform.position - transform.up * 0.6f;
    }
    Vector3 groundCheckBottomPos()
    {
        return transform.position + transform.up * 0.5f;
    }
    Vector3 groundCheckSize()
    {
        return new Vector3(transform.localScale.x * groundCheckRadius, transform.localScale.y, transform.localScale.z * groundCheckRadius);
    }
    #endregion
}
