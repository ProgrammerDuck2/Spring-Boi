using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Rotate_TB : MonoBehaviour
{
    Vector3 speed = new Vector3(0, 100, 0);
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
