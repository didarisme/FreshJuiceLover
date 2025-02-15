using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [Header("SFX Parameters")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 1)] private float volume = 1;

    [Header("Other")]
    [SerializeField][Range(5, 9)] private float jumpPower = 8;

    private Rigidbody2D rb;
    private Rigidbody2D player;

    private PlayerMove playerMove;
    private PlayerData playerData;

    private bool isPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerMove = player.GetComponent<PlayerMove>();

        playerData = FindObjectOfType<PlayerData>();
        isPressed = playerData.LoadPlayerPosition(player.gameObject, gameObject);
    }

    private void Start()
    {
        if (isPressed)
            return;

        playerMove.LockInput(true);

        PlayerMind.ShowBubble(1);
        PlayerCamera.OnFocus(9);
    }

    public void PlayBtn()
    {
        AudioManager.PlaySFX?.Invoke(audioClip, volume);

        if (isPressed)
            return;

        StartCoroutine(PushPlayer());
        playerData.PlayBtnPressed();

        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity = 24;
    }

    private IEnumerator PushPlayer()
    {
        isPressed = true;

        yield return new WaitForSeconds(2);

        PlayerMind.ShowBubble(-1);

        player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        playerMove.LockInput(false);
        PlayerCamera.OnFocus(323);

        gameObject.SetActive(false);
    }
}