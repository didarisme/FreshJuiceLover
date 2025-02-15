using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private ParticleSystem particle;

    public event Action<bool> OnPause;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);

        OpenWindows(-1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        OpenWindows(-1);
        SetProperties(false);
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Pause()
    {
        SetProperties(true);
        particle.Play();
    }

    private void SetProperties(bool newState)
    {
        isPaused = newState;
        pauseMenuUI.SetActive(newState);
        OnPause?.Invoke(newState);
        Time.timeScale = newState ? 0 : 1;
    }

    public void OpenWindows(int windowIndex)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(i == windowIndex);
        }
    }

    public void ReStartLevel()
    {
        Time.timeScale = 1f;
        SceneChanger.LoadSceneByInd(-10);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneChanger.LoadSceneByInd(0);
    }
}