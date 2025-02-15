using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private bool useDisappear;
    private Animator animator;
    private PlayerMove playerMove;
    private PlayerTriggers playerTriggers;

    private bool isFacingRight = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerTriggers = GetComponent<PlayerTriggers>();
    }

    private void OnEnable()
    {
        playerMove.PlayerVelocity += SetPlayerAnim;
        playerTriggers.OnDeath += SetDisappear;
    }

    private void OnDisable()
    {
        playerMove.PlayerVelocity -= SetPlayerAnim;
        playerTriggers.OnDeath -= SetDisappear;
    }

    private void SetPlayerAnim(Vector2 velocity, bool isGrounded)
    {
        HandleSpriteFlip(velocity.x);

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        animator.SetFloat("xVelocity", Mathf.Abs(velocity.x));
        animator.SetFloat("yVelocity", velocity.y);
    }

    private void HandleSpriteFlip(float velocityX)
    {
        if (isFacingRight && velocityX > 0 || !isFacingRight && velocityX < 0)
        {
            isFacingRight = !isFacingRight;
            gameObject.GetComponent<SpriteRenderer>().flipX = isFacingRight;
        }
    }

    private void SetDisappear()
    {
        if (useDisappear)
        {
            Freeze(true);

            animator.SetBool("isJumping", false);
            animator.SetTrigger("isDisappearing");
        }
        else
        {
            DisappearEnd();
        }
    }

    private void DisappearEnd()
    {
        SceneChanger.LoadSceneByInd(-1);
    }

    private void AppearEnd()
    {
        Freeze(false);
    }

    private void Freeze(bool isFreezed)
    {
        GetComponent<PlayerMove>().enabled = !isFreezed;
        GetComponent<Collider2D>().enabled = !isFreezed;

        GetComponent<Rigidbody2D>().constraints = isFreezed ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
    }
}
