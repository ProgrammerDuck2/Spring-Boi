using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class S_Movement_TB : MonoBehaviour
{
    [SerializeField] InputActionProperty leftJoystick;
    [SerializeField] InputActionProperty rightButtonA;
    Vector2 joystickValue;
    [Range(1, 10)]
    [SerializeField] float Speed;

    [SerializeField] Transform povCamera;
    [SerializeField] Transform rotHelp;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        joystickValue = leftJoystick.action.ReadValue<Vector2>();

        rotHelp.eulerAngles = new Vector3(0, povCamera.eulerAngles.y, 0);

        Vector3 move = rotHelp.transform.TransformDirection(Vector3.Normalize(new Vector3(joystickValue.x, 0, joystickValue.y)) * Time.deltaTime * Speed);
        cc.Move(move);

        Debug.Log(rightButtonA.action.ReadValue<bool>());
    }
}
