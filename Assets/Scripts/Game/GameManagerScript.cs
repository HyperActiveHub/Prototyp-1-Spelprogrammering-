using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerScript : MonoBehaviour
{

    private static GameManagerScript _instance = null;
    public static GameManagerScript Instance    //Singleton - Design pattern
    {
        get
        {
            if (_instance == null)
            {
                GameObject gm = new GameObject("Game Manager");
                _instance = gm.AddComponent<GameManagerScript>();   //Lazy - Design pattern
            }
            return _instance;
        }
    }

    List<GoalPickUpScript> pickupConditions = new List<GoalPickUpScript>();

    private GoalScript goal;
    public GoalScript Goal
    {
        get
        {
            if (goal != null)
            {
                return goal;
            }
            else
            {
                Debug.LogError("No Goal object assigned! Have you forgotten to place a goal?", this);
                return null;    //Avoid crashing?
            }
        }

        set
        {
            if (goal == null || value == null)
            {
                goal = value;
            }
            else
                Debug.LogError("Goal is already assigned, only one goal can exist in a level.", this);
        }
    }

    //Many to one relation required for GoalPickUpScript - GoalScript.

    public static bool isOtherTaggedPlayer(Transform other, Object script)      //Helper function - Design pattern
    {
        if (other.parent.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Object without 'Player' tag collided with " + script + ".", script);
            return false;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if((pickupConditions.Count == 0))
        {
            goal.ConditionsMet();
        }
    }

    public void RegisterPickupCondition(GoalPickUpScript o)
    {
        print("called");
        if (!pickupConditions.Contains(o))
        {
            pickupConditions.Add(o);
        }
    }

    void Update()
    {

    }

    public void OnPickup(object o)  //Called by GoalPickUpScript (and possibly other pickups)      //Mediator? - Design pattern
    {
        //Tell the goalScript that an object was picked up, if it is relevant
        if (o.GetType() == typeof(GoalPickUpScript))
        {
            if (AreGoalConditionsMet())
            {
                goal.ConditionsMet();
            }
        }
        //Define for colored pickup-buttons
        else
            Debug.LogError("Behaviour not defined for this object OnPickup");
    }

    bool AreGoalConditionsMet()
    {
        foreach (GoalPickUpScript condition in pickupConditions)
        {
            if (condition.enabled)
            {
                return false;
            }
        }
        return true;
    }

}