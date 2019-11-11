using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanScript : MonoBehaviour
{
    public static LeanScript instance;
    public float wheelieRange = 60f;
    public float leanAngle = 20f;
    public float leanOffset = 40f;
    public float targetAngle = 300f;
    public float smoothing = 10f;
    public float maxRotationSpeed = 30f;
    public float offBalanceDrag = 100;
    Rigidbody2D rb;
    float currentVel;
    float drag;
    bool died;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        drag = rb.angularDrag;
    }

    void Update()
    {
        float h = Input.GetAxis("Vertical");

        //transform.eulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle + (-h * (maxLeanAngle - leanOffset)), ref currentVel, smoothing, maxRotationSpeed));
        float maxLean = targetAngle + wheelieRange * 0.5f;
        float minLean = targetAngle - wheelieRange * 0.5f;

        print("max: " + maxLean);
        print("min: " + minLean);
        if (transform.eulerAngles.z < maxLean && transform.eulerAngles.z > minLean)
        {
            rb.angularDrag = drag;
            transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle + (-h * Mathf.Abs(leanAngle - leanOffset)), ref currentVel, smoothing, maxRotationSpeed * (1 + Mathf.Abs(h))), Vector3.forward);
        }
        else
            rb.angularDrag = offBalanceDrag;

    }
}
