using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelJoint2D))]
public class WheelScript : MonoBehaviour
{
    public float maxTorque = 300, acceleration = 100;
    public float jumpForce = 50;
    [Range(0, 1)]
    public float forwardJumpKoefficent = 0.3f;
    [Range(0.001f, 0.5f)]
    public float distanceToGround = 0.01f;
    public float jumpCooldown = 0.5f;

    Rigidbody2D frameRb;
    WheelJoint2D joint;
    JointMotor2D motor;
    CircleCollider2D cirlceColl;
    Vector2 groundNormal;
    bool isOnCooldown;
    float jumpTimer;

    void Start()
    {
        frameRb = LeanScript.instance.GetComponent<Rigidbody2D>();
        cirlceColl = GetComponent<CircleCollider2D>();
        joint = GetComponent<WheelJoint2D>();

    }

    void Update()
    {
        float inputSpeed = Input.GetAxis("Horizontal");
        motor.maxMotorTorque = maxTorque;
        motor.motorSpeed = inputSpeed * acceleration;
        joint.motor = motor;

        if (isOnCooldown)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpCooldown)
            {
                isOnCooldown = false;
                jumpTimer = 0;
            }
        }

        bool grounded = IsGrounded();
        bool canJump = grounded && !isOnCooldown;

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        isOnCooldown = true;
        frameRb.velocity = new Vector2(frameRb.velocity.x, 0);
        frameRb.AddForce(new Vector2(forwardJumpKoefficent * jumpForce, jumpForce), ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        Vector2 orgin = new Vector2(transform.position.x, transform.position.y);

        //Use this ray to get the normal of the ground
        RaycastHit2D[] groundHit = Physics2D.RaycastAll(orgin, Vector2.down, cirlceColl.bounds.extents.y + distanceToGround + 0.5f);   //Additional offset for inclines
        Debug.DrawRay(orgin, Vector2.down, Color.red);

        float distToNearest = 10;

        foreach (RaycastHit2D h in groundHit)
        {
            if(h.transform.name != "Wheel" && h.transform.name != "Bike_v2")
            {
                if(h.distance < distToNearest)
                {
                    distToNearest = h.distance;
                    groundNormal = -h.normal;
                }
            }
        }

        //Use this ray to check if character is grounded, using the normal vector of the ground to get correct values on inclines aswell.
        RaycastHit2D[] groundNormalHit = Physics2D.RaycastAll(orgin, groundNormal, cirlceColl.bounds.extents.y + distanceToGround);
        Debug.DrawRay(orgin, groundNormal, Color.blue);

        foreach (RaycastHit2D h in groundNormalHit)
        {
            if (h.transform.name != "Wheel" && h.transform.name != "Bike_v2")
            {
                return true;
            }
        }
        return false;
    }
}
