using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(S_Hand_TB))]
public class S_StretchArms_MA : MonoBehaviour
{
    [SerializeField] private int whichArm;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private float distance = 10;
    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Ground", "StickGround");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newscale = transform.localScale;

        if (Input.GetMouseButtonDown(whichArm))
        {
            //Debug.Log("mousedown");
            transform.localScale = new Vector3(0, 0, 10f) + transform.localScale;

            if (whichArm == 0)
            {
                leftHand.transform.position += transform.forward;
            }
            else if (whichArm == 1)
            {
                rightHand.transform.position += transform.forward;
            }
        }

        if (Input.GetMouseButtonUp(whichArm))
        {
            //Debug.Log("mouseup");
            transform.localScale = new Vector3(0, 0, -10f) + transform.localScale;
            player.transform.position += player.transform.forward * 100;
            if (Physics.CheckSphere(transform.position, 1, mask))
            {
                Debug.Log("fghjkp");
                player.transform.position += player.transform.forward * distance;
            }
            if (whichArm == 0)
            {
                leftHand.transform.position -= transform.forward;
            }
            if (whichArm == 1)
            {
                rightHand.transform.position -= transform.forward;
            }
        }

        //Debug.Log(Physics.CheckSphere(transform.position, 1, mask));

        //if (Physics.CheckSphere(transform.position, 1, mask))
        //{
        //    hand.transform.localPosition = transform.position; 
        //    player.transform.position = new Vector3 (0, 0, newscale.z);
        //}
    }
    //change pivot of 
}
