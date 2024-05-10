using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class S_PcCamera_TB : MonoBehaviour
{
    GameObject POV;

    float xRotation;

    int lookUpAngle = -60;
    int lookDownAngle = 60;

    private void Start()
    {
        POV = transform.parent.gameObject;
    }
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * (S_Settings_TB.MouseSensitivity * Time.deltaTime) / Time.timeScale;
        float MouseY = Input.GetAxis("Mouse Y") * (S_Settings_TB.MouseSensitivity * Time.deltaTime) / Time.timeScale;

        if (!float.IsNaN(Vector3.up.y * MouseX))
        {
            POV.transform.Rotate(Vector3.up * MouseX);
        }
        if (!float.IsNaN(MouseY))
        {
            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, lookUpAngle, lookDownAngle);
            POV.transform.localRotation = Quaternion.Euler(xRotation, POV.transform.eulerAngles.y, 0);
        }
    }
}
