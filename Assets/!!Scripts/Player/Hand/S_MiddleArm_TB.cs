using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class S_MiddleArm_TB : MonoBehaviour
{
    LineRenderer lineRenderer;
    Transform player;
    [SerializeField]Transform handLocation;

    [SerializeField]bool isRight;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = Camera.main.transform;
    }

    private void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = Camera.main.transform;

        setLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        setLineRenderer();
    }

    void setLineRenderer()
    {
        if (isRight)
        {
            lineRenderer.SetPosition(0, player.position + player.transform.right / 4 + -player.transform.up / 2);
        }
        else
        {
            lineRenderer.SetPosition(0, player.position + -player.transform.right / 4 + -player.transform.up / 2);
        }
        lineRenderer.SetPosition(1, handLocation.position);
    }
}
