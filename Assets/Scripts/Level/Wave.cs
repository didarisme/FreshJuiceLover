using System.Collections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float waveSpeed = 4.5f;
    [SerializeField] private float waveBobSpeed = 0.5f;
    [SerializeField] private float waveBobAmount = 0.5f;
    [SerializeField] private float mindDistance = 10f;

    private Transform player;
    private Rigidbody2D rgb;
    private float defaultPosY;
    private float timer;
    private bool showMind = true;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rgb = GetComponent<Rigidbody2D>();
        defaultPosY = transform.position.y;
    }

    private void Update()
    {
        timer += Time.deltaTime * waveBobSpeed;
        transform.position = new Vector3(transform.position.x, defaultPosY + Mathf.Sin(timer) * waveBobAmount, transform.position.z);

        if (player.position.x - transform.position.x < mindDistance && showMind)
        {
            StartCoroutine(StopThinking());
            PlayerMind.ShowBubble?.Invoke(2);
        }
    }

    private void FixedUpdate()
    {
        rgb.velocity = Vector2.right * waveSpeed;
    }

    private IEnumerator StopThinking()
    {
        showMind = false;
        yield return new WaitForSeconds(5);

        PlayerMind.ShowBubble?.Invoke(-1);
    }
}
