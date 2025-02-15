using System;
using UnityEngine;

public class SceneProperties : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] private Vector2 newGravity = new Vector2(0, -9.81f);

    [Header("Level Properties")]
    [SerializeField] private Color backgroundColor = Color.white;
    [SerializeField] private float levelRangeY = 10f;

    private PlayerTriggers playerTriggers;

    private Transform player;
    private Transform world;

    public event Action<Color> SetBackgoundColor;

    private void Start()
    {
        playerTriggers = FindObjectOfType<PlayerTriggers>();

        Physics2D.gravity = newGravity;
        SetBackgoundColor?.Invoke(backgroundColor);

        player = GameObject.FindWithTag("Player").transform;
        world = GameObject.Find("World").transform;
    }

    private void Update()
    {
        if (Mathf.Abs(player.position.y) > world.position.y + levelRangeY)
        {
            if (player.GetComponent<Collider2D>().enabled == true)
                playerTriggers.OnDeath?.Invoke();
        }
    }
}