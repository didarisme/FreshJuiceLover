using UnityEngine;

public class DisappearObject : MonoBehaviour
{
    [SerializeField] private GameObject disappearObject;

    [Header("SFX Parameters")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 1)] private float volume = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && disappearObject.activeSelf)
        {
            AudioManager.PlaySFX(audioClip, volume);
            disappearObject.SetActive(false);
        }
    }
}
