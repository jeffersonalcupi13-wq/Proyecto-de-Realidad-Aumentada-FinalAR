using UnityEngine;
using Vuforia;

public class AudioPanelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelReproduciendo;
    public GameObject panelPausado;
    public GameObject advertenciaUI;

    [Header("Audio")]
    public AudioSource audioSource;

    private ObserverBehaviour observer;

    // Control
    private bool audioActivo = false;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        // Estado inicial
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.playOnAwake = false;
        }

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(false);

        if (panelPausado != null)
            panelPausado.SetActive(false);

        if (advertenciaUI != null)
            advertenciaUI.SetActive(true);

        // Evento QR
        if (observer != null)
            observer.OnTargetStatusChanged += OnStatusChanged;
    }

    void OnDestroy()
    {
        if (observer != null)
            observer.OnTargetStatusChanged -= OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        // SOLO cuando se detecta por primera vez
        if ((status.Status == Status.TRACKED ||
             status.Status == Status.EXTENDED_TRACKED) &&
             !audioActivo)
        {
            ActivarAudioAR();
        }

        // Cuando pierde QR NO HACER NADA
        // Audio y panel permanecen
    }

    void ActivarAudioAR()
    {
        if (advertenciaUI != null)
            advertenciaUI.SetActive(false);

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(true);

        if (panelPausado != null)
            panelPausado.SetActive(false);

        // Evitar doble audio
        if (audioSource != null && !audioActivo)
        {
            audioSource.Play();
            audioActivo = true;
        }
    }

    // PAUSAR
    public void PausarAudio()
    {
        if (audioSource != null)
            audioSource.Pause();

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(false);

        if (panelPausado != null)
            panelPausado.SetActive(true);
    }

    // REANUDAR
    public void ReanudarAudio()
    {
        if (audioSource != null)
            audioSource.UnPause();

        if (panelPausado != null)
            panelPausado.SetActive(false);

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(true);
    }

    // REPETIR
    public void RepetirAudio()
    {
        if (audioSource != null)
        {
            audioSource.time = 0f;
            audioSource.Play();
        }

        if (panelPausado != null)
            panelPausado.SetActive(false);

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(true);
    }

    // X
    public void DetenerAudio()
    {
        if (audioSource != null)
            audioSource.Stop();

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(false);

        if (panelPausado != null)
            panelPausado.SetActive(false);

        // Reset para permitir nuevo escaneo
        audioActivo = false;

        // Advertencia vuelve en 4s
        Invoke(nameof(MostrarAdvertencia), 4f);
    }

    void MostrarAdvertencia()
    {
        if (advertenciaUI != null)
            advertenciaUI.SetActive(true);
    }
}