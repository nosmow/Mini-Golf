using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InitializeLineRenderer();
    }

    void InitializeLineRenderer()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void UpdateLine(Vector3 startPosition, Vector3 direction)
    {
        Vector3 endPosition = CalculateLineEndPosition(startPosition, direction);

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    Vector3 CalculateLineEndPosition(Vector3 startPosition, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, direction.magnitude))
        {
            if (hit.collider.tag != "Flag")
            {
                return hit.point;
            }
        }
        return startPosition + direction;
    }

    public void ResetLine()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
