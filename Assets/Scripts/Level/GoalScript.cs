using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    //listen for condition completions.. and change sprite to show that the level can be completed.

    bool conditionsMet;
    SpriteRenderer sprite;

    void Start()
    {
        GameManagerScript.Instance.Goal = this;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            if (conditionsMet)
            {
                Debug.Log("Completed Level.");
            }
            else
            {
                Debug.LogWarning("You need to complete all the conditions for this level before you can complete it.", this);
            }
        }
        else
            Debug.LogWarning("Object without 'Player' tag collided with Goal Trigger.", this);
    }

    public void ConditionsMet()
    {
        conditionsMet = true;
        sprite.color = Color.green;
        //Change sprite of goal to show that it is possible to complete the level now.
    }

}
