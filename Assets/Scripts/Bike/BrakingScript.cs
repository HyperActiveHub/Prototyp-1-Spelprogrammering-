using System.Collections;
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

    bool IsGrounded(out Vector2 groundNormal)
    {
        Vector2 origin = transform.position;
        groundNormal = Vector2.zero;

        Debug.DrawRay(origin, Vector2.down, Color.red);
        //Use this ray to get the normal of the ground
        RaycastHit2D[] groundHit = Physics2D.RaycastAll(origin, Vector2.down, cirlceColl.bounds.extents.y + offsetToGround);
        float distToNearest = 10;

        foreach (RaycastHit2D h in groundHit)
        {
            if (h.transform.GetComponent<BrakingScript>() == null)
            {
                if (h.distance < distToNearest)
                {
                    distToNearest = h.distance;
                    groundNormal = -h.normal;
                }
            }
        }

        //Use this ray to check if character is grounded, using the normal vector of the ground to get correct values on inclines aswell.
        RaycastHit2D[] groundNormalHit = Physics2D.RaycastAll(origin, groundNormal, cirlceColl.bounds.extents.y + offsetToGround);
        Debug.DrawRay(origin, groundNormal, Color.blue);

        foreach (RaycastHit2D h in groundNormalHit)
        {
            if (h.transform.GetComponent<BrakingScript>() == null)
            {
                return true;
            }
        }
        return false;
    }
}
