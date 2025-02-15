using System.Collections;
using UnityEngine;

public class UnexpectedTrap : MonoBehaviour
{
    [Header("Trap Parameters")]
    [SerializeField] private Transform[] spikes;
    [SerializeField] private Transform waypoint;
    [SerializeField] private float trapSpeed = 6f;

    [Header("SFX Parameters")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 1)] private float volume = 1;

    private Vector3 defaultPosition;
    private Animator[] anims;

    private void Start()
    {
        defaultPosition = transform.position;
        anims = new Animator[spikes.Length];

        for (int i = 0; i < anims.Length; i++)
        {
            anims[i] = spikes[i].transform.GetComponent<Animator>();
            if (anims[i] == null)
            {
                Debug.Log("No");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateTrap(true);
    }

    private void ActivateTrap(bool newBool)
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].transform.SetParent(transform);
        }

        anims[0].SetBool("isFromLeft", newBool);
        anims[1].SetBool("isFromRight", newBool);

        StartCoroutine(MoveTrap());
    }

    private IEnumerator MoveTrap()
    {
        yield return new WaitForSeconds(1);

        AudioManager.PlaySFX(audioClip, volume);

        while (transform.position != waypoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, trapSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.4f);

        while (transform.position != defaultPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, defaultPosition, trapSpeed / 4 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
