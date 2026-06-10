using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [Header("Advertencia")]
    public CanvasGroup warningUI;

    [Header("Pantallas")]
    public CanvasGroup[] uiElements;

    [Header("Audio")]
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    private bool targetFound = false;
    private bool firstClick = true;
    private int currentIndex = -1;

    void Start()
    {
        HideEverything();
    }

    public void OnTargetFound()
    {
        targetFound = true;

        HideEverything();

        if (warningUI != null)
            Show(warningUI);

        firstClick = true;
        currentIndex = -1;
    }

    public void OnTargetLost()
    {
        targetFound = false;

        if (audioSource != null)
            audioSource.Stop();

        HideEverything();
    }

    void Update()
    {
        if (!targetFound)
            return;

        bool clicked = Input.GetMouseButtonDown(0);

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            clicked = true;
        }
#endif

        if (!clicked)
            return;

        // Primer click
        if (firstClick)
        {
            firstClick = false;

            if (warningUI != null)
                Hide(warningUI);
        }

        // Ocultar pantalla anterior
        if (currentIndex >= 0)
        {
            Hide(uiElements[currentIndex]);
        }

        // Siguiente pantalla
        currentIndex++;

        if (currentIndex >= uiElements.Length)
            currentIndex = 0;

        // Mostrar pantalla
        Show(uiElements[currentIndex]);

        // Audio correspondiente
        if (audioSource != null &&
            currentIndex < audioClips.Length &&
            audioClips[currentIndex] != null)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();
        }
    }

    void Show(CanvasGroup cg)
    {
        if (cg == null) return;

        cg.gameObject.SetActive(true);
        cg.alpha = 1f;
    }

    void Hide(CanvasGroup cg)
    {
        if (cg == null) return;

        cg.alpha = 0f;
        cg.gameObject.SetActive(false);
    }

    void HideEverything()
    {
        if (warningUI != null)
            Hide(warningUI);

        foreach (CanvasGroup cg in uiElements)
        {
            if (cg != null)
                Hide(cg);
        }
    }
}