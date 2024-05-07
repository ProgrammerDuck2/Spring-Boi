using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MagnetRobotAnim_OR : MonoBehaviour
{
    [SerializeField] private GameObject upperBody, lowerBody, magnet;
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        var rotationPerSecond = rotationSpeed * Time.deltaTime;
        upperBody.transform.Rotate(0,rotationPerSecond/4,0);
        
        lowerBody.transform.Rotate(0,rotationPerSecond,0);
        
        magnet.transform.Rotate(rotationPerSecond/4,rotationPerSecond,rotationPerSecond/2);
    }
}
