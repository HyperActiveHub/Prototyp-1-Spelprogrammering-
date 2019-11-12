using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
//[RequireComponent(typeof(WheelJoint2D))]
public class SwingArmScript : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    float timeScale = 1;
    [SerializeField]
    Transform wheel = null;
    [SerializeField]
    Transform swingAnchor;

    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Time.timeScale = timeScale;

        lr.SetPosition(0, (Vector2)(swingAnchor.position));

       // Vector2 offset = joint.attachedRigidbody.transform.position;
        lr.SetPosition(1, (Vector2)wheel.position);
    }
}
