using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public int buttonID;
    ButtonScript button;

    [SerializeField]
    List<Vector2> movePoints;

    //movespeed
    //more toggles

    void Start()
    {
        //Use GameManager to list all buttons and link current platform with that button
        button.onActivate += OnActivated;
    }

    void Update()
    {
        
    }

    void OnActivated(bool toggle)
    {

    }

}