using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class S_Movement_TB : MonoBehaviour
{
    [Header("VR")]
    [SerializeField] InputActionProperty leftJoystick;
    [SerializeField] InputActionProperty rightButtonA;
    [SerializeField] Transform VrCamera;

    Vector2 joystickValue;

    [Space]
    [Header("PC")]
    [SerializeField] KeyCode JumpKey = KeyCode.Space;

    [Space]
    [Header("Stats")]
    [Range(1, 10)]
    [SerializeField] float Speed;

    [Space]
    [Header("Physics")]
    [SerializeField] float GravityMultiplier = 3.5f;
    [ShowNonSerializedField] Vector3 velocity;
    float MaxVelocity = 100;
    [SerializeField] LayerMask groundLayer;
    [ShowNonSerializedField] bool grounded;
    Vector3 groundCheckPos;

    [Space]
    [Header("Other")]
    Transform bodyArt;

    S_VrManager_TB vrManager;
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        vrManager = FindFirstObjectByType<S_VrManager_TB>();
        bodyArt = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Gravity();
    }

    private void FixedUpdate()
    {
        groundCheckPos = transform.position - transform.up * 0.9f;

        if (grounded != Physics.CheckSphere(groundCheckPos, 1 * 0.5f, groundLayer))
        {
            Collider[] ground = Physics.OverlapSphere(groundCheckPos, 1 * 0.5f, groundLayer);

            grounded = ground.Length >= 1 ? true : false;
            transform.parent = ground.Length >= 1 ? ground[0].transform : null;
        }
    }

    void Movement()
    {
        Vector3 move;

        if (vrManager.IsVrConnected)
        {
            joystickValue = leftJoystick.action.ReadValue<Vector2>();

            bodyArt.eulerAngles = new Vector3(0, VrCamera.eulerAngles.y, 0);

            move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(joystickValue.x, 0, joystickValue.y)) * Time.deltaTime * Speed);
        }
        else
        {
            move = bodyArt.transform.TransformDirection(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * Time.deltaTime * Speed);
        }

        cc.Move(move);
    }

    float t;
    void Gravity()
    {
        if(grounded)
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

}
