using System;
using System.Collections;
using UnityEngine;

public class Lever : Interactable
{
    [Header("Lever Parameters")]
    [SerializeField] private float coolDown = 1f;

    [Header("SFX Parameters")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 1)] private float volume = 1;

    private Animator animator;
    private bool isAnimPlaying = false;

    public event Action OnUseLever;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        if (!isAnimPlaying)
        {
            isAnimPlaying = true;
            animator.Play("OnInteract");
            AudioManager.PlaySFX(audioClip, volume);

            OnUseLever?.Invoke();

            StartCoroutine(InteractCoolDown());
        }
    }

    public override void OnTrigger()
    {
        PlayerMind.ShowBubble?.Invoke(0);
    }

    private IEnumerator InteractCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        isAnimPlaying = false;
    }
}