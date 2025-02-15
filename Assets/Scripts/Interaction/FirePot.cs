using System.Collections;
using UnityEngine;

public class FirePot : Interactable 
{
    [SerializeField] private string onTiggerPar = "isOnTrigger";

    [SerializeField] private float cooldown = 1f;

    private Animator animator;
    private bool isAnimPlaying = false;

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
        if (isAnimPlaying)
            return;

        isAnimPlaying = true;
        animator.SetTrigger(onTiggerPar);

        StartCoroutine(InteractCoolDown());
    }

    private IEnumerator InteractCoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        isAnimPlaying = false;
    }
}