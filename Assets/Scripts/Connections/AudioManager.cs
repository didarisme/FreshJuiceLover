using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] private AudioClip soundTrack;
    [SerializeField] private AudioClip errorSound;

    [Header("Other")]
    [SerializeField] private PauseMenu pauseMenu;

    public static Action<AudioClip, float> PlaySFX;

    private void OnEnable()
    {
        PlaySFX += PlaySound;

        if (pauseMenu != null)
        {
            pauseMenu.OnPause += PauseSound;
        }
    }

    private void OnDisable()
    {
        PlaySFX -= PlaySound;

        if (pauseMenu != null)
        {
            pauseMenu.OnPause -= PauseSound;
        }
    }

    private void Start()
    {
        musicSource.volume = PlayerPrefs.GetFloat("Volume Music", 0.2f);
        audioSource.volume = PlayerPrefs.GetFloat("Volume SFX", 1f);

        musicSource.clip = soundTrack;
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            audioSource.PlayOneShot(errorSound, volume);
        }
    }

    private void PauseSound(bool isPaused)
    {
        if (isPaused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}