using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampMovement : MonoBehaviour
{

    Transform idealPos;
    Vector3 vel;
    public float smoothTime;
    Rigidbody2D rb;
    void Start()
    {
        idealPos = new GameObject("Ideal " + name + " position").transform;
        idealPos.position = transform.position;
        idealPos.SetParent(transform.parent);

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, idealPos.position, smoothTime); //Vector3.SmoothDamp(transform.position, idealPos.position, ref vel, smoothTime);
    }
}
