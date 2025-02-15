using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float cooldown = 2f;

    private Vector3 nextPosition;
    private int currentPoint;

    private bool movingForward;
    private bool isCooldown;

    private void Start()
    {
        nextPosition = points[0].position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if (transform.position == nextPosition && !isCooldown)
        {
            if (currentPoint == points.Length - 1 || currentPoint == 0)
                movingForward = !movingForward;

            isCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);

        currentPoint = (movingForward ? currentPoint + 1 : currentPoint - 1);
        nextPosition = points[currentPoint].position;
        isCooldown = false;
    }
}