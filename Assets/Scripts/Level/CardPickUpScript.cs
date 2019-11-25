using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class CardPickUpScript : MonoBehaviour
{
    public UnityAction onCardPickup = delegate { };
    [SerializeField]
    Color color;

    public Color Color
    {
        get
        {
            return color;
        }
    }

    private void OnDrawGizmos()
    {
        Start();
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().color = color;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManagerScript.isOtherTaggedPlayer(collision.transform, this))
        {
            Debug.Log("Card picked up", this);
            onCardPickup();
            gameObject.SetActive(false);
        }
    }
}
