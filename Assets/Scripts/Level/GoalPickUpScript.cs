using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickUpScript : MonoBehaviour
{
    private void Awake()
    {
        
    }

    void Start()
    {
        GameManagerScript.Instance.RegisterPickupCondition(this);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            GameManagerScript.Instance.OnPickup(this);
            enabled = false;
            gameObject.SetActive(false);
        }
        else
            Debug.LogWarning("Object without 'Player' tag collided with pickup Trigger.", this);
    }
}
