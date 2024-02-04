using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(S_Grab_TB))]
public class S_LaunchArms_TB : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] int speed;
    int tempSpeed;
    
    [SerializeField] InputActionProperty inputPosition;
    [SerializeField] InputActionProperty inputRotation;
    Vector3 controllerPosition;
    Quaternion controllerRotation;

    [SerializeField] InputActionProperty buttonToLaunch;

    S_Grab_TB grab;

    [Required]
    [SerializeField] GameObject handToLaunch;
    GameObject currentHandMissile;

    [Required]
    [SerializeField] GameObject Player;
    GameObject handArt;

    bool pullingHand;

    // Start is called before the first frame update
    void Start()
    {
        buttonToLaunch.action.started += LaunchArm;
        buttonToLaunch.action.canceled += PullArm;
        handArt = transform.GetChild(0).gameObject;

        tempSpeed = speed;

        grab = GetComponent<S_Grab_TB>();
    }

    // Update is called once per frame
    void Update()
    {
        controllerPosition = inputPosition.action.ReadValue<Vector3>();
        controllerRotation = inputRotation.action.ReadValue<Quaternion>();

        if (currentHandMissile != null)
        {
            Vector3 handVelocity = pullingHand ? currentHandMissile.transform.forward * tempSpeed : currentHandMissile.transform.forward * tempSpeed;

            if(pullingHand)
            {
                currentHandMissile.transform.LookAt(transform);

                if(Vector3.Distance(transform.position, currentHandMissile.transform.position) <= .5f)
                {
                    Destroy(currentHandMissile);
                    pullingHand = false;
                    handArt.SetActive(true);
                }
            } else
            {
                if(Physics.CheckSphere(currentHandMissile.transform.position, .5f, grab.grabable))
                {
                    tempSpeed = 0;
                }

                if(Vector3.Distance(transform.position, currentHandMissile.transform.position) >= 10f)
                {

                }
            }
            
            currentHandMissile.transform.position += handVelocity * Time.deltaTime;
        }
    }

    public void LaunchArm(InputAction.CallbackContext context)
    {
        if(currentHandMissile == null)
        {
            pullingHand = false;
            currentHandMissile = Instantiate(handToLaunch, controllerPosition + new Vector3(0, 0.07f, 0), controllerRotation);

            handArt.SetActive(false);

        }
    }

    public void PullArm(InputAction.CallbackContext context)
    {
        pullingHand = true;
    }
}
