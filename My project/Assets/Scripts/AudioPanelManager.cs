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

    // Control interno
    private bool qrDetectado = false;
    private bool audioIniciado = false;

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

        // Conectar evento Vuforia
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
        // QR detectado
        if ((status.Status == Status.TRACKED ||
             status.Status == Status.EXTENDED_TRACKED) &&
             !qrDetectado)
        {
            qrDetectado = true;
            ActivarAudioAR();
        }

        // QR perdido
        else if (status.Status == Status.NO_POSE)
        {
            // NO apagar nada
            // Solo permitir nuevo tracking sin duplicar
            qrDetectado = false;
        }
    }

    void ActivarAudioAR()
    {
        if (advertenciaUI != null)
            advertenciaUI.SetActive(false);

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(true);

        if (panelPausado != null)
            panelPausado.SetActive(false);

        // Solo iniciar una vez
        if (audioSource != null && !audioIniciado)
        {
            audioSource.Play();
            audioIniciado = true;
        }
    }

    // BOTÓN PAUSA
    public void PausarAudio()
    {
        if (audioSource != null)
            audioSource.Pause();

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(false);

        if (panelPausado != null)
            panelPausado.SetActive(true);
    }

    // BOTÓN REANUDAR
    public void ReanudarAudio()
    {
        if (audioSource != null)
            audioSource.UnPause();

        if (panelPausado != null)
            panelPausado.SetActive(false);

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(true);
    }

    // BOTÓN REPETIR
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

    // BOTÓN X
    // BOTÓN X
    public void DetenerAudio()
    {
        if (audioSource != null)
            audioSource.Stop();

        if (panelReproduciendo != null)
            panelReproduciendo.SetActive(false);

        if (panelPausado != null)
            panelPausado.SetActive(false);

        // Ocultar advertencia por ahora
        if (advertenciaUI != null)
            advertenciaUI.SetActive(false);

        // Reset
        audioIniciado = false;
        qrDetectado = false;

        // Mostrar advertencia en 4 segundos
        Invoke("MostrarAdvertencia", 4f);
    }

    void MostrarAdvertencia()
    {
        if (advertenciaUI != null)
            advertenciaUI.SetActive(true);
    }
}