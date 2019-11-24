using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
//[RequireComponent(typeof(WheelJoint2D))]
public class SwingArmScript : MonoBehaviour
{
    [SerializeField]
    Transform wheel = null;
    [SerializeField]
    Transform swingAnchor;

    TurnScript turnScript;
    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        turnScript = transform.parent.GetComponentInParent<TurnScript>();
    }

    void Update()
    {
        if (!turnScript.isTurning)
        {
            lr.useWorldSpace = true;
            lr.SetPosition(0, (Vector2)(swingAnchor.position));
            lr.SetPosition(1, (Vector2)wheel.position);
        }
        else
        {
            //Turn off lr until turn is complete, and meanwhile switch bike sprite to a sprite also containing swingarm + forks? this makes them turn with the bike sprite.

            float factor = (transform.parent.localScale.x + 1) * 0.5f;
            lr.SetPosition(0, Vector3.Lerp(lr.GetPosition(0), swingAnchor.position, factor));
            lr.SetPosition(1, Vector3.Lerp(lr.GetPosition(1), wheel.position, factor));
        }
    }
}
