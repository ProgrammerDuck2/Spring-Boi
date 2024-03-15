using NaughtyAttributes;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class S_Movement_TB : S_Player_TB
{
    [Header("VR")]
    Transform VrCamera;
    Transform VrCameraOffset;

    Vector2 moveValue;
    Vector2 turnValue;

    [Header("Input")]
    Transform pcPov;

    [Header("Physics")]
    [ShowIf("DebugMode")]
    public bool HighSpeed;

    [HorizontalLine(color: EColor.Violet)]
    [Header("Other")]
    Transform bodyArt;

    bool Sprint;

    [ShowIf("DebugMode")] 
    public Vector3 IRLPosOffset;
    // Start is called before the first frame update
    void Start()
    {
        bodyArt = transform.GetChild(1);
        pcPov = transform.GetChild(2);

        VrCameraOffset = transform.GetChild(0).GetChild(0);
        VrCamera = VrCameraOffset.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Grounded && playerRigidbody.velocity.magnitude < new Vector3(S_Stats_MA.AerialMaxVelocity.x, 0, S_Stats_MA.AerialMaxVelocity.z).magnitude / 2)
        {
            HighSpeed = false;
        }

        if (S_Settings_TB.IsVRConnected)
        {
            transform.position -= IRLPosOffset;
            IRLPosOffset = Vector3.zero;
            IRLPosOffset += new Vector3(IRLPosition.x, 0, IRLPosition.z);
            transform.position += IRLPosOffset;
        }

    }
    private void FixedUpdate()
    {
        if (!HighSpeed)
        {
            if (Sprint)
            {
                playerRigidbody.velocity = Clamp(playerRigidbody.velocity, S_Stats_MA.MaxVelocity * 2);
            }
            else
            {
                playerRigidbody.velocity = Clamp(playerRigidbody.velocity, S_Stats_MA.MaxVelocity);
            }
        }
        else
        {
            playerRigidbody.velocity = Clamp(playerRigidbody.velocity, S_Stats_MA.AerialMaxVelocity);
        }

        Movement();
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

        playerRigidbody.velocity += move;
    }

    void Turn()
    {
        turnValue = playerInput.actions["Turn"].ReadValue<Vector2>();

        transform.eulerAngles += new Vector3(0, turnValue.x * S_Stats_MA.TurnSpeed * Time.fixedDeltaTime, 0);
    }
    #endregion

    //InputActions
    #region
    public void SprintHeld(InputAction.CallbackContext context)
    {

        Sprint = !Sprint;
    }
    #endregion

    //Gizmos
    #region

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
    #endregion
}
