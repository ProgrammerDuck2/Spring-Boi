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

    [SerializeField] bool UsePhysics;

    [ShowIf("UsePhysics")]
    [SerializeField] float GravityMultiplier = 3.5f;
    [ShowIf("UsePhysics")]
    [SerializeField] bool VisualizeGroundCheck;
    GameObject visualizerOfGroundCheck;
    [SerializeField] float MaxVelocity = 100;
    LayerMask groundLayer;
    LayerMask stickGroundLayer;
    [HideInInspector] public bool Grounded; //ground :)

    [ShowNonSerializedField] Vector3 velocity;

    [HorizontalLine(color: EColor.Violet)]
    [Header("Other")]
    Transform bodyArt;

    CharacterController cc;

    bool Sprint;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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
        Movement();

        if(S_Settings_TB.IsVRConnected)
        {
            //Turn();
        }

        CustomPhysics();
    }

    private void CustomPhysics()
    {
        CheckGround();

        if (UsePhysics)
        {
            Gravity(); //adds gravity
            Velocity();
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

        bodyArt.eulerAngles = S_Settings_TB.IsVRConnected ? new Vector3(0, VrCamera.eulerAngles.y, 0) : bodyArt.eulerAngles = new Vector3(0, pcPov.eulerAngles.y, 0);

        move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(moveValue.x, 0, moveValue.y)) * Time.deltaTime);
        move *= Sprint ? S_Stats_MA.Speed.y : S_Stats_MA.Speed.x;

        cc.Move(move);
    }
    void Turn()
    {
        turnValue = playerInput.actions["Turn"].ReadValue<Vector2>();

        VrCameraOffset.eulerAngles += new Vector3(0, turnValue.x, 0);
    }

    void Gravity()
    {
        if(Grounded & velocity.y < 0)
        {
            velocity = new Vector3(0, -10, 0);
        } 
        else
        {
            velocity.y += (Physics.gravity.y * GravityMultiplier * Time.deltaTime) * 2;

            velocity = new Vector3(velocity.x, Mathf.Clamp(velocity.y, -MaxVelocity, MaxVelocity), velocity.z); //capping how fast you are allowed to fall
        }
    }

    float t;
    void Velocity()
    {
        velocity = new Vector3(Mathf.Lerp(velocity.x, 0, t), velocity.y, Mathf.Lerp(velocity.z, 0, t));
        t = 2f * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }
    void Jump()
    {
        if (Grounded)
        {
            velocity.y = Mathf.Sqrt(S_Stats_MA.JumpPower * 5 * -3f * Physics.gravity.y);
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

        Sprint = !Sprint;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if(VisualizeGroundCheck && UsePhysics)
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
