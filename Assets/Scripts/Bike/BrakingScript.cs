﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakingScript : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.S;
    HingeJoint2D hinge;
    SpringJoint2D spring;
    CircleCollider2D cirlceColl;
    Rigidbody2D rb;

    [SerializeField]
    float brakeSpeed = 0.2f;
    [SerializeField]
    float breakeForce = 10;
    [SerializeField]
    float offsetToGround = 0.6f;
    [SerializeField]
    float springFrequenzy = 10;

    WheelScript wheelScript;

    void Start()
    {
        if (inputKey == KeyCode.None)
        {
            Debug.LogError("No Key registering 'Brake' input.", this);
        }
        hinge = GetComponent<HingeJoint2D>();
        spring = GetComponent<SpringJoint2D>();
        CreateBrakeSpring();
        cirlceColl = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        wheelScript = GetComponent<WheelScript>();
    }

    void CreateBrakeSpring()
    {
        if(spring == null)
        {
            spring = gameObject.AddComponent<SpringJoint2D>();
        }

        spring.enableCollision = true;
        spring.autoConfigureDistance = false;
        spring.distance = 0;
        spring.dampingRatio = 1;
        spring.frequency = springFrequenzy;
        spring.breakForce = breakeForce;
        spring.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(inputKey))
        {
            if(spring == null)
            {
                CreateBrakeSpring();
            }

            Vector2 contact;
            if (HasContact(out contact) && spring.enabled == false)
            {
                if (wheelScript != null)
                {
                    wheelScript.isBraking = true;
                }

                spring.connectedAnchor = contact;
                contact = transform.InverseTransformPoint(contact);
                spring.anchor = new Vector2(contact.x, contact.y - 0.005f);
                spring.enabled = true;
            }
        }
        else if(Input.GetKeyUp(inputKey))
        {
            if(spring != null)
            spring.enabled = false;

            if (wheelScript != null)
            {
                wheelScript.isBraking = false;
            }
        }
    }

    bool HasContact(out Vector2 contactPoint)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();

        if(cirlceColl.GetContacts(contacts) != 0)
        {
            contactPoint = contacts[0].point;
            return true;
        }

        contactPoint = Vector2.zero;
        return false;
    }
}