using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bouncePower = 10;

    private bool isAnimPlaying = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAnimPlaying)
            return;

        isAnimPlaying = true;
        animator.SetTrigger("isOnTrigger");
        StartCoroutine(ForceDelay(collision.gameObject));
    }

    private IEnumerator ForceDelay(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.7f);
        isAnimPlaying = false;
    }
}
