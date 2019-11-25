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
    //movespeed
    //more toggles
    bool toggleMove;
    bool isMoving;
    void Start()
    {
        //Use GameManager to list all buttons and link current platform with that button?
        foreach (ButtonScript button in buttons)
        {
            button.onPressed += OnActivated;
        }
    }

    void Update()
    {
        if(isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoints[0], moveSpeed * Time.deltaTime);
            if((Vector2)transform.position == movePoints[0])
            {
                isMoving = false;
            }
        }
    }

    void OnActivated(bool toggle, Object o)
    {
        toggleMove = toggle;
        isMoving = true;
        if (toggleMove == false)    //move backwards..
        {
            for(int i = 0; i < movePoints.Count; i++)       
            {
                movePoints[i] *= -1;    //temp
            }
        }
        Debug.Log("Platform activated/moved by " + o, this);
    }

}