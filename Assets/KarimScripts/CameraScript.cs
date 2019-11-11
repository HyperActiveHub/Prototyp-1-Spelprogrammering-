using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform followObj;
    public float offset = 5f;

    void Start()
    {
        followObj = GameObject.Find("Bike_v2").transform;
    }

    void Update()
    {
        Vector3 targetPos = new Vector3(followObj.position.x + offset, transform.position.y, transform.position.z);

        transform.position = targetPos;//Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * maxSpeed);

    }
}
