using System.Collections;
using UnityEngine;

public class Fruit : Interactable
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        //Sorry, nothing here :(
    }

    public override void OnTrigger()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(PickUpFruit());
    }

    private IEnumerator PickUpFruit()
    {
        LevelProgress.PickUpFruit?.Invoke();
        animator.Play("Collected");
        AudioManager.PlaySFX?.Invoke(null, 0.3f);

        float timer = 0;

        while(timer < 1)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
