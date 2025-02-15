using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField, Space] private Transform focus;
    [SerializeField] private float smoothTime = 2f;
    [SerializeField] private float newRange;

    [SerializeField, Space] private float newHeight = 1f;
    [SerializeField] private bool ChangeOnlyHeight;

    [SerializeField, Space] private bool DoNotStopMove;

    private Vector3 velocity = Vector3.zero;

    private bool isFocus;

    private void Start()
    {
        if (newRange == 0)
        {
            newRange = Mathf.Abs(focus.position.x - transform.position.x);
            Debug.Log("Calculated range: " + newRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!ChangeOnlyHeight)
                PlayerCamera.OnFocus(newRange);

            isFocus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerCamera.OnFocus(323);
            isFocus = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFocus)
        {
            if (ChangeOnlyHeight)
            {
                ChangeHeight();
            }
            else
            {
                MoveToFocus(focus.position);
            }
        }
    }

    private void MoveToFocus(Vector3 targetPosition)
    {
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);

        if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.001f && !DoNotStopMove)
        {
            isFocus = false;
        }
    }

    private void ChangeHeight()
    {
        Vector3 targetHeight = new Vector3(cameraTransform.position.x, newHeight, cameraTransform.position.z);

        MoveToFocus(targetHeight);
    }
}
