using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Rotate_z_OR : MonoBehaviour
{
    Vector3 speed = new Vector3(0, 0,100);
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }

}
