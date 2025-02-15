using System.Collections;
using UnityEngine;

public class OnTiggerAnimation : Interactable
{
    [Header("Animation Cooldown")]
    [SerializeField] private float coolDown = 1f;
    private bool isAnimPlaying = false;

    [Header("Start by stateName")]
    [SerializeField] private string animationName = "OnTriggerAnimation";
    [SerializeField] private bool useName = false;

    [Header("Start by parameter")]
    [SerializeField] private string animationParameter = "isActivated";
    [SerializeField] private bool useParameter = false;

    [Header("SFX Parameters")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 1)] private float volume = 1;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        //nothing
    }

    public override void OnTrigger()
    {
        if (!isAnimPlaying)
        {
            isAnimPlaying = true;

            if (useName)
                animator.Play(animationName);
            else if (useParameter)
                animator.SetBool(animationParameter, true);

            AudioManager.PlaySFX?.Invoke(audioClip, volume);
            StartCoroutine(InteractCoolDown());
        }
    }

    private IEnumerator InteractCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        isAnimPlaying = false;
    }
}
