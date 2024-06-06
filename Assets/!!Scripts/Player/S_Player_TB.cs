using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class S_Player_TB : MonoBehaviour
{
    public bool DebugMode;


    public bool Grounded { get { return physics.CheckGround(); } } //ground :)
    public LayerMask groundLayer { get { return LayerMask.GetMask("Ground", "StickGround"); } }
    public LayerMask stickGroundLayer { get { return LayerMask.GetMask("StickGround"); } }
    public GameObject playerArt {  get { return transform.GetChild(1).gameObject; } }

    [Header("Player Components")]
    [HideInInspector] public S_Movement_TB movement;
    [HideInInspector] public S_Jump_TB jump;
    [HideInInspector] public S_PlayerPhysics_TB physics;
    [HideInInspector] public S_Crouch_TB crouch;

    [HideInInspector] public Rigidbody playerRigidbody;

    [Header("Input")]
    [HideInInspector] public PlayerInput playerInput;

    public Vector3 IRLPosition
    {
        get { return playerInput.actions["IRLPosition"].ReadValue<Vector3>(); }
    }

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<S_Movement_TB>();
        jump = GetComponent<S_Jump_TB>();
        playerRigidbody = GetComponent<Rigidbody>();
        crouch = GetComponent<S_Crouch_TB>();

        playerInput = GetComponent<PlayerInput>();
        //playerInput.actions["Sprint"].started += movement.SprintHeld;
    }

    private void OnValidate()
    {
        physics = GetComponent<S_PlayerPhysics_TB>();
    }
}
