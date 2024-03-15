using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerPhysics_TB : S_Player_TB
{
    [ShowIf("DebugMode")]
    [Range(0, 100)]
    public float gravityStrength = 15;

    void FixedUpdate()
    {
        playerRigidbody.AddForce(Vector3.down * gravityStrength, ForceMode.Acceleration);
    }

    public bool CheckGround()
    {
        return Physics.CheckCapsule(groundCheckBottomPos(), groundCheckTopPos(), .9f, groundLayer);

        //if (Grounded != Physics.CheckCapsule(groundCheckBottomPos(), groundCheckTopPos(), groundCheckRadius, groundLayer))
        //{
        //    Collider[] ground = Physics.OverlapCapsule(groundCheckBottomPos(), groundCheckTopPos(), groundCheckRadius, groundLayer);

        //    Grounded = !Grounded;

        //    if (ground.Length >= 1)
        //    {
        //        //transform.parent =  ground[0].transform;
        //        print("landed on stick ground, Object is: " + ground[0].gameObject);
        //    }
        //    else
        //    {
        //        //transform.parent = null;
        //    }
        //}
    }

    public Vector3 groundCheckTopPos()
    {
        return transform.position - transform.up * 0.6f;
    }
    public Vector3 groundCheckBottomPos()
    {
        return transform.position + transform.up * 0.5f;
    }
}
