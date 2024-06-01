using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BouncePad_OR : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private string animName;
    [SerializeField] private EventReference bouncePad;
    private Animator bounceAnim;
    
    private bool isPlayingAnimation;
    private Rigidbody rb;
    [SerializeField] private ParticleSystem boing;

    private void Start()
    {
        bounceAnim = GetComponent<Animator>();
    }

    // Make the player bounce off the pad
    public void BounceOff()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(0,bounceForce,0);
        boing.Play();
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

        S_AudioManager_HA.instance.PlayOneShot(bouncePad, transform.position);
    }
}
