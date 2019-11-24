using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    GoalPickUpScript[] conditions; //new List<GoalPickUpScript>();

    //listen for condition completions.. and change sprite to show that the level can be completed.

    void Start()
    {
        conditions = FindObjectsOfType<GoalPickUpScript>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            if (AreConditionsMet())
            {
                Debug.Log("Completed Level.");
            }
        }
        else
            Debug.LogWarning("Object without 'Player' tag collided with Goal Trigger.", this);
    }

    bool AreConditionsMet()
    {
        foreach (GoalPickUpScript pickup in conditions)
        {
            if (pickup.enabled == true)     //needs testing
            {
                return false;
            }
        }
        return true;
    }

}
