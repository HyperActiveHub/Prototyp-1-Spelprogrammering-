using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform followObj;
    public Vector2 offset = new Vector2(5f, 1.25f);

    [SerializeField]
    float maxSpeed = 10;
    [SerializeField]
    TurnScript turn;
    void Start()
    {
        turn.onTurn += onTurn;
    }

    void Update()
    {
        Vector3 targetPos = new Vector3(followObj.position.x + offset.x, followObj.position.y + offset.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * maxSpeed);

    }

    void onTurn(int side)
    {
        offset.x *= -1;
    }
}
