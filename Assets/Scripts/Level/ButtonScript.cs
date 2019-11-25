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

    public UnityAction<bool, Object> onPressed = delegate { };
    bool toggle;
    SpriteRenderer sRenderer;

    void Start()
    {
        if (dependencies.Count == 0 && isToggle)
        {
            Debug.LogError("No button found in dependencies, this button will always be toggled off.", this);
        }

        enabled = false;
        sRenderer = GetComponent<SpriteRenderer>();

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
        else if(!isToggle)
            startActive = true;

        if(startActive)
        {
            Activate(true, this);
        }
    }

    protected void Activate(bool onOff, Object o)
    {
        if(onOff && toggle && isToggle)
        {
            onOff = false;
        }
        toggle = onOff;

        if (toggle)
        {
            enabled = true;
            sRenderer.sprite = activatedSprite;
            if (o == this)
                Debug.Log("Button activated by self", this);
            else
                Debug.Log("Button activated by " + o, this);
        }
        else if (enabled)
        {
            enabled = false;
            sRenderer.sprite = inactiveSprite;
            if (o == this)
                Debug.Log("Button deactivated by self", this);
            else
                Debug.Log("Button deactivated by " + o, this);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled)
        {
            if (GameManagerScript.isOtherTaggedPlayer(collision.transform, this))
            {
                onPressed(toggle, this);

                if (!isToggle)
                {
                    foreach(ButtonScript button in dependencies)
                    {
                        button.onPressed -= Activate;
                    }
                    Debug.Log("Button pressed", this);
                }
                else
                {
                    
                    Debug.Log("Button toggled (" + toggle + ")", this);
                }

                Activate(false, this);
            }
        }
    }
}