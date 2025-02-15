using UnityEngine;

public class FollowIt : MonoBehaviour
{
    [SerializeField] private Transform follow;

    private void Update()
    {
        transform.position = new Vector3(follow.position.x, transform.position.y, transform.position.z);
    }
}
