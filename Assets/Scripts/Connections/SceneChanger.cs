using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private float newTime = 0f;

    [SerializeField] private bool useCheckpoints;

    private Animator animator;
    private int sceneToLoad;

    public static Action<int> LoadSceneByInd;
    public event Action OnRespawn;

    private void OnEnable()
    {
        LoadSceneByInd += LoadScene;
    }

    private void OnDisable()
    {
        LoadSceneByInd -= LoadScene;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LoadScene(int sceneInd)
    {
        if (sceneInd < 0)
        {
            if (useCheckpoints && sceneInd > -10)
            {
                OnRespawn?.Invoke();
                return;
            }
            else
            {
                sceneToLoad = SceneManager.GetActiveScene().buildIndex;
            }
        }
        else
        {
            sceneToLoad = sceneInd;
        }

        animator.SetTrigger("isFade");
        Time.timeScale = newTime;
    }

    public void OnFadeComplete()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}