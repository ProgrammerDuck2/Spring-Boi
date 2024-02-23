using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BouncePad_OR : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private string animName;
    private Animator bounceAnim;

    private bool isPlayingAnimation;
    private Rigidbody rb;

    private void Start()
    {
        bounceAnim = GetComponent<Animator>();
    }

    // Make the player bounce off the pad
    public void BounceOff()
    {
        rb.AddForce(0,bounceForce,0);
        isPlayingAnimation = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        rb = other.gameObject.GetComponent<Rigidbody>();
        if (!isPlayingAnimation)
        {
            bounceAnim.Play(animName,0,0f);
        }
        isPlayingAnimation = true;
    }
}
