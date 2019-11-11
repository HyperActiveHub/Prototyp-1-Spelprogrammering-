using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikeCollider : MonoBehaviour
{

    bool Collide = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Collide == true)
        {
            Coliding.instance.Collide = true;
        }

        if (Collide == false)
        {
            Coliding.instance.Collide = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        Collide = true;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //avrundar tiden
        Collide = false;
    }
}
