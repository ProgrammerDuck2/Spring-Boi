using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PickupReaction_TB : MonoBehaviour
{
    [HideInInspector] public Vector3 startRotation;
    private void Start()
    {
        startRotation = transform.eulerAngles;
    }
    public virtual void LetGo()
    {
        transform.eulerAngles = startRotation;
    }
    public virtual void PickedUp()
    {

    }
}
