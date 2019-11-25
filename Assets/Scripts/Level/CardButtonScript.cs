using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class CardButtonScript : MonoBehaviour   //Couldve inhereted from ButtonScript but opted not to
{                                               //Since it forced the private SerializedFields to be Serialized
    [SerializeField]
    Sprite activatedSprite;
    [SerializeField]
    Sprite inactiveSprite;
    [SerializeField]
    CardPickUpScript card;

    public UnityAction onPressed = delegate { };
    bool activated;
    SpriteRenderer sRenderer;

    private void OnDrawGizmos()
    {
        SetColor();
    }

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();

        if (card != null)
        {
            SetColor();
            card.onCardPickup += Activate;
        }
        else
        {
            Debug.LogError("Card Button must have a related Card.", this);
        }
    }

    void SetColor()
    {
        GetComponent<SpriteRenderer>().color = card.Color;
    }

    protected void Activate()
    {
        activated = true;
        Debug.Log("Colored Button activated", this);
        sRenderer.sprite = activatedSprite;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManagerScript.isOtherTaggedPlayer(collision.transform, this))
        {
            if (activated)
            {
                onPressed();
                Debug.Log("Colored Button pressed.", this);
                //enabled = false;
                Destroy(this);
            }
        }
    }
}