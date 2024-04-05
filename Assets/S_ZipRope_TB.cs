using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ZipRope_TB : MonoBehaviour
{
    [SerializeField] Transform zipStart;
    [SerializeField] Transform zipEnd;

    LineRenderer lineRenderer;

    private void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, zipStart.position);
        lineRenderer.SetPosition(1, zipEnd.position);
    }

    private void OnDrawGizmos()
    {
        lineRenderer.SetPosition(0, zipStart.position);
        lineRenderer.SetPosition(1, zipEnd.position);
    }
}
