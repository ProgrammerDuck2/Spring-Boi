using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_HandPostion_TB : S_Hand_TB
{
    [Header("Tracking")]
    [SerializeField] Vector3 handOffset;

    [HideInInspector] public Vector3 controllerPosition;
    [HideInInspector] public Quaternion controllerRotation;
    void Update()
    {
        if (!handInput.useReference)
        {
            transform.localPosition = playerInput.actions["Position"].ReadValue<Vector3>() + handOffset - playerMovement.IRLPosOffset;
            transform.localRotation = playerInput.actions["Rotation"].ReadValue<Quaternion>();

            if (playerInput.actions["Position"].ReadValue<Vector3>() == Vector3.zero)
            {
                Debug.LogWarning("Controller position not found");
                HandArt.transform.GetChild(0).gameObject.SetActive(false);

            }
            else
            {
                HandArt.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            transform.localPosition = handInput.pos.action.ReadValue<Vector3>() + handOffset - playerMovement.IRLPosOffset;
            transform.localRotation = handInput.rot.action.ReadValue<Quaternion>();

            if (handInput.pos.action.ReadValue<Vector3>() == Vector3.zero)
            {
                Debug.LogWarning("Controller position not found");
                HandArt.transform.GetChild(0).gameObject.SetActive(false);

            }
            else
            {
                HandArt.transform.GetChild(0).gameObject.SetActive(true);
            }
        }


        controllerPosition = transform.localPosition;
        controllerRotation = transform.localRotation;
    }
}
