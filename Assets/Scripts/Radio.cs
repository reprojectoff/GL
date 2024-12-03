using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Radio : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    public Button toggleButton;
    public Button nextButton;
    public Slider volumeSlider;

    private bool isPlaying = false;

    public CanvasGroup button;
    public float fadeDuration;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        toggleButton.onClick.AddListener(ToggleMusic);
        nextButton.onClick.AddListener(NextTrack);

        button.interactable = false;
        button.alpha = 0f;

        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

    }
    void Update()
    {
        if (!audioSource.isPlaying && isPlaying)
        {
            NextTrack();
        }
    }
    void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeIn(button)); 
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(button));
        }
    }

    void ToggleMusic()
    {
        if (isPlaying)
        {
            PauseMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        if (audioClips.Length == 0) return;

        audioSource.clip = audioClips[currentTrackIndex];
        audioSource.Play();
        isPlaying = true;
        //toggleButton.GetComponentInChildren<Text>().text = "stop";
    }

    void PauseMusic()
    {
        audioSource.Pause();
        isPlaying = false;
        //toggleButton.GetComponentInChildren<Text>().text = "play";
    }
    void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Length;
        PlayMusic();
    }
    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }
    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }
        canvasGroup.interactable = false;
    }
}