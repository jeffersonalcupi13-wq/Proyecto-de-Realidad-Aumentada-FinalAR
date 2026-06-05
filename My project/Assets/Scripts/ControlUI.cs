using System.Collections;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public CanvasGroup[] uiElements;

    public float fadeDuration = 0.5f;
    public float visibleTime = 5f;

    private Coroutine sequenceCoroutine;
    private bool targetFound;

    private void Start()
    {
        HideAllInstant();
    }

    public void OnTargetFound()
    {
        if (targetFound) return;

        targetFound = true;

        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);

        sequenceCoroutine = StartCoroutine(PlaySequence());
    }

    public void OnTargetLost()
    {
        targetFound = false;

        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }

        HideAllInstant();
    }

    private IEnumerator PlaySequence()
    {
        int index = 0;

        while (targetFound)
        {
            CanvasGroup current = uiElements[index];

            yield return StartCoroutine(FadeIn(current));

            yield return new WaitForSeconds(visibleTime);

            yield return StartCoroutine(FadeOut(current));

            index++;

            if (index >= uiElements.Length)
                index = 0;
        }
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