using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 startMousePos;
    private Vector3 endMousePos;
    private Rigidbody rb;
    public float forceMultiplier = 10f;
    public float maxForce = 100f;
    public float minVelocity = 0.05f;
    

    private LineGenerator lineGenerator;

    void Start()
    {
        lineGenerator = FindAnyObjectByType<LineGenerator>();

        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        BallMovement();
    }

    void BallMovement()
    {
        if (rb.velocity.magnitude <= minVelocity)
        {
            HandleMouseInput();
        }
        else
        {
            lineGenerator.ResetLine();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetStartMousePos();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLineRenderer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ApplyForce();
            lineGenerator.ResetLine();
        }
    }

    void SetStartMousePos()
    {
        startMousePos = GetMouseWorldPosition();
        startMousePos.y = transform.position.y;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void UpdateLineRenderer()
    {
        endMousePos = GetMouseWorldPosition();
        endMousePos.y = transform.position.y;

        Vector3 direction = startMousePos - endMousePos;
        lineGenerator.UpdateLine(transform.position, direction);
    }

    void ApplyForce()
    {
        Vector3 direction = startMousePos - endMousePos;
        float force = Mathf.Clamp(direction.magnitude * forceMultiplier, 0, maxForce);
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
}
