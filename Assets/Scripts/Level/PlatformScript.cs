using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField]
    List<ButtonScript> buttons = new List<ButtonScript>();

    [SerializeField]
    List<Vector2> movePoints;
    [SerializeField]
    float moveSpeed = 1;
    [SerializeField]
    bool isBezier;
    [SerializeField]
    bool isLooping;
    [SerializeField]
    bool isMoving;

    List<Vector2> movePositions = new List<Vector2>();
    float minDist = 0.5f;
    int current = 0;
    float timer = 0;

    void Start()
    {
        //Use GameManager to list all buttons and link current platform with that button?
        foreach (ButtonScript button in buttons)
        {
            button.onPressed += OnActivated;
        }

        foreach (Vector2 point in movePoints)
        {
            movePositions.Add(transform.parent.TransformPoint(point));
        }

        if (isBezier)
        {
            if (movePoints.Count != 3)
            {
                Debug.LogError("Quadratic Bezier curve needs 3 positions.", this);
            }
        }
    }


    Vector2 CalcQuadBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        //p1 + (1-t)^2 (p0-p1) + t^2 (p2-p1), 0 <= t <= 1
        return (p1 + Mathf.Pow(1 - t, 2) * (p0 - p1) + Mathf.Pow(t, 2) * (p2 - p1));
    }

    
    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;

            if (isBezier)
            {
                float t = Mathf.Clamp01(timer * moveSpeed);
                Vector2 curvePos = CalcQuadBezierPoint(t, movePositions[0], movePositions[1], movePositions[2]);
                transform.position = curvePos;
                if (t == 1)
                {
                    if (!isLooping)
                    {
                        isMoving = false;
                        timer = 0;
                        movePositions.Reverse();
                    }
                    else
                    {
                        timer = 0;
                        movePositions.Reverse();
                    }
                }
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, movePositions[current], timer * moveSpeed * 0.2f);

                float dist = Vector2.Distance(transform.position, movePositions[current]);

                if (dist < minDist)
                {
                    if (current == movePositions.Count - 1)
                    {
                        if (isLooping)
                        {
                            movePositions.Reverse();
                            current = 0;
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                    else
                    {
                        current++;
                    }
                }
            }
        }
    }

    void OnActivated(Object o)
    {
        isMoving = true;
        movePositions.Reverse();
        current = 0;

        Debug.Log("Platform activated/moved by " + o, this);
    }

}