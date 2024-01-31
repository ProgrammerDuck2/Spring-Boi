using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_MovingPlatform_MA : MonoBehaviour
{
    [SerializeField] List<GameObject> turningPoints = new List<GameObject>();

    private int speed = 10;
    private int nextPoint = 0; //next point in turningPoints list

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.LookAt(turningPoints[nextPoint].transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == turningPoints[nextPoint])
        {
            nextPoint += 1;

            if (nextPoint == turningPoints.Count)
            {
                nextPoint = 0;
            }
        }
    }
}
