using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private SceneChanger sceneChanger;
    private Animator animator;
    private Vector3 cameraPos;

    private Vector3 currentCheckpoint;

    private void Awake()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        sceneChanger.OnRespawn += Respawn;
    }

    private void OnDisable()
    {
        sceneChanger.OnRespawn -= Respawn;
    }

    private void Start()
    {
        currentCheckpoint = transform.position;
        cameraPos = Camera.main.transform.position;
    }

    private void Respawn()
    {
        transform.position = currentCheckpoint;
        Camera.main.transform.position = cameraPos;

        animator.SetTrigger("isAppearing");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform.position;
            cameraPos = new Vector3(collision.transform.position.x, 0.5f, -10f);
        }
    }
}
