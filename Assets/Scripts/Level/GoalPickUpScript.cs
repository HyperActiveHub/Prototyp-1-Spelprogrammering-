using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPickUpScript : MonoBehaviour
{
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
        }
        else
            Debug.LogWarning("Object without 'Player' tag collided with pickup Trigger.", this);
    }
}
