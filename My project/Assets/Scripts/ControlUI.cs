using System.Collections;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup[] uiElements;

    [Header("Audio")]
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    [Header("Animación")]
    public float fadeDuration = 0.5f;
    public float visibleTime = 5f;

    private bool targetFound;
    private bool isShowing;
    private int currentIndex;

    private void Start()
    {
        HideAllInstant();
    }

    public void OnTargetFound()
    {
        targetFound = true;
    }

    public void OnTargetLost()
    {
        targetFound = false;
        isShowing = false;
        currentIndex = 0;

        if (audioSource != null)
            audioSource.Stop();

        HideAllInstant();
    }

    private void Update()
    {
        if (!targetFound || isShowing)
            return;

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartCoroutine(ShowCurrentUI());
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShowCurrentUI());
        }
#endif
    }

    private IEnumerator ShowCurrentUI()
    {
        if (currentIndex >= uiElements.Length)
            yield break;

        isShowing = true;

        CanvasGroup current = uiElements[currentIndex];

        // Reproducir audio correspondiente
        if (audioSource != null &&
            audioClips != null &&
            currentIndex < audioClips.Length &&
            audioClips[currentIndex] != null)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();
        }

        yield return StartCoroutine(FadeIn(current));

        yield return new WaitForSeconds(visibleTime);

        yield return StartCoroutine(FadeOut(current));

        currentIndex++;

        isShowing = false;
    }

    private IEnumerator FadeIn(CanvasGroup cg)
    {
        cg.gameObject.SetActive(true);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        cg.alpha = 1f;
    }

    private IEnumerator FadeOut(CanvasGroup cg)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        cg.alpha = 0f;
        cg.gameObject.SetActive(false);
    }

    private void HideAllInstant()
    {
        foreach (CanvasGroup cg in uiElements)
        {
            cg.alpha = 0f;
            cg.gameObject.SetActive(false);
        }
    }
}