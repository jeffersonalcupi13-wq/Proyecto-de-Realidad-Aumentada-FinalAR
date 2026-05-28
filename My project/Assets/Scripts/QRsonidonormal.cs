using UnityEngine;
using System.Collections;

public class QRsonidonormal : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pitido;
    public AudioClip audioFinal;

    private bool yaDetectado = false;

    // LLAMA ESTA FUNCIÓN cuando tu scanner detecte cualquier QR
    public void QRDetectado()
    {
        if (!yaDetectado)
        {
            yaDetectado = true;
            StartCoroutine(ReproducirSecuencia());
        }
    }

    IEnumerator ReproducirSecuencia()
    {
        // 1. Pitido
        audioSource.PlayOneShot(pitido);

        // Esperar que termine
        yield return new WaitForSeconds(pitido.length);

        // 2. Audio principal
        audioSource.PlayOneShot(audioFinal);
    }
}