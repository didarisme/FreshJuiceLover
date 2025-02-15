using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 3f;

    [SerializeField] private Vector3 boxOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector2 boxSize = new Vector2(0.4f, 0.05f);

    [SerializeField] private LayerMask mask;

    private float horizontalInput;
    private bool isGrounded = false;
    private bool isAxisDown = true;
    private bool canMove = true;
    private bool shouldJump;

    private Rigidbody2D rb;

    public event Action<Vector2, bool> PlayerVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove)
            HandleInput();

        HandleGroundCheck();

        if (shouldJump)
            HandleJump();
    }

    private void FixedUpdate()
    {
        ApplyFinalMovements();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Jump") > 0)
        {
            if (isAxisDown && isGrounded)
            {
                shouldJump = true;
                isAxisDown = false;
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 0 || Input.GetAxisRaw("Jump") == 0)
        {
            isAxisDown = true;
        }
    }

    private void HandleGroundCheck()
    {
        isGrounded = Physics2D.OverlapBox(transform.position - boxOffset * transform.localScale.x, boxSize * transform.localScale.x, 0f, mask);
    }

    private void HandleJump()
    {
        shouldJump = false;

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jump * transform.localScale.x, ForceMode2D.Impulse);
    }

    private void ApplyFinalMovements()
    {
        rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);
        PlayerVelocity?.Invoke(rb.velocity, isGrounded);
    }

    public void LockInput(bool newBool)
    {
        canMove = !newBool;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position - boxOffset * transform.localScale.x, boxSize * transform.localScale.x);
    }
}
