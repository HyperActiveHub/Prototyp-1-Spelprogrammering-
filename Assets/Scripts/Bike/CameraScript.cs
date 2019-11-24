using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform followObj;
    public float offset = 5f;

    void Start()
    {
    }

    void Update()
    {
        Vector3 targetPos = new Vector3(followObj.position.x + offset, transform.position.y, transform.position.z);

        transform.position = targetPos;//Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * maxSpeed);

    }
}
