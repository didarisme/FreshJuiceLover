using UnityEngine;

public class WFValueChanger : MonoBehaviour
{
    [SerializeField] private Vector2 force;

    public Vector3 SetNewForce()
    {
        return force;
    }
}
