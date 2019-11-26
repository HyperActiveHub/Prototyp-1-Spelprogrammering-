using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    Sprite activatedSprite;
    [SerializeField]
    Sprite inactiveSprite;
    [SerializeField]
    List<ButtonScript> dependencies = new List<ButtonScript>();
    [SerializeField]
    bool isToggle;
    [SerializeField]
    bool startActive;

    public UnityAction<Object> onPressed = delegate { };
    bool toggle;
    protected SpriteRenderer sRenderer;

    protected void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (dependencies.Count == 0 && isToggle)
        {
            Debug.LogError("No button found in dependencies, this button will always be toggled off.", this);
        }

        enabled = false;

        if (dependencies.Count != 0)
        {
            foreach (ButtonScript button in dependencies)
            {
                if (button != this)
                {
                    button.onPressed += Activate;

                    if (inactiveSprite != null)
                    {
                        sRenderer.sprite = inactiveSprite;
                    }
                    else
                    {
                        sRenderer.sprite = activatedSprite;
                        Debug.LogWarning("No inactive sprite set to this button.", this);
                    }
                }
                else
                {
                    Debug.LogWarning("Can't add button to be dependant on itself to be activated.", this);
                    break;
                }
            }
        }
        else if (!isToggle)
            startActive = true;

        if (startActive)
        {
            Activate(this);
        }
    }

    void Activate(Object o)
    {
        if (toggle && isToggle)
        {
            toggle = false;
        }
        else if (!isToggle)
        {
            toggle = true;
        }
        else
            toggle = true;

        if (!enabled)
        {
            enabled = true;
            sRenderer.sprite = activatedSprite;
            if (o == this)
                Debug.Log(this + " activated by self", this);
            else
                Debug.Log(this + " activated by " + o, this);
        }
        else if (enabled)
        {
            enabled = false;
            sRenderer.sprite = inactiveSprite;
            if (o == this)
                Debug.Log(this + " deactivated by self", this);
            else
                Debug.Log(this + " deactivated by " + o, this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            if (GameManagerScript.isOtherTaggedPlayer(collision.transform, this))
            {
                onPressed(this);

                if (!isToggle)
                {
                    foreach (ButtonScript button in dependencies)
                    {
                        button.onPressed -= Activate;
                    }
                    Debug.Log(this + " pressed", this);
                }
                else
                {
                    Debug.Log(this + " toggled (" + toggle + ")", this);
                }

                Activate(this);
            }
        }
    }
}