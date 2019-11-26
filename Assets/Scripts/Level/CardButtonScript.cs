using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class CardButtonScript : ButtonScript
{
    [SerializeField]
    Sprite activatedS;
    [SerializeField]
    Sprite inactiveSp;
    [SerializeField]
    CardPickUpScript card;

    bool activated;

    private void OnDrawGizmos()
    {
        SetColor();
    }

    private void Start()
    {
        if (card != null)
        {
            sRenderer.sprite = inactiveSp;
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

    void Activate()
    {
        activated = true;
        sRenderer.sprite = activatedS;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            if (GameManagerScript.isOtherTaggedPlayer(collision.transform, this))
            {
                if (activated)
                {
                    onPressed(this);
                    enabled = false;
                    Destroy(this);
                }
            }
        }
    }
}