using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_StretchArms_MA : MonoBehaviour
{
    [SerializeField] private int whichArm;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hand;
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
            transform.localScale = new Vector3(0, 0, 20f) + transform.localScale;
        }
        if (Input.GetMouseButtonUp(whichArm))
        {
            //Debug.Log("mouseup");
            transform.localScale = new Vector3(0, 0, -20f) + transform.localScale;
        }
        if (Physics.CheckSphere(transform.position, 1, mask))
        {
            hand.transform.localPosition = transform.position; 
            player.transform.position = new Vector3 (0, 0, newscale.z);
        }
    }
}
