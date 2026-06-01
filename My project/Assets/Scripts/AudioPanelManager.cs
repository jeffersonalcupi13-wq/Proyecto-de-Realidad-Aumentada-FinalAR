using UnityEngine;
using Vuforia;

public class AudioPanelManager : MonoBehaviour
{
    public GameObject panelReproduciendo;
    public GameObject panelPausado;
    public GameObject advertenciaUI;
    public AudioSource audioSource;

    private ObserverBehaviour observer;

    private bool qrDetectado = false;
    private bool cerradoManual = false;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        if (audioSource != null)
            audioSource.Stop();

        panelReproduciendo.SetActive(false);
        panelPausado.SetActive(false);
        advertenciaUI.SetActive(true);

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
             !qrDetectado &&
             !cerradoManual)
        {
            qrDetectado = true;
            ActivarAudioAR();
        }

        // QR perdido
        else if (status.Status == Status.NO_POSE)
        {
            qrDetectado = false;

        }
    }

    void ActivarAudioAR()
    {
        advertenciaUI.SetActive(false);
        panelReproduciendo.SetActive(true);
        panelPausado.SetActive(false);

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void PausarAudio()
    {
        Debug.Log("PAUSA");

        audioSource.Pause();
        panelReproduciendo.SetActive(false);
        panelPausado.SetActive(true);
    }

    public void ReanudarAudio()
    {
        Debug.Log("REANUDAR");

        audioSource.UnPause();
        panelPausado.SetActive(false);
        panelReproduciendo.SetActive(true);
    }

    public void RepetirAudio()
    {
        Debug.Log("REPETIR");

        audioSource.Stop();
        audioSource.Play();
    }

    public void DetenerAudio()
    {
        Debug.Log("DETENER");

        cerradoManual = true;

        audioSource.Stop();
        panelReproduciendo.SetActive(false);
        panelPausado.SetActive(false);
        advertenciaUI.SetActive(true);
    }
}