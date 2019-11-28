using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickUpScript : MonoBehaviour
{
    private void Awake()
    {
        GameManagerScript.Instance.RegisterPickupCondition(this);
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            enabled = false;
            gameObject.SetActive(false);
            GameManagerScript.Instance.OnPickup(this);  //Order is important, enabled = false must be called before OnPickup.
        }
        else
            Debug.LogWarning("Object without 'Player' tag collided with pickup Trigger.", this);
    }
}
