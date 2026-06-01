using System.Collections;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public CanvasGroup[] uiElements;

    public float showTime = 1f;

    public float fadeDuration = 0.3f;
    public float popScale = 0.8f;
    public float endScale = 0.9f;

    private Coroutine routine;

    void Start()
    {
        HideAllInstant();
    }

    public void OnTargetFound()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(PlaySequence());
    }

    public void OnTargetLost()
    {
        if (routine != null)
            StopCoroutine(routine);

        HideAllInstant();
    }

    IEnumerator PlaySequence()
    {
        int index = 0;

        while (true)
        {
            HideAllInstant();

            yield return StartCoroutine(AnimateIn(uiElements[index]));

            yield return new WaitForSeconds(showTime);

            yield return StartCoroutine(AnimateOut(uiElements[index]));

            index++;

            if (index >= uiElements.Length)
                index = 0;
        }
    }

    IEnumerator AnimateIn(CanvasGroup ui)
    {
        ui.gameObject.SetActive(true);

        float t = 0;

        ui.alpha = 0;
        ui.transform.localScale = Vector3.one * popScale;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float p = t / fadeDuration;

            ui.alpha = Mathf.Lerp(0, 1, p);
            ui.transform.localScale = Vector3.Lerp(
                Vector3.one * popScale,
                Vector3.one,
                p
            );

            yield return null;
        }

        ui.alpha = 1;
        ui.transform.localScale = Vector3.one;
    }

    IEnumerator AnimateOut(CanvasGroup ui)
    {
        float t = 0;

        Vector3 startScale = ui.transform.localScale;
        Vector3 targetScale = Vector3.one * endScale;

        float startAlpha = ui.alpha;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float p = t / fadeDuration;

            ui.alpha = Mathf.Lerp(startAlpha, 0, p);
            ui.transform.localScale = Vector3.Lerp(startScale, targetScale, p);

            yield return null;
        }

        ui.alpha = 0;
        ui.transform.localScale = Vector3.one;
        ui.gameObject.SetActive(false);
    }

    void HideAllInstant()
    {
        foreach (var ui in uiElements)
        {
            ui.alpha = 0;
            ui.transform.localScale = Vector3.one;
            ui.gameObject.SetActive(false);
        }
    }
}