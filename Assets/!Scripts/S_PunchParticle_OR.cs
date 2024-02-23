using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class S_PunchParticle_OR : MonoBehaviour
{
    [SerializeField] private ParticleSystem impactParticle;
    [SerializeField] private ParticleSystem[] hitParticle;
    private void OnCollisionEnter(Collision other)
    {
        ParticlesOnImpact();
    }

    public void ParticlesOnImpact()
    {
        var random = Random.Range(0, hitParticle.Length);
        var particle = Instantiate(impactParticle, transform.position, Quaternion.identity);
        particle.Play();
        hitParticle[random].Play();
    }
}
