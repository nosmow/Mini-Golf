using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private Vector3 starMousePos;
    private Vector3 endMousePos;
    private Rigidbody rb;
    public float forceMultiplier = 10f;
    public float maxForce = 100f;
    public float minVelocity = 0.05f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.angularDrag = 1f;
        //rb.drag = 1f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    void Update()
    {
        BallMovement();
    }

    void BallMovement()
    {
        if (rb.velocity.magnitude <= minVelocity)
        {
            if (Input.GetMouseButtonDown(0))
            {
                starMousePos = Input.mousePosition;
                starMousePos.z = Camera.main.transform.position.y;
                starMousePos = Camera.main.ScreenToWorldPoint(starMousePos);
                starMousePos.y = transform.position.y;
            }

            Vector3 direction = starMousePos - endMousePos;
            float force = Mathf.Clamp(direction.magnitude * forceMultiplier, 0, maxForce);

            if (Input.GetMouseButton(0))
            {
                endMousePos = Input.mousePosition;
                endMousePos.z = Camera.main.transform.position.y;
                endMousePos = Camera.main.ScreenToWorldPoint(endMousePos);
                endMousePos.y = transform.position.y;


                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + direction);
            }

            if (Input.GetMouseButtonUp(0))
            {
                rb.AddForce(direction.normalized * force, ForceMode.Impulse);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
