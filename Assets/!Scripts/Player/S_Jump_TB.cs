using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Jump_TB : S_Player_TB
{
    [ShowIf("DebugMode")]
    [SerializeField] Mesh capsule;

    bool canJump;
    float timer;

    float jumpCD;

    private void Update()
    {
        if(jumpCD > 0)
        {
            jumpCD -= Time.deltaTime;
        }


        if (Grounded)
            canJump = true;
        else if(canJump)
        {
            timer += Time.deltaTime;

            if(timer > .3f)
            {
                canJump = false;
                timer = 0f;
            }
        }
    }
    public void JumpPressed(InputAction.CallbackContext context)
    {
        Jump();
    }
    public void Jump()
    {
        if (canJump && jumpCD <= 0)
        {
            jumpCD = .3f;
            canJump = false;
            playerRigidbody.velocity = new Vector3 (playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);
            playerRigidbody.velocity += !crouch.isCrouching ? Vector3.up * S_Stats_MA.JumpPower : Vector3.up * S_Stats_MA.JumpPower / 2;
        }
    }
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = new Color(0, 1, 0, .5f);
            Gizmos.DrawMesh(capsule, physics.groundCheckTopPos() + physics.groundCheckBottomPos() - transform.up, transform.rotation, groundCheckSize());

        }
    }
    Vector3 groundCheckSize()
    {
        return new Vector3(transform.localScale.x * .1f, transform.localScale.y, transform.localScale.z * .1f);
    }
}
