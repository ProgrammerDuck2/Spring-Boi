using NaughtyAttributes;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class S_Movement_TB : MonoBehaviour
{
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

    Rigidbody rb;
    [HideInInspector] public bool HighSpeed;

    [HideInInspector] public bool Grounded; //ground :)
    public bool DebugMode;
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

        if (!HighSpeed)
        {
            //rb.drag = 3;
            rb.velocity = Clamp(rb.velocity, S_Stats_MA.MaxVelocity);
        } else
        {
            //rb.drag = 1;
            rb.velocity = Clamp(rb.velocity, S_Stats_MA.AerialMaxVelocity);
        }

        Movement();

        if (S_Settings_TB.IsVRConnected)
        {
            //Turn();
        }

        if (Grounded && rb.velocity.magnitude < new Vector3(S_Stats_MA.AerialMaxVelocity.x, 0, S_Stats_MA.AerialMaxVelocity.z).magnitude / 2)
        {
            HighSpeed = false;
        }
    }

    void CheckGround()
    {
        if (Grounded != Physics.CheckSphere(groundCheckPos(), groundCheckSize(), groundLayer))
        {
            Collider[] ground = Physics.OverlapSphere(groundCheckPos(), 1 * 0.5f, stickGroundLayer);

            Grounded = !Grounded;
            transform.parent = ground.Length >= 1 ? ground[0].transform : null;
        }
    }

    private void OnDestroy()
    {
        playerInput.actions["Jump"].started -= JumpPressed;
        playerInput.actions["Sprint"].started -= SprintHeld;
        playerInput.actions["Crouch"].started -= Crouch;
        playerInput.actions["Crouch"].canceled -= Crouch;
    }

    void Movement()
    {
        Vector3 move;
        moveValue = playerInput.actions["Move"].ReadValue<Vector2>();

        if(S_Settings_TB.IsVRConnected)
        {
            transform.position -= IRLPosOffset;
            IRLPosOffset = Vector3.zero;
            IRLPosOffset += new Vector3(playerInput.actions["IRLPosition"].ReadValue<Vector3>().x, 0, playerInput.actions["IRLPosition"].ReadValue<Vector3>().z);
            transform.position += IRLPosOffset;
        }

        bodyArt.eulerAngles = S_Settings_TB.IsVRConnected ? new Vector3(0, VrCamera.eulerAngles.y, 0) : bodyArt.eulerAngles = new Vector3(0, pcPov.eulerAngles.y, 0);

        move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(moveValue.x, 0, moveValue.y)));
        move *= Sprint ? S_Stats_MA.Speed.y : S_Stats_MA.Speed.x;

        rb.velocity += move;
    }

    void Turn()
    {
        turnValue = playerInput.actions["Turn"].ReadValue<Vector2>();

        VrCameraOffset.eulerAngles += new Vector3(0, turnValue.x, 0);
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
    Vector3 Clamp(Vector3 toClamp, Vector3 MaxVelocity)
    {
        return new Vector3(
            Mathf.Clamp(toClamp.x, -MaxVelocity.x, MaxVelocity.x),
            Mathf.Clamp(toClamp.y, -MaxVelocity.y, MaxVelocity.y),
            Mathf.Clamp(toClamp.z, -MaxVelocity.z, MaxVelocity.z));
    }

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

    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundCheckPos(), groundCheckSize());
        }
    }

    Vector3 groundCheckPos()
    {
        return transform.position - transform.up;
    }
    float groundCheckSize()
    {
        return .5f * 0.3f;
    }
}
