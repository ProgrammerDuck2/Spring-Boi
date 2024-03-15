using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Jump_TB : S_Player_TB
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpPressed(InputAction.CallbackContext context)
    {
        Jump();
    }
    public void Jump()
    {
        if (Grounded)
        {
            print("Jump");
            playerRigidbody.velocity += Vector3.up * S_Stats_MA.JumpPower;
        }
    }
    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            Gizmos.color = new Color(0, 1, 0, .5f);
            Gizmos.DrawMesh(GetComponentInChildren<MeshFilter>().sharedMesh, physics.groundCheckTopPos() + physics.groundCheckBottomPos() - transform.up, transform.rotation, groundCheckSize());
        }
    }
    Vector3 groundCheckSize()
    {
        return new Vector3(transform.localScale.x * .9f, transform.localScale.y, transform.localScale.z * .9f);
    }
}
