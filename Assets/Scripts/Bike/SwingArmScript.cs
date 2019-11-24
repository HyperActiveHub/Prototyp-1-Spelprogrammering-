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
    [SerializeField]
    Material rightMat;
    [SerializeField]
    Material leftMat;

    Material currentMat;
    float normalDistance;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        turnScript = transform.parent.GetComponentInParent<TurnScript>();
        turnScript.onTurn += OnTurn;
        normalDistance = GetDist(lr.GetPosition(0).x, swingAnchor.position.x);
    }

    void OnTurn(int side)
    {
        if (side < 0)
        {
            currentMat = leftMat;
        }
        else if (side > 0)
        {
            currentMat = rightMat;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            lr.alignment = LineAlignment.View;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            lr.alignment = LineAlignment.TransformZ;
        }

        if (!turnScript.isTurning)
        {
            lr.useWorldSpace = true;
            lr.SetPosition(1, new Vector3(swingAnchor.position.x, swingAnchor.position.y, 1));
            lr.SetPosition(0, new Vector3(wheel.position.x, wheel.position.y, 1));
        }
        else
        {
            //Turn off lr until turn is complete, and meanwhile switch bike sprite to a sprite also containing swingarm + forks? this makes them turn with the bike sprite.
            float factor = (transform.parent.localScale.x + 1) * 0.5f;
            Vector2 lerpedAnchor = Vector2.Lerp(lr.GetPosition(1), swingAnchor.position, factor);
            Vector3 newAnchor = new Vector3(lerpedAnchor.x, lerpedAnchor.y, 1);

            Vector2 lerpedWheel = Vector2.Lerp(lr.GetPosition(0), wheel.position, factor);
            Vector3 newWheel = new Vector3(lerpedWheel.x, lerpedWheel.y, 1);

            lr.SetPosition(1, newAnchor);
            lr.SetPosition(0, newWheel);

            if (currentMat != lr.material)
            {
                if (GetDist(wheel.position.x, lr.GetPosition(0).x) <= normalDistance * 0.5f)
                {
                    lr.material = currentMat;
                }
            }

        }
    }

    float GetDist(float a, float b)
    {
        float x1 = a;
        float x2 = b;

        if (x2 < x1)
        {
            float temp = x2;
            x2 = x1;
            x1 = temp;
        }
        return x2 - x1;
    }
}