using System.Collections;
using UnityEngine;

public class FireWay : MonoBehaviour
{
    [Header("FirePots and Fire Colliders")]
    [SerializeField] private Animator[] animators;
    [SerializeField] private Collider2D[] colliders;

    [Header("Parameters")]
    [SerializeField] private float fireSpeed = 0.2f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private int divider = 2;

    private bool isFire = true;

    private void Start()
    {
        colliders = new Collider2D[animators.Length];
        for (int i = 0; i < animators.Length; i++)
        {
            colliders[i] = animators[i].gameObject.GetComponentsInChildren<Collider2D>()[1];
        }
    }

    private void Update()
    {
        if (isFire)
        {
            isFire = false;
            StartCoroutine(OnFireWay());
        }
    }

    IEnumerator OnFireWay()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            SetFirePot(i, true);

            yield return new WaitForSeconds(fireSpeed);
        }

        yield return new WaitForSeconds(cooldown);

        for (int i = 0; i < animators.Length; i++)
        {
            SetFirePot(i, false);

            if (animators.Length / divider == i)
            {
                isFire = true;
            }

            yield return new WaitForSeconds(fireSpeed);
        }
    }

    private void SetFirePot(int index, bool newBool)
    {
        animators[index].SetBool("isOnFire", newBool);
        colliders[index].enabled = newBool;
    }
}