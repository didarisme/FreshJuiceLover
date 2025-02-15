using UnityEngine;
using UnityEngine.UI;

public class SoundScroller : MonoBehaviour
{
    [SerializeField] private Image[] volumeBars;
    [SerializeField] private Image[] selectedBars;
    [SerializeField] private Text[] volumeTexts;
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private Lever lever;

    private Rigidbody2D rgb;
    private float volume;
    private int currentMixer;
    private bool isFirst = true;
    private bool isUsing;

    private const float MinPositionX = 0f;
    private const float MaxPositionX = 10f;
    private const float FixedYPosition = 0.5f;

    private void OnEnable()
    {
        lever.OnUseLever += SetMixer;
    }

    private void OnDisable()
    {
        lever.OnUseLever -= SetMixer;
    }

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();

        SetPositionAndFreeze(new Vector3(audioSources[currentMixer].volume * MaxPositionX, FixedYPosition));

        for (int i = 0; i < audioSources.Length; i++)
        {
            volumeBars[i].fillAmount = audioSources[i].volume;
            volumeTexts[i].text = (audioSources[i].volume * 100).ToString();
        }
    }

    private void SetMixer()
    {
        isFirst = !isFirst;

        currentMixer = (currentMixer + 1) % volumeBars.Length;
        UpdateSelectedBar();

        SetPositionAndFreeze(new Vector3(audioSources[currentMixer].volume * MaxPositionX, FixedYPosition));
    }

    private void Update()
    {
        if (isUsing)
        {
            CheckBounds();
            UpdateVolume();
        }
    }

    private void CheckBounds()
    {
        if (transform.localPosition.x < MinPositionX)
        {
            SetPositionAndFreeze(new Vector3(MinPositionX, FixedYPosition));
        }
        else if (transform.localPosition.x > MaxPositionX)
        {
            SetPositionAndFreeze(new Vector3(MaxPositionX, FixedYPosition));
        }
    }

    private void UpdateVolume()
    {
        volume = Mathf.Round(transform.localPosition.x * 10f);
        volumeBars[currentMixer].fillAmount = volume / 100f;
        volumeTexts[currentMixer].text = volume.ToString();
        audioSources[currentMixer].volume = volume / 100f;
    }

    private void UpdateSelectedBar()
    {
        for (int i = 0; i < selectedBars.Length; i++)
        {
            selectedBars[i].enabled = (i == currentMixer);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isUsing = true;
        rgb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isUsing = false;
        rgb.constraints = RigidbodyConstraints2D.FreezeAll;
        SetVolume();
    }

    private void SetVolume()
    {
        PlayerPrefs.SetFloat("Volume Music", audioSources[0].volume);
        PlayerPrefs.SetFloat("Volume SFX", audioSources[1].volume);
    }

    private void SetPositionAndFreeze(Vector3 position)
    {
        transform.localPosition = position;
        rgb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
