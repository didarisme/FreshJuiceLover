using UnityEngine;

public class WildFruit : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("WildFruit Parameters")]
    [SerializeField, Space] private Vector2 speedRange;
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float groundCheckDistance = 1f;

    private float currentSpeed;
    private bool isNear;
    private bool isJumping;

    private Rigidbody2D rgb;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    private void FixedUpdate()
    {
        if (isNear && !isJumping)
        {
            MoveFruit();
        }

        HandleGroundCheck();
    }

    private void HandleGroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("WildFruit/Jump"))
            {
                if (!isJumping)
                {
                    isJumping = true;

                    Jump(jumpForce);
                }
            }
            else if (hit.collider.CompareTag("WildFruit/NewForce"))
            {
                jumpForce = hit.collider.GetComponent<WFValueChanger>().SetNewForce();
            }
            else if (isJumping)
            {
                isJumping = false;
            }
        }
    }

    private void Jump(Vector2 force)
    {
        rgb.velocity = Vector2.zero;
        rgb.AddForce(force, ForceMode2D.Impulse);
    }

    private void MoveFruit()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < detectionRange)
        {
            currentSpeed = Mathf.Lerp(speedRange.y, speedRange.x, distance / detectionRange);

            rgb.velocity = new Vector2(currentSpeed, rgb.velocity.y);
        }
        else
        {
            isNear = false;
        }
    }
}