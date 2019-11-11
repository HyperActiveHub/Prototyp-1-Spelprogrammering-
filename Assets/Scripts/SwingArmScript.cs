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
    Vector2 swingAnchor = new Vector2();

    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Time.timeScale = timeScale;

        lr.SetPosition(0, swingAnchor);

        Vector2 offset = wheel.InverseTransformPoint(wheel.position);
        lr.SetPosition(1, offset);
    }
}
