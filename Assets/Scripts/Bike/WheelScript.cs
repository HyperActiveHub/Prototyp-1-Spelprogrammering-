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

    Rigidbody2D frameRb;
    HingeJoint2D joint;
    JointMotor2D motor;
    float throttleTimer = 0;

    float torque;
    public bool isBraking;

    private void Awake()
    {
        transform.parent.GetComponentInChildren<TurnScript>().onTurn += OnTurn; //Observer - Design pattern
    }

    void Start()
    {
        if (GetComponent<HingeJoint2D>() != null)
        {
            joint = GetComponent<HingeJoint2D>();
        }
    }

    void OnTurn(int toSide)
    {
        if (toSide < 0 && acceleration < 0)
        {
            acceleration *= -1;
        }
        else if (toSide > 0 && acceleration > 0)
        {
            acceleration *= -1;
        }
    }

    void Update()
    {
        float input = 0;
        if (!isBraking)
        input = Input.GetAxis("Horizontal");

        float currentThrottle;

        if (input > 0)
        {
            if (throttleTimer < timeToMaxThrottle)
            {
                throttleTimer += Time.deltaTime;
            }
            else
                throttleTimer = timeToMaxThrottle;

            currentThrottle = throttleCurve.Evaluate(throttleTimer);
            torque = Mathf.Clamp01(input) * currentThrottle * acceleration;

            if (torque > maxTorque)
            {
                torque = maxTorque;
            }
            motor.maxMotorTorque = maxTorque;
            motor.motorSpeed = -torque;
            joint.motor = motor;
        }
        else if (input == 0 || isBraking)
        {
            joint.useMotor = false;
            throttleTimer = 0;
        }
    }

    private void OnDisable()
    {
        transform.parent.GetComponentInChildren<TurnScript>().onTurn -= OnTurn;
    }
}