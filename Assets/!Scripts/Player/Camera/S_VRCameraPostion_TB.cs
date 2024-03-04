using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_VRCameraPostion_TB : MonoBehaviour
{
    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = transform.parent.parent.parent.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.up * playerInput.actions["IRLPosition"].ReadValue<Vector3>().y;
    }
}
