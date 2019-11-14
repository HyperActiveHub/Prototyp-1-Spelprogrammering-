using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    [SerializeField]
    float timeToMaxThrottle = 1.0f;
    [SerializeField]
    AnimationCurve throttleCurve;
    [SerializeField]
    float maxTorque = 300;
    [SerializeField]
    float acceleration = 100;
    [SerializeField]
    float engineBreakMult = 0.4f;
    [SerializeField]
    float downForce = 10.0f;

    [SerializeField]
    float rotationDamp = 0.5f;

    [SerializeField]
    Vector2 pivotForceOffset;

    Rigidbody2D rb;
    Rigidbody2D frameRb;
    HingeJoint2D joint;
    JointMotor2D motor;
    CircleCollider2D cirlceColl;
    float throttleTimer = 0;

    float torque;
    void Start()
    {
        cirlceColl = GetComponent<CircleCollider2D>();
        joint = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        //frameRb = GetComponentsInParent<Rigidbody2D>()[1];
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        //movetowards?
        if (input != 0)
        {
            throttleTimer += Time.deltaTime;
        }
        else if (throttleTimer > 0)
            throttleTimer = 0;
        float currentThrottle = throttleCurve.Evaluate(throttleTimer);
        torque = input * currentThrottle * -acceleration;
        if(torque > maxTorque)
        {
            torque = maxTorque;
        }
        motor.maxMotorTorque = maxTorque;
        motor.motorSpeed = -torque;
        joint.motor = motor;

        print("Torque: " + torque);

        //testing purposes
        if(Input.GetKeyDown(KeyCode.Space))
        {
            frameRb.AddForce(Vector2.down * downForce * frameRb.mass);
        }
    }

    //private void FixedUpdate()
    //{
    //    //Use this ray to get the normal of the ground
    //    RaycastHit2D[] groundHit = Physics2D.RaycastAll(transform.position, Vector2.down, cirlceColl.bounds.extents.y + 0.5f);   //Additional offset for inclines
    //    Debug.DrawRay(transform.position, Vector2.down, Color.red);

    //    float distToNearest = 10;
    //    Vector2 groundNormal = Vector2.zero;

    //    foreach (RaycastHit2D h in groundHit)
    //    {
    //        if (h.rigidbody == null)
    //        {
    //            if (h.distance < distToNearest)
    //            {
    //                distToNearest = h.distance;
    //                groundNormal = -h.normal;
    //            }
    //        }
    //    }


    //    Vector2 forcePos = (Vector2)frameRb.transform.position + pivotForceOffset;

    //    float dot = Vector2.Dot(Vector2.right, rb.velocity.normalized);

    //    //torque or physical rotation of the wheel fucks up suspension.. maybe only rotate sprite according to some relative speed?
    //    if (torque != 0)
    //        rb.AddTorque(torque);
    //    else if (dot > 0)    //if moving right
    //    {
    //        rb.AddTorque(acceleration * engineBreakMult);
    //    }
    //    else
    //    {
    //        rb.AddTorque(-acceleration * engineBreakMult);
    //    }


    //    //frameRb.AddForceAtPosition((-groundNormal * -torque * rotationDamp) * Vector2.Dot(rb.position.normalized, forcePos.normalized), forcePos, ForceMode2D.Force);

    //    //Debug.DrawRay(forcePos, (-groundNormal * torque * rotationDamp) * Vector2.Dot(rb.position.normalized, forcePos.normalized), Color.blue);
    //}
}