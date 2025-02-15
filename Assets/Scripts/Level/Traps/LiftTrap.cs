using UnityEngine;

public class LiftTrap : MonoBehaviour
{
    [SerializeField] private Transform[] point;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float speedMultiplier = 2f;

    private Vector3 nextPosition;
    private Transform player;
    private bool isGoingUp;

    private void Start()
    {
        nextPosition = point[0].position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;
            player.SetParent(transform);
            isGoingUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.position.y < point[1].position.y + 0.5f)
            {
                player.SetParent(null);
                isGoingUp = false;
            }
        }
    }

    private void Update()
    {
        float currentSpeed = moveSpeed;

        if (isGoingUp)
        {
            if (transform.position == nextPosition)
            {
                nextPosition = nextPosition == point[1].position ? point[0].position : point[1].position;
            }
            else if (nextPosition == point[1].position && transform.position.y > point[2].position.y)
            {
                currentSpeed = moveSpeed * speedMultiplier;
            }
        }
        else
        {
            nextPosition = point[0].position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, currentSpeed * Time.deltaTime);
    }
}