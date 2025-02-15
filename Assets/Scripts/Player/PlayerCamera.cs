using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private float smoothTime = 0.3f;

    [SerializeField] private SceneProperties sceneProperties;

    private float defaultMoveRange;
    private float posX;

    private Vector3 velocity = Vector3.zero;

    public static Action<float> OnFocus;

    private void OnEnable()
    {
        sceneProperties.SetBackgoundColor += ChangeColor;
        OnFocus += ChangeRange;
    }

    private void OnDisable()
    {
        sceneProperties.SetBackgoundColor -= ChangeColor;
        OnFocus += ChangeRange;
    }

    private void Start()
    {
        defaultMoveRange = moveRange;
    }

    private void FixedUpdate()
    {
        posX = Mathf.Clamp(transform.position.x, player.position.x - moveRange, player.position.x + moveRange);

        Vector3 targetPosition = new Vector3(posX, transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void ChangeColor(Color newColor)
    {
        GetComponent<Camera>().backgroundColor = newColor;
    }

    private void ChangeRange(float newRange)
    {
        if (newRange == 323f)
        {
            moveRange = defaultMoveRange;
        }
        else
        {
            moveRange = newRange;
        }
    }
}
