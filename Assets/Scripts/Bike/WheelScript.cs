using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This curve (0 <= x <= 1) determines how much torque will increase as the speed of the wheel (deg/s) approaches Max Motor Speed.")]
    AnimationCurve torqueCurve;
    [SerializeField]
    [Tooltip("The maximum amount of torque added to accelerate the wheel towards the target rotational speed (degrees/second). " +
        "\nThis value sets the upper limit of the torque. The current torque will move towards this value the faster the wheel is spinning.")]
    float maxTorque = 400;
    [SerializeField]
    float minTorque = 100;
    [SerializeField]
    [Tooltip("The maximum rotational speed of the wheel that the motor will try to reach, measured in degrees/second.")]
    float maxMotorSpeed = 1500f;


    Rigidbody2D frameRb;
    HingeJoint2D joint;
    JointMotor2D motor;
    CircleCollider2D circleColl;

    float currentTorque;
    float deltaTorque;
    //float currentSpeed = 0;     //deg/s
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
        circleColl = GetComponent<CircleCollider2D>();
    }

    void OnTurn(int toSide)
    {
        if (toSide < 0 && maxMotorSpeed < 0)
        {
            maxMotorSpeed *= -1;
        }
        else if (toSide > 0 && maxMotorSpeed > 0)
        {
            maxMotorSpeed *= -1;
        }
    }

    void Update()
    {
        deltaTorque = maxTorque - minTorque;    //temp in update for ease of testing.

        float input = 0;
        if (!isBraking)
        input = Input.GetAxis("Vertical");

        if (input > 0)
        {
            float speedMult = Mathf.Abs(joint.jointSpeed) / Mathf.Abs(maxMotorSpeed);
            float t = speedMult;      //movetowards; t = speedMult?

            //input adds torque -> input increases t. Linearly with jointSpeed.
            currentTorque = minTorque + (torqueCurve.Evaluate(t) * deltaTorque);

            //newSpeed = Mathf.Clamp01(input) * currentSpeed;       //Change torque instead of speed with input
            
            if(Mathf.Abs(joint.jointSpeed) > Mathf.Abs(maxMotorSpeed))     
            {
                motor.motorSpeed = joint.jointSpeed;
                currentTorque = 0;
            }
            else
                motor.motorSpeed = -maxMotorSpeed;

            motor.maxMotorTorque = currentTorque;
            joint.motor = motor;

            print("Torque: " + currentTorque);
            print("WheelSpeed: " + joint.jointSpeed);
        }
        else if (input == 0 || isBraking)
        {
            joint.useMotor = false;
        }

        //print("Wheel speed (deg./s): " + joint.jointSpeed);
        
    }

    private void OnDisable()
    {
        transform.parent.GetComponentInChildren<TurnScript>().onTurn -= OnTurn;
    }
}