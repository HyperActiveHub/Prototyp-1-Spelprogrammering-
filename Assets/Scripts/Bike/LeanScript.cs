using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanScript : MonoBehaviour
{
    [SerializeField]
    float leanTimer = 0.75f;
    [SerializeField]
    float leanTorque = 10;

    Rigidbody2D rb;
    float timer;
    bool canLean;

    float lastPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = leanTimer;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (timer < leanTimer)
        {
            timer += Time.deltaTime;
        }
        else
        {
            canLean = true;
        }

        if(canLean && h != 0)
        {
            timer = 0;
            //lastPressed = h;
            ////for (int i = 0; i < forceFrames; i++)
            ////{
            ////    rb.AddTorque(-h * leanTorque, ForceMode2D.Force);
            ////}
            //currentTorque = Mathf.MoveTowards(currentTorque, leanTorque, Time.deltaTime * forceStep);
            //rb.AddTorque(currentTorque * -lastPressed);

            //if(currentTorque >= leanTorque)
            //{
            //    leaning = false;
            //    print("leaned");
            //    currentTorque = 0;
            //}

            rb.AddTorque(leanTorque * -h);
            canLean = false;
        }
    }
}