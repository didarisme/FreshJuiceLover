using UnityEngine;

public class TeleportDoor : Interactable
{
    [Header("Teleport Parameters")]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector2 cameraPosition;
    [SerializeField] private int bubbleIndex;
    [SerializeField] private bool isOntrigger;

    [Header("SFX Parameters")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 1)] private float volume = 1;

    private Transform mainCamera;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main.transform;
    }

    public override void OnInteract()
    {
        if (!isOntrigger)
        {
            Teleport();
        }
    }

    public override void OnTrigger()
    {
        if (isOntrigger)
        {
            AudioManager.PlaySFX(audioClip, volume);
            Teleport();
        }
        else
        {
            PlayerMind.ShowBubble(bubbleIndex);
        }
    }

    private void Teleport()
    {
        player.position = playerPosition;
        mainCamera.position = new Vector3(cameraPosition.x, cameraPosition.y, mainCamera.position.z);
    }
}
