using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelJoint2D))]
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

    Rigidbody2D frameRb;
    WheelJoint2D joint;
    JointMotor2D motor;
    CircleCollider2D cirlceColl;
    float throttleTimer = 0;

    void Start()
    {
        //frameRb = LeanScript.instance.GetComponent<Rigidbody2D>();
        cirlceColl = GetComponent<CircleCollider2D>();
        joint = GetComponent<WheelJoint2D>();

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

        if(currentThrottle == 1)
        {
            print("max throttle");
        }

        float speed = input * currentThrottle * -acceleration;

        motor.maxMotorTorque = maxTorque;
        motor.motorSpeed = speed;
        joint.motor = motor;
        
    }
}