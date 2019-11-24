using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnScript : MonoBehaviour
{
    public float turnSpeed = 1.5f;
    [SerializeField]
    GameObject drive;
    [SerializeField]
    GameObject front;
    [SerializeField]
    Rigidbody2D forks;
    [SerializeField]
    Rigidbody2D swingarm;
    [SerializeField]
    Transform bikeSprite;

    Rigidbody2D driveRB;
    Rigidbody2D frontRB;

    [HideInInspector]
    public bool isTurning;
    Vector3 target;

    public UnityAction<int> onTurn = delegate { };
    
    void Start()
    {
        onTurn((int)Mathf.Sign(transform.localScale.x));

        driveRB = drive.GetComponent<Rigidbody2D>();
        frontRB = front.GetComponent<Rigidbody2D>();
        target = bikeSprite.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bikeSprite.localScale = target;                                             //In case of turning in the middle of another turn, snap to previous target and turn normally from there.
            bikeSprite.localScale = new Vector3(-1, bikeSprite.localScale.y);
            target = new Vector3(1, target.y); 
            isTurning = true;

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);    //Physical bike is instantly turned around.

            var temp = drive.transform.position;                        //Switch place of front and rear wheel.
            drive.transform.position = front.transform.position;
            front.transform.position = temp;

            var temp2 = driveRB.velocity;                               //Switch velocities
            driveRB.velocity = frontRB.velocity;
            frontRB.velocity = temp2;

            onTurn((int)Mathf.Sign(transform.localScale.x));
        }

        if (isTurning)
        {
            bikeSprite.localScale = Vector3.MoveTowards(bikeSprite.localScale, target, turnSpeed * Time.deltaTime);

            if (bikeSprite.localScale.x == target.x)
            {
                isTurning = false;
            }
        }
    }
}