using UnityEngine;
using Vuforia; // Necesario

public class AudioPanelManager : MonoBehaviour
{
    public GameObject panelReproduciendo;
    public GameObject panelPausado;
    public GameObject advertenciaUI;
    public AudioSource audioSource;

    // Referencia al objeto que tiene el BarcodeBehaviour
    public BarcodeBehaviour barcodeTarget;
    private bool haEscaneado = false;

    void Update()
    {
        if (haEscaneado) return;

        // Cambiamos la forma de verificar si el objeto está siendo rastreado
        if (barcodeTarget != null && barcodeTarget.enabled)
        {
            ActivarAudioAR();
            haEscaneado = true;
        }
    }

    public void ActivarAudioAR()
    {
        if (advertenciaUI) advertenciaUI.SetActive(false);
        if (panelReproduciendo) panelReproduciendo.SetActive(true);
        if (audioSource) audioSource.Play();
    }

    // ... mantén tus otros métodos (PausarAudio, etc.) igual que antes
    public void PausarAudio()
    {
        if (audioSource != null) audioSource.Pause();
        if (panelReproduciendo != null) panelReproduciendo.SetActive(false);
        if (panelPausado != null) panelPausado.SetActive(true);
    }

    public void ReanudarAudio()
    {
        if (audioSource != null) audioSource.UnPause();
        if (panelReproduciendo != null) panelReproduciendo.SetActive(true);
        if (panelPausado != null) panelPausado.SetActive(false);
    }

    public void DetenerAudio()
    {
        if (audioSource != null) audioSource.Stop();
        if (panelReproduciendo != null) panelReproduciendo.SetActive(false);
        if (panelPausado != null) panelPausado.SetActive(false);
        if (advertenciaUI != null) advertenciaUI.SetActive(true);

        // IMPORTANTE: Un pequeño retraso antes de volver a permitir el escaneo
        Invoke("ReiniciarEscaneo", 2.0f);
    }

    void ReiniciarEscaneo()
    {
        haEscaneado = false;
    }
}