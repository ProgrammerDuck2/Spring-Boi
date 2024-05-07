using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_MovingPlatform_MA : MonoBehaviour
{
    S_Lever_TB lever;

    [SerializeField] float speed;
    
    [Range(0, 1)]
    [SerializeField] float value;

    Transform Location1;
    Transform Location2;

    [SerializeField] bool reverse;

    private void Start()
    {
        lever = FindFirstObjectByType<S_Lever_TB>();
        Location1 = transform.parent.GetChild(1);
        Location2 = transform.parent.GetChild(2);
    }

    private void Update()
    {
        if (lever == null) return;
        if (lever.active == false) return;

        transform.localPosition = Vector3.Lerp(Location1.localPosition, Location2.localPosition, value);

        value += reverse ? -Time.deltaTime * speed : Time.deltaTime * speed;
        if(value > 1) reverse = true;
        if(value < 0) reverse = false;
    }
}
