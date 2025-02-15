using System;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private Interactable currentInteractable;
    public Action OnDeath;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactive")
        {
            if (currentInteractable == null || collision.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID())
            {
                collision.gameObject.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnTrigger();
                }
            }
        }
        else if (currentInteractable)
        {
            currentInteractable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone")
        {
            OnDeath?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactive")
        {
            currentInteractable = null;
            PlayerMind.ShowBubble?.Invoke(-1);
        }
    }
}
