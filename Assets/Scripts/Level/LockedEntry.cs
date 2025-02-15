using UnityEngine;

public class LockedEntry : MonoBehaviour
{
    [SerializeField] private Lever lever;
    [SerializeField] private GameObject blocks;

    private void OnEnable()
    {
        lever.OnUseLever += UnlockPassage;
    }

    private void OnDisable()
    {
        lever.OnUseLever -= UnlockPassage;
    }

    private void UnlockPassage()
    {
        blocks.SetActive(false);
    }
}
